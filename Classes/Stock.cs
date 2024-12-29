using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3WebScrapperToGoggleSheets.Classes
{
    public class Stock
    {
        private string ticker;
        private string name;
        private string sector;
        private decimal price;
        private decimal pl;
        private decimal pvp;
        private decimal dy;
        private decimal dy5y;
        private decimal lpa;
        private decimal vpa;
        private decimal dpa;
        private decimal variation12m;
        private decimal variation1m;
        private decimal variation7d;
        private decimal variation1day;
        private decimal tagAlong;
        private decimal roe;
        private decimal roic;

        public string Name
        {
            get { return name; }
            set
            {
                //if (string.IsNullOrWhiteSpace(value))
                //throw new ArgumentException("Name cannot be null or empty.");
                name = value;
            }
        }

        public string Ticker
        {
            get { return ticker; }
            set
            {
                //if (string.IsNullOrWhiteSpace(value))
                //throw new ArgumentException("Ticker cannot be null or empty.");
                ticker = value;
            }
        }

        public string Sector
        {
            get { return sector; }
            set
            {
                //if (string.IsNullOrWhiteSpace(value))
                //throw new ArgumentException("Sector cannot be null or empty.");
                sector = value;
            }
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

        public decimal PL
        {
            get { return pl; }
            set
            {
                pl = value;
            }
        }

        public decimal PVP
        {
            get { return pvp; }
            set
            {
                pvp = value;
            }
        }

        public decimal DY
        {
            get { return dy; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("DY cannot be negative.");
                dy = value;
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

        public decimal LPA
        {
            get { return lpa; }
            set
            {
                lpa = value;
            }
        }

        public decimal VPA
        {
            get { return vpa; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("VPA cannot be negative.");
                vpa = value;
            }
        }

        public decimal DPA
        {
            get { return dpa; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("DPA cannot be negative.");
                dpa = value;
            }
        }

        public decimal Variation12M
        {
            get { return variation12m; }
            set => variation12m = value;
        }

        public decimal Variation1M
        {
            get { return variation1m; }
            set => variation1m = value;
        }

        public decimal Variation7D
        {
            get { return variation7d; }
            set => variation7d = value;
        }

        public decimal Variation1Day
        {
            get { return variation1day; }
            set => variation1day = value;
        }

        public decimal TagAlong
        {
            get { return tagAlong; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("TagAlong cannot be negative.");
                tagAlong = value;
            }
        }

        public decimal ROE
        {
            get { return roe; }
            set
            {
                roe = value;
            }
        }

        public decimal ROIC
        {
            get { return roic; }
            set
            {
                roic = value;
            }
        }
        public Stock()
        {
            Ticker = "";
            Name = "";
            Sector = "";
        }
        public Stock(string _ticker)
        {
            Ticker = _ticker;
        }
        public Stock(string _name, string _ticker)
        {
            Name = _name;
            Ticker = _ticker;
        }
        public Stock(string _name, string _ticker, string _sector, decimal _price, decimal _pl, decimal _pvp, decimal _dy, decimal _dy5y, decimal _lpa, decimal _vpa, decimal _dpa, decimal _variation12m, decimal _variation1m, decimal _variation7d, decimal _variation1day, decimal _tagAlong, decimal _roe, decimal _roic)
        {
            Name = _name;
            Ticker = _ticker;
            Sector = _sector;
            Price = _price;
            PL = _pl;
            PVP = _pvp;
            DY = _dy;
            DY5Y = _dy5y;
            LPA = _lpa;
            VPA = _vpa;
            DPA = _dpa;
            Variation12M = _variation12m;
            Variation1M = _variation1m;
            Variation7D = _variation7d;
            Variation1Day = _variation1day;
            TagAlong = _tagAlong;
            ROE = _roe;
            ROIC = _roic;
        }

        public IList<object> ToIList()
        {
            return new List<object>
            {
                Ticker,
                Name,
                Sector,
                Price,
                PL,
                PVP,
                DY,
                DY5Y,
                LPA,
                VPA,
                DPA,
                Variation12M,
                Variation1M,
                Variation7D,
                Variation1Day,
                TagAlong,
                ROE,
                ROIC
            };
        }

        public IList<object> GetHeader()
        {
            return new List<object>
            {
                "Ticker",
                "Name",
                "Sector",
                "Price",
                "PL",
                "PVP",
                "DY",
                "DY5Y",
                "LPA",
                "VPA",
                "DPA",
                "Variation12M",
                "Variation1M",
                "Variation7D",
                "Variation1Day",
                "TagAlong",
                "ROE",
                "ROIC"
            };
        }
    }
}
