using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LL.Models;

namespace LL.Helpers.Loaded
{
    public class LoadApplication
    {
        /// <summary>
        /// Загружает новые данные в DataContext
        /// </summary>
        /// <param name="loadedFile"></param>
        /// <param name="curentDataContext"></param>
        public static void LoadFile(BooksModel loadedFile, BooksModel curentDataContext)
        {
            curentDataContext.Books.Clear(); // Очищаем данные если они там были
            curentDataContext.Notes.Clear();
            foreach (var item in loadedFile.Books) { curentDataContext.Books.Add(item); } // Добавляем новые данные в контекст даннных
            foreach (var item in loadedFile.Notes) { curentDataContext.Notes.Add(item); }
        }
    }
}
