using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3WebScrapperToGoggleSheets.Classes
{
    class Www
    {
        #region private
        private HttpClient httpClient;
        private HtmlDocument htmlDocument;
        private string html;
        #endregion

        public Www()
        {
            htmlDocument = new HtmlDocument();
            httpClient = new HttpClient();
        }

        #region Methods
        public bool GetHttp(string url)
        {
            try
            {
                html = httpClient.GetStringAsync(url).Result;
                htmlDocument.LoadHtml(html);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetHttp({url}) catch ex: {ex.Message}");
                return false;
            }
        }

        public string GetXpathInfo(string xpath)
        {
            try
            {
                if (!string.IsNullOrEmpty(xpath))
                {
                    HtmlNodeCollection nodes = htmlDocument?.DocumentNode.SelectNodes(xpath);
                    if (!nodes.Equals(null))
                    {
                        if (nodes.Count > 0)
                        {
                            return nodes.First()?.InnerText.Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetXpathInfo({xpath}) catch ex: {ex.Message}");
            }
            return string.Empty;
        }
        #endregion
    }
}