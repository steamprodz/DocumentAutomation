using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
using WpfAppTest.Database;

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<DocumentControl> selectedDocControls = new List<DocumentControl>();

        public MainWindow()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            InitializeComponent();
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
            string employeeId = this.LabeledTextBox_CustomerId.TextBox.Text;

            using (var context = new M1_FM_DEV_DataEntities())
            {
                var employee = context.Employees.Where(x => x.lmeEmployeeID == employeeId).FirstOrDefault();

                if (employee != null)
                {
                    this.labeledTextBox_FirstName.TextBoxText = employee.lmeEmployeeName;

                    // Enable controls
                    this.labeledTextBox_FirstName.IsEnabled = true;
                    this.labeledTextBox_SecondName.IsEnabled = true;
                    this.labeledTextBox_BankAccount.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Customer not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    // Disable controls
                    this.labeledTextBox_FirstName.IsEnabled = false;
                    this.labeledTextBox_SecondName.IsEnabled = false;
                    this.labeledTextBox_BankAccount.IsEnabled = false;
                }
            }
        }
    }
}
