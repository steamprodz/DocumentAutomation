using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<DocumentControl> selectedDocControls = new List<DocumentControl>();

        public MainWindow()
        {
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
    }
}
