using HtmlAgilityPack;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
//using B3WebScrapperToGoogleSheets.Classes;
using static System.Net.WebRequestMethods;
using System.Collections;
using System.Timers;
using Google.Apis.Sheets.v4;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using System.ComponentModel.Design;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Google.Apis.Sheets.v4.Data;
using B3WebScrapperToGoggleSheets.Classes.Config;
using B3WebScrapperToGoggleSheets.Classes;
using System.Text;

namespace B3WebScrapperToGoogleSheets
{
    public class Program
    {
        private static ConfigManager configs;
        private static Www wwwStocks;
        private static Www wwwFiis;
        private static StockManager stocks;
        private static FiiManager fiis;
        private static GoogleSheetsManager googleSheetsManager;

        private static IList<IList<object>> b3StockSheet = new List<IList<object>>();
        private static IList<IList<object>> b3FiiSheet = new List<IList<object>>();

        public static async Task Main(string[] args)
        {
            // Get the base directory of the project execution path string
            string baseDirectory = AppContext.BaseDirectory;
            string configFilePath = Path.Combine(baseDirectory, "config\\config.json");

            if (!System.IO.File.Exists(configFilePath))
            {
                Console.WriteLine($"[{DateTime.Now}] Configuration file missing: {configFilePath} ");
                return;
            }

            // Instance classes
            configs = new ConfigManager(configFilePath);
            wwwStocks = new Www();
            wwwFiis = new Www();
            stocks = new StockManager();
            fiis = new FiiManager();
            googleSheetsManager = new GoogleSheetsManager();

            // Authenticate to google API services
            authenticateGoogleAPIs(configs.GetCredentialsJsonFilePath());

            // Scrape stocks
            webScrapeStocks(stocks, wwwStocks, configs.GetStocksUrl(), configs.GetStockPortfolio(), configs.GetStockXpath(),false);
            
            // Stocks spreadsheet exists? If not create
            if (!googleSheetsManager.SheetExists(configs.GetSpreadsheetId(), configs.GetStockSheetName()))
            {
                googleSheetsManager.CreateSheet(configs.GetSpreadsheetId(), configs.GetStockSheetName());
                Console.WriteLine($"[{DateTime.Now}] Created sheet: {configs.GetStockSheetName()}");
            }

            // Read current stocks spreadsheet
            b3StockSheet = retrieveSpreadsheet(googleSheetsManager, configs.GetSpreadsheetId(), configs.GetStockSheetName());
            
            // Update and publish stocks spreadsheet
            publishStockSpreadsheet(googleSheetsManager, b3StockSheet, stocks, configs.GetSpreadsheetId(),configs.GetStockSheetName());

            // Scrape fiis
            webScrapeFiis(fiis, wwwFiis, configs.GetFiisUrl(), configs.GetFiiPortfolio(), configs.GetFiiXpath(), false);

            // Fiis spreadsheet exists? If not create
            if (!googleSheetsManager.SheetExists(configs.GetSpreadsheetId(), configs.GetFiiSheetName()))
            {
                googleSheetsManager.CreateSheet(configs.GetSpreadsheetId(), configs.GetFiiSheetName());
                Console.WriteLine($"[{DateTime.Now}] Created sheet: {configs.GetFiiSheetName()}");
            }

            // Read current fii spreadsheet
            b3FiiSheet = retrieveSpreadsheet(googleSheetsManager, configs.GetSpreadsheetId(), configs.GetFiiSheetName());
            
            // Update and publish fii spreadsheet
            publishFiiSpreadsheet(googleSheetsManager, b3FiiSheet, fiis, configs.GetSpreadsheetId(), configs.GetFiiSheetName());

            while (true)
            {
                Console.WriteLine("Press any key to exit..."); 
                Console.ReadKey(); 
                
                // Terminate the console application
                Environment.Exit(0);
                //return;
            }
        }

        #region Tasks
        private static void authenticateGoogleAPIs(string _credentialFilePath)
        {
            if (googleSheetsManager.Authenticate(_credentialFilePath))
            {
                Console.WriteLine($"[{DateTime.Now}] Authenticated @{googleSheetsManager.CredentialsInfo.UniverseDomain}" +
                    $"\r\n\t\t\t{{" +
                    $"\r\n\t\t\t  \"type\": \"{googleSheetsManager.CredentialsInfo.Type}\"" +
                    $"\r\n\t\t\t  \"project_id\": \"{googleSheetsManager.CredentialsInfo.ProjectId}\"" +
                    $"\r\n\t\t\t  \"client_id\": \"{googleSheetsManager.CredentialsInfo.ClientId}\"" +
                    $"\r\n\t\t\t  \"client_email\": \"{googleSheetsManager.CredentialsInfo.ClientEmail}\"" +
                    $"\r\n\t\t\t  \"private_key_id\": \"{googleSheetsManager.CredentialsInfo.PrivateKeyId}\"" +
                    $"\r\n\t\t\t  \"auth_uri\": \"{googleSheetsManager.CredentialsInfo.AuthenticationUrl}\"" +
                    $"\r\n\t\t\t}}");
            }
        }

