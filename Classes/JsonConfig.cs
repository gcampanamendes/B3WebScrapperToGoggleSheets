using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3WebScrapperToGoggleSheets.Classes.Config
{
    public class JsonConfig
    {
        public Web Web { get; set; }
        public Sheets Sheets { get; set; }
        public Portfolios Portfolios { get; set; }
    }

    public class Web
    {
        public WebAttribute Fii { get; set; }
        public WebAttribute Stock { get; set; }
    }

    public class WebAttribute
    {
        public Dictionary<string, string> Xpath { get; set; }
        public string Url { get; set; }
    }

    public class Sheets
    {
        public string CredentialsJsonFilePath { get; set; }
        public string SpreadsheetId { get; set; }
        public SheetNames SheetNames { get; set; }
    }

    public class SheetNames
    {
        public string Fii { get; set; }
        public string Stock { get; set; }
    }

    public class Portfolios
    {
        public List<string> Fii { get; set; }
        public List<string> Stock { get; set; }
    }
}
