using SevenZipExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<bool> Load(bool fullBase, DirectoryInfo destinationDir, DateTime LastLoad)
        {
            var actual = webClient.DownloadString(Actual);
            if (!string.IsNullOrEmpty(actual))
            {
                if (!destinationDir.Exists) destinationDir.Create();
                var actdate = DateTime.Parse(actual);
                if (actdate > LastLoad)
                    return await Load(fullBase, destinationDir);
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<bool> Load(bool fullBase,DirectoryInfo destinationDir)
        {
            string tempPath = Path.Combine(System.IO.Path.GetTempPath(), "fias_dbf_"+((fullBase) ? "delta_":"") + DateTime.Now.ToShortDateString() + ".rar");
            try
            {
                webClient.DownloadFile((fullBase ? FullUri : DeltaUri), tempPath);
                using (ArchiveFile archiveFile = new ArchiveFile(tempPath))
                {
                    archiveFile.Extract(destinationDir.FullName);
                    File.Delete(tempPath);
                    return true;
                }
            }
            catch (Exception e)
            { return false; }
        }
    }
}
