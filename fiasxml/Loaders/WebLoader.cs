using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;

namespace Fias.Loaders
{
    public class WebLoader
    {
        public Uri FullUri = new Uri("http://fias.nalog.ru/Public/Downloads/Actual/fias_dbf.rar"); 
        public Uri DeltaUri=new Uri("http://fias.nalog.ru/Public/Downloads/Actual/fias_delta_dbf.rar");
        public Uri Actual = new Uri("http://fias.nalog.ru/Public/Downloads/Actual/VerDate.txt");
        protected WebClient webClient = new WebClient();
    public WebLoader()
        { }
        public Nullable<DateTime> Load(bool fullBase, DirectoryInfo destinationDir, DateTime LastLoad)
        {
            var actual = webClient.DownloadString(Actual);
            if (!string.IsNullOrEmpty(actual))
            {
                if (!destinationDir.Exists) destinationDir.Create();
                var actdate = DateTime.Parse(actual);
                if (actdate > LastLoad)
                {
                    if (Load(fullBase, destinationDir).Result)
                    {
                        return actdate;
                    }
                    else
                        return null;
                }
                else
                    return null;
                }
            else
                return null;
        }
        public async Task<bool> Load(bool fullBase,DirectoryInfo destinationDir)
        {
            string tempPath = Path.Combine(destinationDir.FullName, "fias_dbf_"+((fullBase) ? "":"delta_") + DateTime.Now.ToShortDateString() + ".rar");
            try
            {
                webClient.DownloadFile((fullBase ? FullUri : DeltaUri), tempPath);
                using (RarArchive archive = RarArchive.Open(tempPath))
                {
                    foreach (RarArchiveEntry item in archive.Entries)
                    {
                        item. WriteToDirectory(destinationDir.FullName);
                    }
                }
                    File.Delete(tempPath);
                    return true;
            }
            catch (Exception e)
            {
                Logger.Logger.Error(e.Message);
                return false; 
            }
        }

        public IEnumerable<dynamic> LoadStreams(bool fullBase, DirectoryInfo destinationDir)
        {
            string tempPath = Path.Combine(destinationDir.FullName, "fias_dbf_" + ((fullBase) ? "" : "delta_") + DateTime.Now.ToShortDateString() + ".rar");
            try
            {
                webClient.DownloadFile((fullBase ? FullUri : DeltaUri), tempPath);
                using (RarArchive archive = RarArchive.Open(tempPath))
                {
                      return  archive.Entries.Select(s=> new { Name = s.LinkTarget, stream = s.OpenEntryStream() }).ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Logger.Error(e.Message);
                return null;
            }
        }
    }
}
