using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3WebScrapperToGoggleSheets.Classes
{
    public class FiiManager
    {
        public List<Fii> Fiis = new List<Fii>();

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(Fiis, Formatting.Indented);
        }

        public string ConvertToJson(string _ticker)
        {
            var ticker = _ticker.ToUpper();

            var fii = Fiis.FirstOrDefault(x => x.Ticker == ticker);
            if (fii != null)
            {
                return JsonConvert.SerializeObject(fii, Formatting.Indented);
            }
            else
            {
                return ($"Ticker '{ticker}' not found in the list.");
            }
        }

        public void UpdateFii(string _ticker, string _propertyName, object _newValue)
        {
            var ticker = _ticker.ToUpper();
            var propertyName = _propertyName.ToLower();
            object newValue = _newValue;

            // Ticker existant in Fiis? Add if not
            if (!Fiis.Any(x => x.Ticker == ticker))
            {
                Fiis.Add(new Fii(ticker));
            }

            var fii = Fiis.FirstOrDefault(x => x.Ticker == ticker);
            string parseString;
            string[] parts;
            decimal value;

            if (newValue == null || string.IsNullOrEmpty(newValue.ToString())) return;

            if (fii != null)// && newValue != null && !string.IsNullOrEmpty(newValue.ToString()))
            {
                switch (propertyName)
                {
                    case "name":
                        fii.Name = newValue.ToString().Replace("Ações ", "");
                        break;
                    case "ticker":
                        break;
                    case "segment":
                        fii.Segment = newValue.ToString();
                        break;
                    case "anbima":
                        fii.Anbima = newValue.ToString();
                        break;
                    case "management":
                        fii.Management = newValue.ToString();
                        break;
                    case "price":
                        parseString = newValue.ToString().Replace("R$ ", "").Trim().Replace(",", ".");
                        if (decimal.TryParse(parseString, out value))
                        {
                            fii.Price = value;
                        }
                        break;
                    case "dy30d":
                        fii.DY30D = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "dy12m":
                        fii.DY12M = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "dy5y":
                        fii.DY5Y = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "payoutpershare12m":
                        parseString = newValue.ToString().Replace("R$ ", "").Trim().Replace(",", ".");
                        if (decimal.TryParse(parseString, out value))
                        {
                            fii.PayoutPerShare12m = value;
                        }
                        break;
                    case "payoutlast":
                        fii.PayoutLast = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "vp":
                        parseString = newValue.ToString().Replace("R$ ", "").Trim().Replace(",", ".");
                        if (decimal.TryParse(parseString, out value))
                        {
                            fii.VP = value;
                        }
                        break;
                    case "pvp":
                        fii.PVP = Convert.ToDecimal(newValue.ToString().Replace(",", "."));
                        break;
                    case "vacancy":
                        fii.Vacancy = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "variation1d":
                        fii.Variation1D = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "variation30d":
                        fii.Variation30D = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "variation12m":
                        fii.Variation12M = Convert.ToDecimal(newValue.ToString().Replace("%", "").Trim().Replace(",", ".")) / 100;
                        break;
                    case "dailyliquidity":
                        parts = newValue.ToString().Replace(",", ".").Split(' ');
                        value = decimal.Parse(parts.First());

                        switch (parts.Last().ToUpper())
                        {
                            case "K":
                                value *= 1000;
                                break;
                            case "M":
                                value *= 1000000;
                                break;
                            case "B":
                                value *= 1000000000;
                                break;
                            case "T":
                                value *= 1000000000000;
                                break;
                            default: break;
                        }
                        fii.DailyLiquidity = value;
                        break;
                    case "quotas":
                        try
                        {
                            fii.Quotas = Convert.ToDecimal(newValue.ToString().Replace(".", ""));
                        }
                        catch
                        {
                        }
                            break;
                    case "holders":
                        try
                        {
                            fii.Holders = Convert.ToDecimal(newValue.ToString().Replace(".", ""));
                        }
                        catch
                        {
                        }

                            break;
                    case "networth":
                        try
                        {
                            parts = newValue.ToString().Replace(",", ".").Split(' ');
                            value = decimal.Parse(parts.First());

                            switch (parts.Last().ToUpper())
                            {
                                case "K":
                                    value *= 1000;
                                    break;
                                case "M":
                                    value *= 1000000;
                                    break;
                                case "B":
                                    value *= 1000000000;
                                    break;
                                case "T":
                                    value *= 1000000000000;
                                    break;
                                default: break;
                            }
                            fii.NetWorth = value;
                        }
                        catch 
                        { 
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
                Console.WriteLine($"Ticker '{_ticker}' not found in the list, or attribute null.");
            }
        }
    }
}

