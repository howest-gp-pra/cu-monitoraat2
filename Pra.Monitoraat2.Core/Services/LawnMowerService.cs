using System;
using System.Collections.Generic;
using System.Text;
using Pra.Monitoraat2.Core.Entities;
using System.Data;
using System.Linq;

namespace Pra.Monitoraat2.Core.Services
{
    public class LawnMowerService
    {
        private List<LawnMower> lawnMowers;

        public List<LawnMower> LawnMowers
        {
            get { return lawnMowers; }
        }

        public LawnMowerService()
        {
            lawnMowers = new List<LawnMower>();
            GetAllLawnMowers();
        }
        private void GetAllLawnMowers()
        {
            string sql;
            sql = "select * from lawnmower order by brand, series";
            DataTable dataTable = DBConnector.ExecuteSelect(sql);
            LawnMower lawnMower;
            foreach (DataRow rw in dataTable.Rows)
            {
                lawnMower = new LawnMower
                {
                    ID = rw["id"].ToString(),
                    Brand = rw["brand"].ToString(),
                    Series = rw["series"].ToString(),
                    HasEngine = Convert.ToBoolean(rw["hasengine"]),
                    BladeWidth = int.Parse(rw["bladewidth"].ToString()),
                    SalesPrice = decimal.Parse(rw["salesprice"].ToString())
                };
                lawnMowers.Add(lawnMower);
            }
        }
        public bool AddLawnMower(LawnMower lawnMower)
        {
            // oppassen met decimals !
            string salesPrice = lawnMower.SalesPrice.ToString();
            salesPrice = salesPrice.Replace(",", ".");
            // bool naar bit
            string hasEngine = "0";
            if (lawnMower.HasEngine) hasEngine = "1";

            lawnMower.ID = Guid.NewGuid().ToString();

            string sql;
            sql = "insert into lawnmower(id, brand, series, hasengine, bladewidth, salesprice) values(";
            sql += "'" + lawnMower.ID + "' , ";
            sql += "'" + Helper.HandleQuotes(lawnMower.Brand) + "' , ";
            sql += "'" + Helper.HandleQuotes(lawnMower.Series) + "' , ";
            sql += hasEngine + " , ";
            sql += lawnMower.BladeWidth.ToString() + " , ";
            sql += salesPrice + ")";
            if(DBConnector.ExecuteCommand(sql))
            {
                lawnMowers.Add(lawnMower);
                lawnMowers = lawnMowers.OrderBy(lm => lm.Brand).ThenBy(lm => lm.Series).ToList();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateLawnMower(LawnMower lawnMower)
        {
            // oppassen met decimals ! op SQL server : decimaalsymbool = .
            string salesPrice = lawnMower.SalesPrice.ToString();
            salesPrice = salesPrice.Replace(",", ".");
            // bool naar bit
            string hasEngine = "0";
            if (lawnMower.HasEngine) hasEngine = "1";

            string sql;
            sql = "update lawnmower set ";
            sql += " brand = '" + Helper.HandleQuotes(lawnMower.Brand) + "' , ";
            sql += " series = '" + Helper.HandleQuotes(lawnMower.Series) + "' , ";
            sql += " hasengine = " + hasEngine + " , ";
            sql += " bladewidth = " + lawnMower.BladeWidth + " , ";
            sql += " salesprice = " + salesPrice;
            sql += " where id = '" + lawnMower.ID + "' ";
            if (DBConnector.ExecuteCommand(sql))
            {
                lawnMowers = lawnMowers.OrderBy(lm => lm.Brand).ThenBy(lm => lm.Series).ToList();
                return true;
            }
            else
                return false;
        }
        public bool DeleteLawnMower(LawnMower lawnMower)
        {
            string sql;
            sql = "delete from lawnmower where id = '" + lawnMower.ID + "' ";
            if (DBConnector.ExecuteCommand(sql))
            {
                lawnMowers.Remove(lawnMower);
                return true;
            }
            else
                return false;
        }
    }
}
