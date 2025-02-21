using System;
using System.Collections.Generic;
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

namespace Copier.Views
{
    /// <summary>
    /// Interaction logic for SelectFolder.xaml
    /// </summary>
    public partial class SelectFolder : UserControl
    {
        public SelectFolder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
         DependencyProperty.Register(
             "Title",
             typeof(string),
             typeof(UserControl),
             new PropertyMetadata(default));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SelectFolder;
        }
    }
}
