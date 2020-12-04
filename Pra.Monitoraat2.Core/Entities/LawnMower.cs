using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Monitoraat2.Core.Entities
{
    public class LawnMower
    {
        private string id;
        private string brand;
        private string series;
        private bool hasEngine;
        private int bladeWidth;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Brand
        {
            get { return brand; }
            set 
            {
                if (value == "")
                    throw new Exception("Merk kan niet leeg zijn!");
                if (value.Length > 50)
                    value = value.Substring(0, 50);
                brand = value; 
            }
        }

        public string Series
        {
            get { return series; }
            set 
            {
                if (value == "")
                    throw new Exception("Serie kan niet leeg zijn!");
                if (value.Length > 50)
                    value = value.Substring(0, 50); 
                series = value; 
            }
        }

        public bool HasEngine
        {
            get { return hasEngine; }
            set { hasEngine = value; }
        }

        public int BladeWidth
        {
            get { return bladeWidth; }
            set 
            { 
                bladeWidth = value; 
            }
        }
        private decimal salesPrice;

        public decimal SalesPrice
        {
            get { return salesPrice; }
            set { salesPrice = value; }
        }
        public LawnMower()
        { }
        public LawnMower(string id, string brand, string series, bool hasEngine, int bladeWidth, decimal salesPrice)
        {
            this.id = id;
            Brand = brand;
            Series = series;
            this.hasEngine = hasEngine;
            this.bladeWidth = bladeWidth;
            this.salesPrice = salesPrice;
        }
        public override string ToString()
        {
            return $"{brand} - {series}";
        }




    }
}
