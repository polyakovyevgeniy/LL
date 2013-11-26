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
using System.Windows.Shapes;
using Microsoft.Win32;
using LL.Models;

namespace LL.Views
{
    /// <summary>
    /// Interaction logic for AddBookView.xaml
    /// </summary>
    public partial class AboutLLView : Window
    {
        public AboutLLView()
        {
            InitializeComponent();
            WindowRect.MouseLeftButtonDown += (sender, e) => DragMove(); // Перемещение окна
            btnWindowClose.Click += (sender, e) => this.Close(); // Закрытие окна           
        }       
    }
}
