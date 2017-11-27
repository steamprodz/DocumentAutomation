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

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for DocumentControl.xaml
    /// </summary>
    public partial class DocumentControl : UserControl
    {
        #region Design UI

        [Category("MyApp")]
        public String FilenameText
        {
            get { return (String)GetValue(FilenameTextProperty); }
            set { SetValue(FilenameTextProperty, value); }
        }

        public static readonly DependencyProperty FilenameTextProperty =
            DependencyProperty.Register("FilenameText", typeof(String), typeof(DocumentControl), new PropertyMetadata("Filename Text", FilenameTextChanged));

        private static void FilenameTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var documentControl = (DocumentControl)d;
            documentControl.TextBox_Filename.Text = e.NewValue.ToString();
        }
        
        #endregion

        public bool IsSelected { get; set; }

        public DocumentControl()
        {
            InitializeComponent();
        }

        private void GridBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsSelected)
                this.GridBackground.Background = Brushes.LightBlue;
        }

        private void GridBackground_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsSelected)
                this.GridBackground.Background = Brushes.Transparent;
        }

        private void GridBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.GridBackground.Background = Brushes.Blue;
        }
    }
}
