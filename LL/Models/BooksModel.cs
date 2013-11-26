using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace LL.Models
{
    [Serializable]
    public class BooksModel
    {
        private ObservableCollection<Book> _books;
        /// <summary>
        /// Список книг
        /// </summary>
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }

        private ObservableCollection<Note> _notes;
        /// <summary>
        /// Список заметок
        /// </summary>
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }       
        
      
        public BooksModel()
        {
            // TODO: Реализовать возможность загрузки книг из базы, на основе пути к базе из настроек приложения
            Notes = new ObservableCollection<Note>()
            {
                new Note {Id = 0, Title = "Заметка1", BookId=1, Tag="Тег 1"},
                new Note {Id = 1,Title = "Заметка2", BookId=2, Tag="Тег2"},
                new Note {Id = 2, Title = "Заметка3", BookId=1},
            };

            Books = new ObservableCollection<Book>()
            {
                new Book {Title = "Книга1", Id=1},
                new Book {Title = "Книга2", Id=2},
                new Book {Title = "Книга3", Id=3}
            };
        }

        /// <summary>
        /// Добавить книгу
        /// </summary>
        /// <returns></returns>
        public bool Addbook(string bookTitle, string bookLocation, string bookUseCode)
        {
            try
            {
                Books.Add(new Book(){Id = GetNewBookId(), Title = bookTitle, BookLocation = bookLocation, UseCode = bookUseCode});
            
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Удаляем книгу
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool DeleteBook(long bookId)
        {
            try
            {  
                IEnumerable<Note> noteForDelete =  Notes.Where(note => note.BookId == bookId).ToList();

                if (noteForDelete.Count() > 0) // Если есть что удалять
                {                  
	                foreach (var item in noteForDelete)
                    {
                        Notes.Remove(item);
                    }                    

                    //for (var item in Notes.Where(note => note.BookId == bookId)) { Notes.Remove(item); } // Удаляем заметки принадлежащие книге
                }
                Books.Remove(Books.SingleOrDefault(book => book.Id == bookId)); // Удаляем книгу
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Добавить заметку в выбранную книгу
        /// </summary>
        /// <param name="bookId">Идентификатор книги</param>
        /// <param name="noteTitle"></param>
        /// <param name="noteTag"></param>
        /// <param name="code"></param>
        /// <param name="descriptoon"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public bool AddNote(long bookId, string noteTitle, string noteTag, string code, string descriptoon, int pageNumber)
        {
            try
            {
                Notes.Add(new Note()
                {
                    Id = GetNewNoteId(),
                    BookId = bookId,
                    Title = noteTitle,
                    Tag = noteTag,
                    Code = code,
                    Description = descriptoon,
                    PageNumber = pageNumber,
                    CreationDate = DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString()
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }
        /// <summary>
        /// Удалить заметку
        /// </summary>
        /// <param name="p">Идентификатор замкетки</param>
        /// <returns></returns>
        public bool DeleteNote(long noteId)
        {
            try
            {
                Notes.Remove(Notes.SingleOrDefault(note => note.Id == noteId));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        /// <summary>
        /// Возвращает новый(ещё не используемый) идентификатор для создаваемой книги
        /// </summary>
        /// <returns></returns>
        private long GetNewBookId()
        {
            //return (Books.Max(book => book.Id)) + 1;
            return Guid.NewGuid().GetHashCode();
        }
         /// <summary>
        /// Возвращает новый(ещё не используемый) идентификатор для создаваемой заметки
        /// </summary>
        /// <returns></returns>
        private long GetNewNoteId()
        {
            return (Notes.Max(note => note.Id)) + 1;
        }
        
    }
}
