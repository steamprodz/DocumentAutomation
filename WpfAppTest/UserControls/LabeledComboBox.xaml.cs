using System;
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
        public String ComboBoxText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty TextBoxTextProperty =
            DependencyProperty.Register("ComboBoxText", typeof(String), typeof(LabeledComboBox), new PropertyMetadata("ComboText", ComboBoxTextChanged));

        private static void ComboBoxTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var labeledComboBox = (LabeledComboBox)d;
            labeledComboBox.ComboBox_PartName.Text = e.NewValue.ToString();
        }
        #endregion

        public LabeledComboBox()
        {
            InitializeComponent();

            this.ComboBox_PartName.IsEnabled = false;
            this.ComboBox_AssociatedDocuments.IsEnabled = false;
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

                this.ComboBox_AssociatedDocuments.Items.Add(comboItem);

                // Select item if only one presented
                if (this.ComboBox_AssociatedDocuments.Items.Count == 1)
                    this.ComboBox_AssociatedDocuments.SelectedItem = comboItem;
            }
        }
    }
}
