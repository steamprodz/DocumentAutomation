﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTest.UserControls;
using WpfAppTest.Helpers;

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String customQuestion = "";

        public Point MousePosition { get; private set; }

        public MainWindow()
        {
            LoadCulture();

            InitializeComponent();
        }

        private void LoadCulture()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
        }

        private void UpdateTextBoxQuestionFont(bool textEntered)
        {
            if (textEntered)
            {
                this.textBox_CustomQuestion.Foreground = Brushes.Black;
                this.textBox_CustomQuestion.FontStyle = FontStyles.Normal;
            }
            else
            {
                this.textBox_CustomQuestion.Foreground = Brushes.Gray;
                this.textBox_CustomQuestion.FontStyle = FontStyles.Italic;
            }
        }

        private void TextBoxQuestionUpdateToFocus(bool gotFocus)
        {
            if (customQuestion == "")
            {
                if (gotFocus)
                {
                    this.textBox_CustomQuestion.Foreground = Brushes.Black;
                    this.textBox_CustomQuestion.FontStyle = FontStyles.Normal;
                    this.textBox_CustomQuestion.Text = "";
                }
                else
                {
                    this.textBox_CustomQuestion.Foreground = Brushes.Gray;
                    this.textBox_CustomQuestion.FontStyle = FontStyles.Italic;
                    this.textBox_CustomQuestion.Text = "Type question...";
                }
            }
        }

        private void TextBoxQuestionLostFocus()
        {
            if (customQuestion == "")
            {
                
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var documentControls = this.WrapPanel_FileBrowser.Children.OfType<DocumentControl>();

            foreach (var docControl in documentControls)
            {
                if (!docControl.IsMouseOver)
                {
                    if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        docControl.IsSelected = false;
                    }
                }
            }

            if (!this.textBox_CustomQuestion.IsMouseOver)
            {
                Keyboard.ClearFocus();
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filepath in openFileDialog.FileNames)
                {
                    var docControl = new DocumentControl();
                    docControl.FilePath = filepath;
                    docControl.FilenameText = System.IO.Path.GetFileName(filepath);

                    this.WrapPanel_FileBrowser.Children.Add(docControl);
                }
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var documentControls = this.WrapPanel_FileBrowser.Children.OfType<DocumentControl>().Where(x => x.IsSelected);

            int count = documentControls.Count();

            for (int i = count - 1; i >= 0; i--)
            {
                this.WrapPanel_FileBrowser.Children.Remove(documentControls.ElementAt(i));
            }
        }

        private void button_LoadFromDB_Click(object sender, RoutedEventArgs e)
        {
            string custId = this.LabeledTextBox_CustomerId.TextBox.Text;

            using (var context = new M1_FM_DEV_DataEntities())
            {
                var orgLocations = context.OrganizationLocations.Where(x => x.cmlOrganizationID == custId).FirstOrDefault();
                var orgContacts = context.OrganizationContacts.Where(x => x.cmcOrganizationID == custId).FirstOrDefault();

                this.labeledTextBox_FirstName.TextBoxText = orgContacts.UCMCFIRST;
                this.labeledTextBox_LastName.TextBoxText = orgContacts.UCMCLAST;
                this.labeledTextBox_ContactEmail.TextBoxText = orgContacts.cmcEMailAddress;

                this.labeledTextBox_CompanyName.TextBoxText = orgLocations.cmlName;
                this.labeledTextBox_Country.TextBoxText = orgLocations.cmlCountry;
                this.labeledTextBox_State.TextBoxText = orgLocations.cmlState;
                this.labeledTextBox_City.TextBoxText = orgLocations.cmlCity;
                this.labeledTextBox_PostalCode.TextBoxText = orgLocations.cmlPostCode;
                this.labeledTextBox_CountryCode.TextBoxText = orgLocations.cmlCountryCode;
                this.labeledTextBox_Address1.TextBoxText = orgLocations.cmlAddressLine1;
                this.labeledTextBox_Address2.TextBoxText = orgLocations.cmlAddressLine2;
                this.labeledTextBox_Address3.TextBoxText = orgLocations.cmlAddressLine3;
                this.labeledTextBox_Phone.TextBoxText = orgLocations.cmlPhoneNumber;
                this.labeledTextBox_Fax.TextBoxText = orgLocations.cmlFaxNumber;
                this.labeledTextBox_CompanyEmail.TextBoxText = orgLocations.cmlEMailAddress;

                //if (orgLocations != null)
                //{
                //    this.labeledTextBox_CustomerName.TextBoxText = orgLocations.cmlName;

                //    // Enable controls
                //    this.labeledTextBox_CustomerName.IsEnabled = true;
                //    this.labeledTextBox_SecondName.IsEnabled = true;
                //    this.labeledTextBox_BankAccount.IsEnabled = true;
                //}
                //else
                //{
                //    MessageBox.Show("Customer not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //    // Disable controls
                //    this.labeledTextBox_CustomerName.IsEnabled = false;
                //    this.labeledTextBox_SecondName.IsEnabled = false;
                //    this.labeledTextBox_BankAccount.IsEnabled = false;
                //}
            }
        }

        private void checkBox_CustomQuestion_Checked(object sender, RoutedEventArgs e)
        {
            this.textBox_CustomQuestion.IsEnabled = true;
        }

        private void checkBox_CustomQuestion_Unchecked(object sender, RoutedEventArgs e)
        {
            this.textBox_CustomQuestion.IsEnabled = false;
        }

        private void textBox_CustomQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_CustomQuestion.Text != "Type question...")
                this.customQuestion = textBox_CustomQuestion.Text;
        }

        private void textBox_CustomQuestion_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxQuestionUpdateToFocus(true);
        }

        private void textBox_CustomQuestion_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBoxQuestionUpdateToFocus(true);
        }

        private void textBox_CustomQuestion_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxQuestionUpdateToFocus(false);
        }

        private void textBox_CustomQuestion_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBoxQuestionUpdateToFocus(false);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            this.MousePosition = Mouse.GetPosition(this);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void DocumentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.textBox_CustomQuestion.Text = (sender as DocumentControl).TextBox_Filename.GetLines().ToString();
        }

        private void button_BrowseDir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ExportPDF_Click(object sender, RoutedEventArgs e)
        {
            PdfDoc pdfCreator = new PdfDoc(@"PDF/cover_page.pdf", @"output.pdf");
        }
    }
}
