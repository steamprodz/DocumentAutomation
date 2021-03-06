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
    public partial class LabeledTextBox : UserControl
    {
        [Category("MyApp")]
        public String LabelText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(String), typeof(LabeledTextBox), new PropertyMetadata("Label", LabelTextChanged));

        private static void LabelTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var labeledTextBox = (LabeledTextBox)d;
            labeledTextBox.Label.Content = e.NewValue;
        }

        [Category("MyApp")]
        public String TextBoxText
        {
            get { return (String)GetValue(TextBoxTextProperty); }
            set { SetValue(TextBoxTextProperty, value); }
        }

        public static readonly DependencyProperty TextBoxTextProperty =
            DependencyProperty.Register("TextBoxText", typeof(String), typeof(LabeledTextBox), new PropertyMetadata("Text", TextBoxTextChanged));

        private static void TextBoxTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var labeledTextBox = (LabeledTextBox)d;
            labeledTextBox.TextBox.Text = e.NewValue.ToString().Trim();
        }

        [Category("MyApp")]
        public Double LabelWidth
        {
            get { return (Double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(Double), typeof(LabeledTextBox), new PropertyMetadata(Double.NaN, LabelWidthChanged));

        private static void LabelWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var labeledTextBox = (LabeledTextBox)d;
            labeledTextBox.Label.Width = (Double)e.NewValue;
        }


        public LabeledTextBox()
        {
            InitializeComponent();
        }
    }
}
