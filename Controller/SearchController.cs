using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Controller
{
    class SearchController
    {
        public enum compareOP {
            EQ,
            GT,
            LT
        }
        public DataTable refreshMedGrid(string search="All", string MedName ="" , string expDt= null, compareOP op = compareOP.EQ)
        {
            DataTable tb = new DataTable();
            using (OleDbConnection oledb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Sugandha\\Documents\\Medicine_Inventory\\Database11.accdb"))
            {
                try
                {
                    oledb.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = oledb;
                    string sql = "select * from medicine";
                    if (search.ToUpper() == "SEARCH") {
                        string whereClause = "";
                        if(!String.IsNullOrEmpty(MedName)) {
                            whereClause = " where UCASE(Medicine.[Medicine_Name]) like '%" + MedName.ToUpper() +"%'";
                        }

                        if (!String.IsNullOrEmpty(expDt))
                        {
                            DateTime dt = DateTime.Parse(expDt);
                            string oper = "=";
                            if (op == compareOP.GT)
                                oper = ">=";
                            else
                            if (op == compareOP.LT)
                                oper = "<=";
                            if (!String.IsNullOrEmpty(whereClause))  //where DateValue(Medicine.[Expiration]) >= DateValue('20-Mar-2017');
                                whereClause = whereClause + " and  DateValue(Medicine.[Expiration]) " + oper + "DateValue('" + dt.ToString("dd-MMM-yyyy") + "')"; //" pExpDt ";
                            else
                                whereClause = whereClause + " where  DateValue(Medicine.[Expiration]) " + oper + "DateValue('" + dt.ToString("dd-MMM-yyyy") + "')";
                            OleDbParameter param = new OleDbParameter("pExpDt", OleDbType.Date);
                            param.Value = dt;
                            command.Parameters.Add(param);
                        }
                        sql = sql + whereClause; 
                    }
                    command.CommandText = sql;
                    OleDbDataAdapter adaptor = new OleDbDataAdapter(command);
                    DataSet ds = new DataSet();
                    adaptor.Fill(ds);

                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            DateTime dt = DateTime.Parse(row["Expiration"].ToString());
                            int medId = -1;
                            Int32.TryParse(row["ID"].ToString(), out medId);
                        }
                    }
                    tb = ds.Tables[0];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return tb;
        }
    }
}
