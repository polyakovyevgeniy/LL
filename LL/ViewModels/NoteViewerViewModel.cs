using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LL.ViewModels
{
    public class NoteViewerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private string viewerContent;

        public string ViewerContent
        {
            get { return viewerContent; }
            set
            {
                if (ViewerContent == value) return;
                viewerContent = value;
                OnNotifyPropertyChanged("ViewerContent");
            }
        }
    }
}
