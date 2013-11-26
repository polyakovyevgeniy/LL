using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LL.App_Code
{
    public class NotifyPropertyChangedImplements: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNotifyPropertyChanged(object sender, string proprtyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName: proprtyName));
        }
    }
}
