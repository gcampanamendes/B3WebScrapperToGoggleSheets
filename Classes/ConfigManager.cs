using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace B3WebScrapperToGoggleSheets.Classes.Config
{
    public class ConfigManager
    {
        public JsonConfig config { get; }
        
        public ConfigManager(string _filePath)
        {
            filePath = _filePath;

            var file = File.ReadAllText(filePath);
            var json = JsonConvert.DeserializeObject<Dictionary<string, JsonConfig>>(file);
            config = json["config"];
        }

        public string filePath { get; set; }

        public Dictionary<string, string> GetFiiXpath()
        {
            return config.Web.Fii.Xpath;
        }

        public Dictionary<string, string> GetStockXpath()
        {
            return config.Web.Stock.Xpath;
        }

        public string GetFiisUrl()
        {
            return config.Web.Fii.Url;
        }

        public string GetStocksUrl()
        {
            return config.Web.Stock.Url;
        }

        public string GetCredentialsJsonFilePath()
        {
            return config.Sheets.CredentialsJsonFilePath;
        }

        public string GetSpreadsheetId()
        {
            return config.Sheets.SpreadsheetId;
        }

        public string GetFiiSheetName()
        {
            return config.Sheets.SheetNames.Fii;
        }

        public string GetStockSheetName()
        {
            return config.Sheets.SheetNames.Stock;
        }

        public List<string> GetFiiPortfolio() 
        {
            return config.Portfolios.Fii; 
        }

        public List<string> GetStockPortfolio()
        {
            return config.Portfolios.Stock;
        }

        public void SetFiiXpath(Dictionary<string, string> fiiXpath)
        {
            config.Web.Fii.Xpath = fiiXpath;
            SaveConfig();
        }

        public void SetStockXpath(Dictionary<string, string> stockXpath)
        {
            config.Web.Stock.Xpath = stockXpath;
            SaveConfig();
        }

        public void SetFiisUrl(string fiisUrl)
        {
            config.Web.Fii.Url = fiisUrl;
            SaveConfig();
        }

        public void SetStocksUrl(string stocksUrl)
        {
            config.Web.Stock.Url = stocksUrl;
            SaveConfig();
        }

        public void SetCredentialsJsonFilePath(string credentialsJsonFilePath)
        {
            config.Sheets.CredentialsJsonFilePath = credentialsJsonFilePath;
            SaveConfig();
        }

        public void SetSpreadsheetId(string spreadsheetId)
        {
            config.Sheets.SpreadsheetId = spreadsheetId;
            SaveConfig();
        }

        public void SetFiiSheetName(string fiiSheetName)
        {
            config.Sheets.SheetNames.Fii = fiiSheetName;
            SaveConfig();
        }

        public void SetStockSheetName(string stockSheetName)
        {
            config.Sheets.SheetNames.Stock = stockSheetName;
            SaveConfig();
        }

        public void SetFiiPortfolio(List<string> fiiPortfolio) 
        { 
            config.Portfolios.Fii = fiiPortfolio; 
            SaveConfig(); 
        }

        public void SetStockPortfolio(List<string> stockPortfolio) 
        { 
            config.Portfolios.Stock = stockPortfolio; 
            SaveConfig(); 
        }

        private void SaveConfig()
        { 
            File.WriteAllText(filePath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}