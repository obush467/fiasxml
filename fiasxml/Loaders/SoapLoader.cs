using SoapHttpClient;
using SoapHttpClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fias.Loaders
{
    public class SoapLoader
    {
        public async Task CallNasaAsync()
        {
            var soapClient = new SoapClient();
            var ns = new XElement("qqqq","<?xml version=\"1.0\" encoding=\"utf - 8\"?>< soap:Envelope xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" xmlns: xsd = \"http://www.w3.org/2001/XMLSchema\" xmlns: soap = \"http://schemas.xmlsoap.org/soap/envelope/\">< soap:Body >< GetAllDownloadFileInfo xmlns = \"https://fias.nalog.ru/WebServices/Public/DownloadService.asmx\" /></ soap:Body >          </ soap:Envelope > ");
            var result =
                await soapClient.PostAsync(
                    new Uri("https://fias.nalog.ru/WebServices/Public/DownloadService.asmx/GetAllDownloadFileInfo"),
                    SoapVersion.Soap11,
                    body: ns);
            result.StatusCode.ToString();
        }
    }
}
