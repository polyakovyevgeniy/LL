using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LL.Models;

using LL.Helpers.Loaded;


namespace LL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///  Модель данных
        /// </summary>
        public BooksModel BooksModel { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //BooksModel = new BooksModel();
        }

        public void LoadApplicationFile()
        {
            try
            {
                var ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.Filter = "LL(*.ll)|*.bin";
                ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if(ofd.ShowDialog() == true)
                {
                    //Открываем файл с расширением .bin
                    using (var fs = new System.IO.FileStream(ofd.FileName, System.IO.FileMode.Open))
                        
                        LoadApplication.LoadFile((BooksModel)new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(fs), BooksModel);                   
                }               
                
            }
            catch (Exception ex)
            {
                
            }
        }

        public bool SaveApplicationFile()
        {
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "LL(*.ll)|*.bin";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    using (var fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create))
                    {
                        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(fs, BooksModel);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }                
            }
            return false;
        }
    }
}