        private static void publishStockSpreadsheet(GoogleSheetsManager googleSheetsManager, IList<IList<object>> worksheet, StockManager stockManager, string spreadsheetId, string range)
        {
            try
            {
                IList<IList<object>> newWorksheet = worksheet;

                // If null (empty) sheet, instance
                if (worksheet == null)
                {
                    newWorksheet = new List<IList<object>>();
                }

                // Copy or create header
                if (newWorksheet.Count > 0) 
                {
                    newWorksheet[0] = stocks.Stocks.First()?.GetHeader();
                }
                else
                {
                    newWorksheet.Add(stocks.Stocks.First()?.GetHeader());
                }

                // Update or add values
                foreach (var stock in stockManager.Stocks)
                {
                    // Skip the header row and find the index of the matching ticker
                    int index = newWorksheet.Skip(1).Select((row, i) => new { Row = row, Index = i + 1 })
                        .FirstOrDefault(x => x.Row[0].ToString().ToLower() == stock.Ticker.ToLower())?.Index ?? -1;

                    if (index != -1)
                    {
                        // Update the existing row
                        newWorksheet[index] = stock.ToIList();
                    }
                    else
                    {
                        // Add a new row
                        newWorksheet.Add(stock.ToIList());
                    }
                }

                googleSheetsManager.WriteSheet(spreadsheetId, range, newWorksheet);
                Console.WriteLine($"[{DateTime.Now}] Published to stock spreadsheet complete: {range}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"publish() catch ex: {ex.Message}");
            }
        }

        private static void publishFiiSpreadsheet(GoogleSheetsManager googleSheetsManager, IList<IList<object>> worksheet, FiiManager fiiManager, string spreadsheetId, string range)
        {
            try
            {
                IList<IList<object>> newWorksheet = worksheet;

                // If null (empty) sheet, instance
                if (worksheet == null)
                {
                    newWorksheet = new List<IList<object>>();
                }

                // Copy or create header
                if (newWorksheet.Count > 0)
                {
                    newWorksheet[0] = fiis.Fiis.First()?.GetHeader();
                }
                else
                {
                    newWorksheet.Add(fiis.Fiis.First()?.GetHeader());
                }

                // Update or add values
                foreach (var fii in fiiManager.Fiis)
                {
                    // Skip the header row and find the index of the matching ticker
                    int index = newWorksheet.Skip(1).Select((row, i) => new { Row = row, Index = i + 1 })
                        .FirstOrDefault(x => x.Row[0].ToString().ToLower() == fii.Ticker.ToLower())?.Index ?? -1;

                    if (index != -1)
                    {
                        // Update the existing row
                        newWorksheet[index] = fii.ToIList();
                    }
                    else
                    {
                        // Add a new row
                        newWorksheet.Add(fii.ToIList());
                    }
                }

                googleSheetsManager.WriteSheet(spreadsheetId, range, newWorksheet);
                Console.WriteLine($"[{DateTime.Now}] Published to fii spreadsheet complete: {range}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"publish() catch ex: {ex.Message}");
            }
        }

        private static IList<IList<object>> retrieveSpreadsheet(GoogleSheetsManager googleSheetsManager, string spreadsheetId, string range)
        {
            return googleSheetsManager.ReadSheet(spreadsheetId, range);
        }

        private static async Task webScrapeStocks(StockManager stockManager, Www www, string url, IList<string> portfolio, Dictionary<string, string> attributes, bool debug = false)
        {
            var successfullCount = 0;
            string info;

            Console.WriteLine($"[{DateTime.Now}] Start web scraping stock portifolio list...");

            // Flush values
            stockManager.Stocks.Clear();

            // Web scrapping logic
            foreach (var ticker in portfolio)
            {
                if (www.GetHttp($"{url}{ticker.ToLower()}"))
                {
                    Console.WriteLine($"[{DateTime.Now}] GET successful {url}{ticker}");
                    successfullCount++;

                    foreach (var attribute in attributes)
                    {
                        info = www.GetXpathInfo(attribute.Value);
                        stockManager.UpdateStock(ticker, attribute.Key, info);
                    }
                    if (debug) Console.WriteLine($"{stockManager.ConvertToJson(ticker)}");
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now}] GET fail {url}/ ");
                }
            }
            if (debug) Console.WriteLine($"[{DateTime.Now}] GET Success:[{successfullCount}] Fail:[{portfolio.Count - successfullCount}]");
        }

        private static async Task webScrapeFiis(FiiManager fiiManager, Www www, string url, IList<string> portfolio, Dictionary<string, string> attributes, bool debug = false)
        {
            var successfullCount = 0;
            string info;

            Console.WriteLine($"[{DateTime.Now}] Start web scraping fii portifolio list...");

            // Flush values
            fiiManager.Fiis.Clear();

            // Web scrapping logic
            foreach (var ticker in portfolio)
            {
                if (www.GetHttp($"{url}{ticker.ToLower()}"))
                {
                    Console.WriteLine($"[{DateTime.Now}] GET successful {url}{ticker}");
                    successfullCount++;

                    foreach (var attribute in attributes)
                    {
                        info = www.GetXpathInfo(attribute.Value);
                        fiiManager.UpdateFii(ticker, attribute.Key, info);
                    }
                    if (debug) Console.WriteLine($"{fiiManager.ConvertToJson(ticker)}");
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now}] GET fail {url}/ ");
                }
            }
            if (debug) 
                Console.WriteLine($"[{DateTime.Now}] GET Success:[{successfullCount}] Fail:[{portfolio.Count - successfullCount}]");
        }
        #endregion
    }
}