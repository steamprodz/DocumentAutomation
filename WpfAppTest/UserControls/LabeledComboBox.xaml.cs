﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace WpfAppTest.UserControls
{
    /// <summary>
    /// Interaction logic for LabeledTextBox.xaml
    /// </summary>
    public partial class LabeledComboBox : UserControl
    {
        #region Designer UI
        [Category("MyApp")]
        public String CheckBoxText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("CheckBoxText", typeof(String), typeof(LabeledComboBox), new PropertyMetadata("Checkbox", CheckBoxTextChanged));

        private static void CheckBoxTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var labeledComboBox = (LabeledComboBox)d;
            labeledComboBox.CheckBox.Content = e.NewValue;
        }

        [Category("MyApp")]
        public Boolean Checked
        {
            get { return (Boolean)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked", typeof(Boolean), typeof(LabeledComboBox), new PropertyMetadata(false, CheckedChanged));

        private static void CheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var labeledComboBox = (LabeledComboBox)d;
            labeledComboBox.CheckBox.IsChecked = (Boolean?)e.NewValue;
        }
        #endregion

        public Dictionary<int, String> Filepaths { get; private set; }

        public LabeledComboBox()
        {
            InitializeComponent();

            this.Filepaths = new Dictionary<int, string>();

            this.ComboBox_PartName.IsEnabled = false;
            this.ComboBox_AssociatedDocuments.IsEnabled = false;

            SetTextAsCount();
        }

        private void SetTextAsCount()
        {
            //var countLabel = (Label)LogicalTreeHelper.FindLogicalNode(this.ComboBox_AssociatedDocuments, "Label");
            var template = ComboBox_AssociatedDocuments.Template;
            var countLabel = (Label)template.FindName("Label", ComboBox_AssociatedDocuments);

            if (countLabel != null)
                countLabel.Content = "Documents count: " + this.ComboBox_AssociatedDocuments.Items.Count;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.ComboBox_PartName.IsEnabled = true;
            this.ComboBox_AssociatedDocuments.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ComboBox_PartName.IsEnabled = false;
            this.ComboBox_AssociatedDocuments.IsEnabled = false;
        }

        private void ComboBox_AssociatedDocuments_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                var comboItem = new ComboBoxItem();
                comboItem.Content = e.Data.GetData(DataFormats.StringFormat);
                comboItem.Style = this.Resources["ComboBoxItemStyle"] as Style;

                this.ComboBox_AssociatedDocuments.Items.Add(comboItem);
                this.Filepaths.Add(this.ComboBox_AssociatedDocuments.Items.Count - 1, e.Data.GetData("Filepath").ToString());

                //// Select item if only one presented
                //if (this.ComboBox_AssociatedDocuments.Items.Count == 1)
                //    this.ComboBox_AssociatedDocuments.SelectedItem = comboItem;

                SetTextAsCount();
            }
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dependencyObject = (DependencyObject)sender;

            while (!(dependencyObject is ComboBoxItem))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            var comboItem = (ComboBoxItem)dependencyObject;

            this.Filepaths.Remove(this.ComboBox_AssociatedDocuments.Items.IndexOf(comboItem));
            this.ComboBox_AssociatedDocuments.Items.Remove(comboItem);

            SetTextAsCount();
        }
    }
}
