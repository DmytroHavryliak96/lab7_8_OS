using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherService
{
    class FileWatcher
    {
        private FileSystemWatcher watcher;

        public FileWatcher()
        {
            watcher = new FileSystemWatcher(PathLocation());
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | 
                NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.Error += new ErrorEventHandler(OnError);
            watcher.EnableRaisingEvents = true;
            ChangeLogger.Log(String.Format("The FileWatcherService Start At Date:{0}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss")));

        }

        private string PathLocation()
        {
            string value = String.Empty;
            try
            {
                value = ConfigurationManager.AppSettings["WatchPath"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            WatcherChangeTypes wct = e.ChangeType;
            ChangeLogger.Log(String.Format("File {0}: Path:{1}, Name:{2}, Date:{3}", wct.ToString(),  e.FullPath, e.Name, DateTime.Now.ToString("MM/dd/yy HH:mm:ss")));

        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            ChangeLogger.Log(String.Format("File Renamed: From Path1:{0}, Name1:{1} To Path2:{2}, Name2:{3}, Date:{4}", e.OldFullPath, e.OldName, e.FullPath, e.Name, DateTime.Now.ToString("MM/dd/yy HH:mm:ss")));

        }

        private static void OnError(object source, ErrorEventArgs e)
        {
            ChangeLogger.Log("The FileSystemWatcher has detected an error");
            if (e.GetException().GetType() == typeof(InternalBufferOverflowException))
            {
                ChangeLogger.Log("The file system watcher experienced an internal buffer overflow: " + e.GetException().Message);
            }
        }

    }
}
