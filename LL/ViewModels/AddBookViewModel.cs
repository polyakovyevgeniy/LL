using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using LL.Models;
using System.ComponentModel;

namespace LL.ViewModels
{
    public class AddBookViewModel: INotifyPropertyChanged
    {
        public AddBookViewModel()
        {
            AddBookCommand = new Command(arg=> CreateBookMethod());
            ChoosePathCommand = new Command(arg => ChoosePathMethod());
            BooksModel = ((App) (Application.Current)).BooksModel; // Модель данных
        }

        #region

        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            {
                if (Title == value) return;
                _title = value;
                OnNotifyPropertyChanged("Title");
            }
        }
        private string _bookLocation;
        public string BookLocation
        {
            get { return _bookLocation; }
            set 
            {
                if (BookLocation == value) return;
                _bookLocation = value;
                OnNotifyPropertyChanged("BookLocation");
            }
        }

        private string _useCode;

        public string UseCode
        {
            get { return _useCode; }
            set 
            {
                if (UseCode == value) return;
                _useCode = value;
                OnNotifyPropertyChanged("UseCode");
            }
        }
        
        

        #endregion
        private void ChoosePathMethod()
        {
           var ofd = new Microsoft.Win32.OpenFileDialog();
                if (ofd.ShowDialog() == true) {BookLocation = ofd.FileName; }
        }       
        /// <summary>
        /// Создание новой книги
        /// </summary>
        private void CreateBookMethod()
        {
            if (!BooksModel.Addbook(Title, BookLocation, UseCode)) MessageBox.Show("Error adding books!");
            ClearProperties();
        }       
        /// <summary>
        /// Модель данных
        /// </summary>
        public BooksModel BooksModel { get; set; }
        /// <summary>
        /// Добавить книгу
        /// </summary>
        public ICommand AddBookCommand { get; set; }
     
        public ICommand ChoosePathCommand {get; set;}
        /// <summary>
        /// Ощищаем данные для добавления новой книги
        /// </summary>
        public void ClearProperties()
        {
            Title = String.Empty;
            this.BookLocation = String.Empty;
            this.UseCode = String.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName: propertyName));
        }
    }
}
