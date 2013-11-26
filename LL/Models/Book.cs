using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace LL.Models
{
    [Serializable]
    public class Book
    {
        #region Properties
      
        /// <summary>
        /// Возвращает или устанавливает название книги
        /// </summary>
        public string Title { get; set; }      
        /// <summary>
        /// Возврвщает или устанавливает идентификатор книги
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Возврвщает или устанавливает путь к книге на жестком диске
        /// </summary>
        public string BookLocation { get; set; }      
        /// <summary>
        /// Возвращает или устанавливает часто используемый код в книге
        /// </summary>
        public string UseCode { get; set; }

        #endregion       
    }
}