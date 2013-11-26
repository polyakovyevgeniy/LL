using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace LL.Models
{
    [Serializable]
    public class Note
    {
        #region Properties
      
        /// <summary>
        /// Возвращает или устанавливает идентификатор заметки
        /// </summary>
        public long Id { get; set; }      
        /// <summary>
        /// Возвращает или устанавливает идентификатор книги, которой принадлежит заметка
        /// </summary>
        public long BookId { get; set; }      
        /// <summary>
        /// Устанавливает или возврвщает название заметки
        /// </summary>
        public string Title { get; set; }      
        /// <summary>
        /// Возврвщает или устанавливает тег заметки
        /// </summary>
        public string Tag { get; set; }            
        /// <summary>
        /// Возврвщает или устанавливает описание заметки
        /// </summary>
        public string Description { get; set; }      
        /// <summary>
        /// Возвращает или устанавливает код заметки
        /// </summary>
        public string Code { get; set; }       
        /// <summary>
        /// Возвращает или устанавливает дату создания заметки
        /// </summary>
        public string CreationDate { get; set; }        
        /// <summary>
        /// Возващает или устанавливает страницу в книге относительно которой была добавлена заметка
        /// </summary>
        public int PageNumber { get; set; }

        #endregion       
    }
}