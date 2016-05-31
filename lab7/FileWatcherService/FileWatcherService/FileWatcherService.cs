using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.ServiceProcess;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherService
{
    public partial class FileWatcherService : ServiceBase
    {     
        public FileWatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                FileWatcher f = new FileWatcher();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        protected override void OnStop()
        {
            ChangeLogger.Log(String.Format("The FileWatcherService Stop At Date:{0}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss")));
        }

    }
}
