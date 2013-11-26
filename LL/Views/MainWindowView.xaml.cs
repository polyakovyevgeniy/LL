using LL.Models;
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


namespace LL.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView: Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            WindowRect.MouseLeftButtonDown += (sender, e) => DragMove();
            // TODO: Реализовать проверку, сохранены ли все изменения
            btnWindowsClose.MouseLeftButtonDown += (sender, e) => Close();
            // Изменение состояния окна
            this.StateChanged += (sender, e) =>
            {
                if (this.WindowState == System.Windows.WindowState.Maximized)
                    contentBorder.Padding = new Thickness(6);
                else if (WindowState == System.Windows.WindowState.Normal)
                    contentBorder.Padding = new Thickness(0);
            };
             //Разворачивание окна и возврат в первоначальное положение
            btnWindowsMaximize.MouseLeftButtonDown += (senser, e) =>
            {
                WindowState = WindowState == System.Windows.WindowState.Maximized ?
                  System.Windows.WindowState.Normal
                  : System.Windows.WindowState.Maximized;
            };

            // Сворачивание окна
            btnWindowsMinimize.MouseLeftButtonDown += (sender, e) =>
                {
                    { WindowState = System.Windows.WindowState.Minimized; };
                };          
        }
    }
}
