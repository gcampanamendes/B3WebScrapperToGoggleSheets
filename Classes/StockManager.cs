using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3WebScrapperToGoggleSheets.Classes
{
    public class StockManager
    {
        public List<Stock> Stocks = new List<Stock>();

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(Stocks, Formatting.Indented);
        }

        public string ConvertToJson(string _ticker)
        {
            var ticker = _ticker.ToUpper();

            var stock = Stocks.FirstOrDefault(x => x.Ticker == ticker);
            if (stock != null)
            {
                return JsonConvert.SerializeObject(stock, Formatting.Indented);
            }
            else
            {
                return ($"Ticker '{ticker}' not found in the list.");
            }

        }

        public void UpdateStock(string _ticker, string _propertyName, object _newValue)
        {
            var ticker = _ticker.ToUpper();
            var propertyName = _propertyName.ToLower();
            object newValue = _newValue;

            // Ticker existant in Stocks? Add if not
            if (!Stocks.Any(x => x.Ticker == ticker))
            {
                Stocks.Add(new Stock(ticker));
            }

            var stock = Stocks.FirstOrDefault(x => x.Ticker == ticker);
            string parseString;
            decimal value;

            if (stock != null)
            {
                switch (propertyName)
                {
                    case "name":
                        stock.Name = newValue.ToString().Replace("Ações ", "");
                        break;
                    case "ticker":
                        break;
                    case "sector":
                        stock.Sector = newValue.ToString();
                        break;
                    case "price":
                        parseString = newValue.ToString().Replace("R$ ", "").Trim().Replace(",", ".");
                        if (decimal.TryParse(parseString, out value))
                        {
                            stock.Price = value;
                        }
                        break;
                    case "pl":
                        stock.PL = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "pvp":
                        stock.PVP = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "dy":
                        stock.DY = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "dy5y":
                        stock.DY5Y = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "lpa":
                        stock.LPA = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "vpa":
                        stock.VPA = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "dpa":
                        stock.DPA = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "variation12m":
                        stock.Variation12M = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "variation1m":
                        stock.Variation1M = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "variation7d":
                        stock.Variation7D = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "variation1day":
                        stock.Variation1Day = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "tagalong":
                        parseString = newValue.ToString().Replace("%", "");
                        if (decimal.TryParse(parseString.Trim().Replace(",", "."), out value))
                        {
                            stock.TagAlong = value / 100;
                        }
                        break;
                    case "roe":
                        parseString = newValue.ToString().Replace("%", "");
                        if (decimal.TryParse(parseString.Trim().Replace(",", "."), out value))
                        {
                            stock.ROE = value / 100;
                        }
                        break;
                    case "roic":
                        parseString = newValue.ToString().Replace("%", "");
                        if (decimal.TryParse(parseString.Trim().Replace(",", "."), out value))
                        {
                            stock.ROIC = value / 100;
                        }
                        break;
                    default:
                        Console.WriteLine($"Property '{propertyName}' not found.");
                        break;
                }
                //Console.WriteLine($"Ticker '{_ticker}' updated: {propertyName} = {newValue}");
            }
            else
            {
                Console.WriteLine($"Ticker '{_ticker}' not found in the list.");
            }
        }
    }
}
