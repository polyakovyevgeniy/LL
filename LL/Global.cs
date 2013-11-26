using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LL.ViewModels;

namespace LL
{
    public class Global
    {
        public static Lazy<MainWindowViewModel>  MainWindowViewModel { get; set; }
        public static Lazy<AddBookViewModel> AddBookViewModelLazy { get; set; }
        public static Lazy<AddNoteViewModel> AddNoteViewModel { get; set; }
        public static Lazy<NoteViewerViewModel> NoteViewerViewModel { get; set; }

        static Global()
        {
            MainWindowViewModel = new Lazy<MainWindowViewModel>();
            AddBookViewModelLazy = new Lazy<AddBookViewModel>();
            AddNoteViewModel = new Lazy<AddNoteViewModel>();
            NoteViewerViewModel = new Lazy<NoteViewerViewModel>();
        }
    }
}
