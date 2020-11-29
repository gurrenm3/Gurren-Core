using Gurren_Core.Utils;
using MelonLoader;
using System;
using System.Net;

namespace Gurren_Core.Web
{
    /// <summary>
    /// Class for reading text from a URL
    /// </summary>
    public class WebReader
    {
        /// <summary>
        /// Downloads string of text from the URL. Needs to be run on a thread if using UI, otherwise the UI will freeze
        /// </summary>
        public string ReadText_FromURL(string url)
        {
            Guard.ThrowIfArgumentIsNull(url, "url", "Can't read text from url. Url argument is null");
            var webClient = CreateWebClient();

            string downloadedText = "";
            string lastExeption = "";
            const int maxTries = 5;

            for (int i = 0; i <= maxTries; i++)
            {
                try
                {
                    downloadedText = webClient.DownloadString(url);
                    if (!String.IsNullOrEmpty(downloadedText))
                        break;
                }
                catch (Exception e)
                {
                    if (e.Message == lastExeption)
                        continue;

                    string message = e.Message + ".";
                    if (i < maxTries)
                        message += " Trying again...";

                    MelonLogger.Log(message);
                    lastExeption = e.Message;
                }
            }
            return downloadedText;
        }

        private WebClient CreateWebClient()
        {
            WebClient client = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.Headers.Add("user-agent", " Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
            return client;
        }
    }
}
