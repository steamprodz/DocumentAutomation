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

        private MainWindow mainWindow;
        private DocumentControl dragControl;
        private Canvas canvas;
        private Point mouseDownPos;

        public string FilePath { get; set; }
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
                {
                    this.GridBackground.Background = Brushes.Blue;
                    this.GridBackground.Opacity = 0.5;
                }
                else
                    this.GridBackground.Background = Brushes.Transparent;
            }
        }

        public DocumentControl()
        {
            InitializeComponent();

            this.FilePath = "No File";
        }

        private void CopyControl()
        {
            canvas = new Canvas();
            mainWindow.Grid_Main.Children.Add(canvas);
            canvas.Width = mainWindow.Grid_Main.ActualWidth;
            canvas.Height = mainWindow.Grid_Main.ActualHeight;
            canvas.Margin = new Thickness(0, 0, 0, 0);

            dragControl = new DocumentControl();
            dragControl.FilenameText = this.FilenameText;
            dragControl.FilePath = this.FilePath;
            dragControl.Width = this.Width;
            dragControl.Height = this.Height;
            dragControl.Opacity = 0.7;
            dragControl.AllowDrop = false;
            dragControl.IsHitTestVisible = false;
            

            //mainWindow.Grid_Main.Children.Add(docDragControl);
            canvas.Children.Add(dragControl);

            Point relativePoint = this.TransformToAncestor(mainWindow).Transform(new Point(0, 0));

            Canvas.SetTop(dragControl, relativePoint.Y);
            Canvas.SetLeft(dragControl, relativePoint.X);
        }

        //private Point mousePosWindow;

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsSelected)
                this.GridBackground.Background = Brushes.LightBlue;

            if (dragControl != null && e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData(DataFormats.StringFormat, this.FilenameText.ToString());
                data.SetData("Filepath", this.FilePath);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);

                // Fire MouseUp event to dispose
                MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
                args.RoutedEvent = Mouse.MouseUpEvent;
                this.RaiseEvent(args);
            }

            //mousePosWindow = e.GetPosition(mainWindow);
            //mainWindow.textBox_CustomQuestion.Text = mousePosWindow.X + "; " + mousePosWindow.Y;
        }

        private void UserControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (dragControl != null)
            {
                var mousePosScreen = MouseHelper.GetMousePosition(); //mainWindow.MousePosition;
                mainWindow.textBox_CustomQuestion.Text = mousePosScreen.X + "; " + mousePosScreen.Y;
                //var mousePosDoc = Mouse.GetPosition(this);
                //var mousePosScreen = this.PointToScreen(mousePosWindow);
                var topLeft = mainWindow.PointFromScreen(new Point(mousePosScreen.X - mouseDownPos.X, mousePosScreen.Y - mouseDownPos.Y));
                var botRight = mainWindow.PointFromScreen(new Point(mousePosScreen.X - mouseDownPos.X + dragControl.Width, mousePosScreen.Y - mouseDownPos.Y + dragControl.Height));
                //docDragControl.Margin = new Thickness(mousePosWindow.X - mousePosDoc.X, mousePosWindow.Y - mousePosDoc.Y, gridMain.ActualWidth, 100);
                //docDragControl.Margin = new Thickness(topLeft.X, topLeft.Y, mainWindow.ActualWidth - botRight.X, mainWindow.ActualHeight - botRight.Y);
                //canvas.SetTop(docDragControl, topLeft.Y);
                Canvas.SetLeft(dragControl, topLeft.X);
                Canvas.SetTop(dragControl, topLeft.Y);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainWindow = (MainWindow)Window.GetWindow(this);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.IsSelected = true;

                CopyControl();
                mouseDownPos = e.GetPosition(this);

                //var mousePosScreen = canvas.PointFromScreen(MouseHelper.GetMousePosition());
                //Canvas.SetTop(docDragControl, mousePosScreen.X - mouseDownPos.X);
                //Canvas.SetLeft(docDragControl, mousePosScreen.Y - mouseDownPos.Y);
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsSelected)
                this.GridBackground.Background = Brushes.Transparent;
        }

        // Fired from MouseMove when DoDragDrop returns result
        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                if (dragControl != null)
                {
                    canvas.Children.Remove(dragControl);
                    mainWindow.Grid_Main.Children.Remove(canvas);

                    dragControl = null;
                    canvas = null;
                }
            }
        }
    }
}
