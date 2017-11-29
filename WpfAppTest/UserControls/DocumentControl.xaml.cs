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

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;

                if (value)
                    this.GridBackground.Background = Brushes.Blue;
                else
                    this.GridBackground.Background = Brushes.Transparent;
            }
        }

        public string FilePath { get; set; }

        public DocumentControl()
        {
            InitializeComponent();
            this.FilePath = "No File";
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
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData(DataFormats.StringFormat, this.FilePath.ToString());
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private void UserControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            var docDragControl = new DocumentControl();
            docDragControl.FilenameText = this.FilenameText;
            docDragControl.FilePath = this.FilePath;
            docDragControl.Width = this.Width;
            docDragControl.Height = this.Height;

            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);

            var gridMain = mainWindow.Grid_Main;
            gridMain.Children.Add(docDragControl);

            var mousePosWindow = Mouse.GetPosition(mainWindow);
            var mousePosDoc = Mouse.GetPosition(this);
            var mousePosScreen = this.PointToScreen(mousePosDoc);
            var topLeft = PointFromScreen(new Point(mousePosScreen.X - mousePosDoc.X, mousePosScreen.Y - mousePosDoc.Y));
            var botRight = PointFromScreen(new Point(mousePosScreen.X - mousePosDoc.X + docDragControl.ActualWidth, mousePosScreen.Y - mousePosDoc.Y + docDragControl.ActualHeight));
            //docDragControl.Margin = new Thickness(mousePosWindow.X - mousePosDoc.X, mousePosWindow.Y - mousePosDoc.Y, gridMain.ActualWidth, 100);
            docDragControl.Margin = new Thickness(topLeft.X, topLeft.Y, mainWindow.ActualWidth - botRight.X, mainWindow.ActualHeight - botRight.Y);
        }
    }
}
