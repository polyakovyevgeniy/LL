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
    public class AddNoteViewModel : Note, INotifyPropertyChanged
    {
        public AddNoteViewModel()
        {
            AddNoteCommand = new Command(arg=> CreateNoteMethod());
            BooksModel = ((App) (Application.Current)).BooksModel; // Модель данных
        }
        #region Properties

       

        #endregion




        private long _selectedBookId;
        /// <summary>
        /// Возващает или устанавливает страницу в книге относительно которой была добавлена заметка
        /// </summary>
        public long SelectedBookId
        {
            get { return _selectedBookId; }
            set
            {
                if (this.SelectedBookId == value) return;
                _selectedBookId = value;
                OnNotifyPropertyChanged("SelectedBook");
            }
        }       

        /// <summary>
        ///  Создание заметки
        /// </summary>
        private void CreateNoteMethod()
        {
            if (!BooksModel.AddNote(
                bookId:SelectedBookId, 
                noteTitle: Title, 
                noteTag: Tag,
                code: Code,
                descriptoon:Description,
                pageNumber: PageNumber)) 
                MessageBox.Show("Error adding Note!");

            ClearProperties(); // Очищаем контролы после добавлеия заметки
        }
        /// <summary>
        /// Модель данных
        /// </summary>
        public BooksModel BooksModel { get; set; }

        public ICommand AddNoteCommand { get; set; }
    
        /// <summary>
        /// Ощищаем данные для добавления новой книги
        /// </summary>
        public void ClearProperties()
        {
            this.Title = String.Empty;
            this.Tag = String.Empty;
            this.Code = String.Empty;
            this.Description = String.Empty;
            this.PageNumber = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName: PropertyName));
        }
    }
}
