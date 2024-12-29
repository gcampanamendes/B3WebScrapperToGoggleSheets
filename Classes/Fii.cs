using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3WebScrapperToGoggleSheets.Classes
{
    public class Fii
    {
        private string ticker;
        private string name;
        private string segment;
        private string anbima;
        private string management;
        private decimal price;
        private decimal dy30d;
        private decimal dy12m;
        private decimal dy5y;
        private decimal payoutPerShare12m;
        private decimal payoutLast;
        private decimal vp;
        private decimal pvp;
        private decimal vacancy;
        private decimal variation1d;
        private decimal variation30d;
        private decimal variation12m;
        private decimal dailyLiquidity;
        private decimal quotas;
        private decimal holders;
        private decimal netWorth;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Ticker
        {
            get { return ticker; }
            set { ticker = value; }
        }

        public string Segment
        {
            get { return segment; }
            set { segment = value; }
        }

        public string Anbima
        {
            get { return anbima; }
            set { anbima = value; }
        }

        public string Management
        {
            get { return management; }
            set { management = value; }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative.");
                price = value;
            }
        }

        public decimal DY30D
        {
            get { return dy30d; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("DY30D cannot be negative.");
                dy30d = value;
            }
        }

        public decimal DY12M
        {
            get { return dy12m; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("DY12M cannot be negative.");
                dy12m = value;
            }
        }

        public decimal DY5Y
        {
            get { return dy5y; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("DY5Y cannot be negative.");
                dy5y = value;
            }
        }

        public decimal PayoutPerShare12m
        {
            get { return payoutPerShare12m; }
            set { payoutPerShare12m = value; }
        }

        public decimal PayoutLast
        {
            get { return payoutLast; }
            set { payoutLast = value; }
        }

        public decimal VP
        {
            get { return vp; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("VP cannot be negative.");
                vp = value;
            }
        }

        public decimal PVP
        {
            get { return pvp; }
            set { pvp = value; }
        }

        public decimal Vacancy
        {
            get { return vacancy; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Vacancy cannot be negative.");
                vacancy = value;
            }
        }

        public decimal Variation1D
        {
            get { return variation1d; }
            set { variation1d = value; }
        }

        public decimal Variation30D
        {
            get { return variation30d; }
            set { variation30d = value; }
        }

        public decimal Variation12M
        {
            get { return variation12m; }
            set { variation12m = value; }
        }

        public decimal DailyLiquidity
        {
            get { return dailyLiquidity; }
            set { dailyLiquidity = value; }
        }

        public decimal Quotas
        {
            get { return quotas; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quotas cannot be negative.");
                quotas = value;
            }
        }

        public decimal Holders
        {
            get { return holders; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Holders cannot be negative.");
                holders = value;
            }
        }

        public decimal NetWorth
        {
            get { return netWorth; }
            set { netWorth = value; }
        }

        public Fii()
        {
            Ticker = "";
            Name = "";
            Segment = "";
        }

        public Fii(string _ticker)
        {
            Ticker = _ticker;
        }

        public Fii(string _name, string _ticker)
        {
            Name = _name;
            Ticker = _ticker;
        }

        public Fii(string _name, string _ticker, string _segment, string _anbima, string _management, decimal _price, decimal _dy30d, decimal _dy12m, decimal _dy5y, decimal _payoutPerShare12m, decimal _payoutLast, decimal _vp, decimal _pvp, decimal _vacancy, decimal _variation1d, decimal _variation30d, decimal _variation12m, decimal _dailyLiquidity, decimal _quotas, decimal _holders, decimal _netWorth)
        {
            Name = _name;
            Ticker = _ticker;
            Segment = _segment;
            Anbima = _anbima;
            Management = _management;
            Price = _price;
            DY30D = _dy30d;
            DY12M = _dy12m;
            DY5Y = _dy5y;
            PayoutPerShare12m = _payoutPerShare12m;
            PayoutLast = _payoutLast;
            VP = _vp;
            PVP = _pvp;
            Vacancy = _vacancy;
            Variation1D = _variation1d;
            Variation30D = _variation30d;
            Variation12M = _variation12m;
            DailyLiquidity = _dailyLiquidity;
            Quotas = _quotas;
            Holders = _holders;
            NetWorth = _netWorth;
        }

        public IList<object> ToIList()
        {
            return new List<object>
            {
                Ticker,
                Name,
                Segment,
                Anbima,
                Management,
                Price,
                DY30D,
                DY12M,
                DY5Y,
                PayoutPerShare12m,
                PayoutLast,
                VP,
                PVP,
                Vacancy,
                Variation1D,
                Variation30D,
                Variation12M,
                DailyLiquidity,
                Quotas,
                Holders,
                NetWorth,
            };
        }

        public IList<object> GetHeader()
        {
            return new List<object>
            {
                "Ticker",
                "Name",
                "Segment",
                "Anbima",
                "Management",
                "Price",
                "DY30D",
                "DY12M",
                "DY5Y",
                "PayoutPerShare12m",
                "PayoutLast",
                "VP",
                "PVP",
                "Vacancy",
                "Variation1D",
                "Variation30D",
                "Variation12M",
                "DailyLiquidity",
                "Quotas",
                "Holders",
                "NetWorth",
            };
        }
    }
}

