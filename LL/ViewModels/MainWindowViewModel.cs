using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LL.Models;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using LL.Views;
using System.ComponentModel;

using System.Collections.ObjectModel;

using LL.App_Code;

namespace LL.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        #region Constructor

        public BooksModel DataModel { get; set; }

        public MainWindowViewModel()
        {

            ((App)(App.Current)).BooksModel = new Models.BooksModel();
            DataModel = ((App)(App.Current)).BooksModel;

            // Обновляем список книг в ListBox
            UpdateBookListBox();

            // Добавить книгу
            AddBookCommand = new Command(arg => {       
            var editBook = new AddBookView();
            //var addBookViewModel = new AddBookViewModel();
            //editBook.DataContext = addBookViewModel;
            editBook.ShowDialog();
            UpdateBookListBox();
        });
            // Удалить книгу
            DeleteBookCommand = new Command(selectedBook => 
            {

                if (selectedBook == null) return; // TODO: Реализовать отключение команды с помощью CanExecute
                if (!DataModel.DeleteBook(((Book)selectedBook).Id))
                    MessageBox.Show("Error Deleting Books", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });
            // Обновляяет состояние команд для книг
            UpdateBookCommandStateCommand = new Command(arg =>
            {
                if (BookCurrentSelected != null)
                    CanShaowOrEditOrDeleteBook = true;
                else
                    CanShaowOrEditOrDeleteBook = false;
            });

            UpdateNoteCommandStateCommand = new Command(arg => 
            {
                if (NoteCurrentSelected != null)
                    CanShowOrEditOrDeleteNote = true;
                else
                    CanShowOrEditOrDeleteNote = false;

                if (BookCurrentSelected != null)
                    CanAddNote = true;
                else
                    CanAddNote = false;
            });
            // Добавить заметку
            AddNoteCommand = new Command(selectedBook =>
                {
                    if (selectedBook == null) return; // TODO: Реализовать отключение команды с помощью CanExecute
                    var addNoteWindow = new AddNoteView();
                    var addNoteWiewModel = new AddNoteViewModel();
                    addNoteWiewModel.SelectedBookId = ((Book)selectedBook).Id;
                    addNoteWindow.DataContext = addNoteWiewModel;
                    addNoteWindow.ShowDialog();
                    UpdateNoteListBox();

                });
            // Удалить заметку
            DeleteNoteCommand = new Command(selectNote =>
            {
                if (selectNote == null) return; // TODO: Реализовать отключение команды с помощью CanExecute
                if (!DataModel.DeleteNote(((Note)selectNote).Id)) 
                    MessageBox.Show("Error Deleting Notes", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                UpdateNoteListBox();
            }); 
            ShowNoteInViewerCommand = new Command(arg => ShowNoteInViewerMethod(arg));
            EditingBookCommand = new Command(arg => ChangeEditBookMethod()); // Редактирование книги
            EditingNoteCommand = new Command(arg => ChangeEditNoteMethod());// Редактирование закладки

            ShowApplicationSettingsCommand = new Command(arg => new SettingsView().ShowDialog()); // Показать настройки приложения

            SaveCommand = new Command(arg => 
            {
                ((App)Application.Current).SaveApplicationFile();
            });
            OpenCommand = new Command(arg =>
            {
                ((App)(App.Current)).LoadApplicationFile();
                UpdateBookListBox();
            });

            AboutBoxShowCommand = new Command(arg => { new AboutLLView().ShowDialog(); });// Окно о программе
            DataModel = ((App)(Application.Current)).BooksModel; // Модель данных
            ChoosePathCommand = new Command(arg => ChoosePathMethod(arg)); // Выбор книги на жестком диске
            ChangeSelectedListItemComand = new Command(arg => SelectChanged(arg)); // Изменение выбранной книги
        }

        /// <summary>
        /// Отобразить заметку во вьювере
        /// </summary>
        /// <param name="selectedNote"></param>
        /// <returns></returns>
        private void ShowNoteInViewerMethod(object selectedNote)
        {
            StringBuilder strBld = new StringBuilder();

            strBld.Append("----------TITLE----------");
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append(((Note)selectedNote).Title);
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append("----------TAG----------");
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append(((Note)selectedNote).Tag);
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append("----------CODE----------");
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append(((Note)selectedNote).Code);
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append("----------DESCRIPTION----------");
            strBld.Append(Environment.NewLine);
            strBld.Append(Environment.NewLine);
            strBld.Append(((Note)selectedNote).Description);

            Global.NoteViewerViewModel.Value.ViewerContent = strBld.ToString();
            new NoteViewerView().ShowDialog();
        }       
            
        /// <summary>
        /// Сменить оасположение книги на жестком диске
        /// </summary>
        /// <returns></returns>
        private void ChoosePathMethod(object selectedBook)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true) { DataModel.Books.SingleOrDefault(book=> book.Id == ((Book)selectedBook).Id).BookLocation = ofd.FileName; }
        }
        /// <summary>
        /// Включение возможности редактирования Заметки
        /// </summary>
        private void ChangeEditNoteMethod()
        {
            if (IsEditingNote == true)
                IsEditingNote = false;
            else
            {
                IsEditingNote = true;
                UpdateNoteListBox();
            }                
        }
        /// <summary>
        /// Включение возможности редактрования книги
        /// </summary>
        private void ChangeEditBookMethod()
        {
            if (IsEditingBook == true)
            {
                IsEditingBook = false;
            }
            else
            {
                IsEditingBook = true;
                UpdateBookListBox();
            }
        }

        private List<Note> _listSource;
        /// <summary>
        /// Отсортированные список для Заметок
        /// </summary>
        public List<Note> ListNoteSource
        {
            get { return _listSource; }
            set 
            {
                if (ListNoteSource == value) return;
                _listSource = value;
                OnPropertyChanged("ListNoteSource");                
            }
        }

        private List<Book> _listBookSorce;

        public List<Book> ListBookSorce
        {
            get { return _listBookSorce; }
            set 
            {
                if (ListBookSorce == value) return;
                _listBookSorce = value;
                OnPropertyChanged("ListBookSorce");
            }
        }


        private Book _bookCurrentSelected;
        /// <summary>
        /// Выделенная книга в ListBox
        /// </summary>
        public Book BookCurrentSelected
        {
            get { return _bookCurrentSelected; }
            set 
            {               
                if(BookCurrentSelected == value) return;
                _bookCurrentSelected = value;
                OnPropertyChanged("BookCurrentSelected");
            }
        }

        private Note _noteCurrentSelected;

        public Note NoteCurrentSelected
        {
            get { return _noteCurrentSelected; }
            set 
            {               
                if (NoteCurrentSelected == value) return;
                _noteCurrentSelected = value;
                OnPropertyChanged("NoteCurrentSelected");
            }
        }

        private bool _canEditOrDeleteNote;

        public bool CanShowOrEditOrDeleteNote
        {
            get { return _canEditOrDeleteNote; }
            set 
            {
                if (CanShowOrEditOrDeleteNote == value) return;
                _canEditOrDeleteNote = value;
                OnPropertyChanged("CanShowOrEditOrDeleteNote");
            }
        }

        private bool _canAddNote;

        public bool CanAddNote
        {
            get { return _canAddNote; }
            set 
            {
                if (CanAddNote == value) return;
                _canAddNote = value;
                OnPropertyChanged("CanAddNote");
            }
        }
        
        

        /// <summary>
        /// Обновляет список заметок при изменении их в источнике данных
        /// </summary>
        public void UpdateNoteListBox()
        {
            ListNoteSource = DataModel.Notes.Where(notes => notes.BookId == BookCurrentSelected.Id).ToList();
        }
        /// <summary>
        /// Обновляет список книг при измеенении их в источнике данных
        /// </summary>
        public void UpdateBookListBox()
        {
            ListBookSorce = DataModel.Books.AsEnumerable().ToList();            
        }
        

        #endregion

        #region Properties

        private string applicationStatus = "Ready";
        /// <summary>
        /// Состояние приложения
        /// </summary>
        public string ApplicationStatus
        {
            get { return applicationStatus; }
            set 
            {
                if (ApplicationStatus == value) return;
                applicationStatus = value;
                OnPropertyChanged( "ApplicationStatus");                
            }
        }
        private bool isEditingNote = true;
        /// <summary>
        /// Редактирование заметки ON/OFF
        /// </summary>
        public bool IsEditingNote
        {
            get { return isEditingNote; }
            set
            {
                if (IsEditingNote == value) return;
                isEditingNote = value;
                OnPropertyChanged("IsEditingNote");


                if (value == false)
                    ApplicationStatus = "Note Editing...";
                else ApplicationStatus = "Ready";               
            }
        }        
        private bool isEditingBook = true;
        /// <summary>
        /// Редактирование книги ON/OFF
        /// </summary>
        public bool IsEditingBook
        {
            get { return isEditingBook; }
            set
            {
                if (IsEditingBook == value) return;
                isEditingBook = value;
                OnPropertyChanged("IsEditingBook");

                 if (value == false)
                    ApplicationStatus = "Book Editing...";
                else ApplicationStatus = "Ready";

                 if (IsEditingBook == true)
                     IsExpanded = true;
            }
        }

        private bool isExpanded;
        /// <summary>
        /// Возвращает или устанавливает состояние развернутости раздела книг на форме
        /// </summary>
        public bool IsExpanded
        {
            //get { return isExpanded; }
            set 
            {
                //if (IsExpanded == value) return;
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }


        private bool _canEditBook;
        /// <summary>
        /// Получает или устанавливает доступность редактирования книги
        /// </summary>
        public bool CanShaowOrEditOrDeleteBook
        {
            get { return _canEditBook; }
            set 
            {
                if (CanShaowOrEditOrDeleteBook == value) return;
                _canEditBook = value;
                OnPropertyChanged("CanShaowOrEditOrDeleteBook");
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Сохранить рабочее пространство
        /// </summary>
        public ICommand OpenCommand { get; set; }
        /// <summary>
        /// Обновить сосотояние команд для книг
        /// </summary>
        public ICommand UpdateNoteCommandStateCommand { get; set; }
        /// <summary>
        /// Обновляет состояние комманд для книги
        /// </summary>
        public ICommand UpdateBookCommandStateCommand { get; set; }        
        /// <summary>
        /// Сохранить
        /// </summary>
        public ICommand SaveCommand { get; set; }
        /// <summary>
        /// Показать настройки приложения
        /// </summary>
        public ICommand ShowApplicationSettingsCommand { get; set; }
        
        /// <summary>
        /// Показать заметку во вьювере
        /// </summary>
        public ICommand ShowNoteInViewerCommand { get; set; }
        /// <summary>
        /// Окно о программе
        /// </summary>
        public ICommand AboutBoxShowCommand { get; set; }
        /// <summary>
        /// Удалить заметку
        /// </summary>
        public ICommand DeleteNoteCommand { get; set; }
        /// <summary>
        /// Удаление книги
        /// </summary>
        public ICommand DeleteBookCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand ChoosePathCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand EditingNoteCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand EditingBookCommand { get; set; }        
        /// <summary>
        /// Добавляем Книгу
        /// </summary>
        public ICommand AddBookCommand { get; set; }
        /// <summary>
        /// Добавляес закладку
        /// </summary>
        public ICommand AddNoteCommand { get; set; }
        /// <summary>
        /// Выбор в списке новой книги
        /// </summary>
        public ICommand ChangeSelectedListItemComand { get; set; }       
        /// <summary>
        /// Модель данных
        /// </summary>
        /// 
        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName: propertyName));
        }

        #endregion

        public void SelectChanged(object parameter)
        {
            if (parameter != null)
                UpdateNoteListBox();          
        }        
    } 
}
