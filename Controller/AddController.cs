using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Model;

namespace Inventory.Controller
{
    class AddController
    {
        public bool AddMedicine(MedRowFormat med) {
            try
            {
                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Sugandha\\Documents\\Medicine_Inventory\\Database11.accdb"))
                {
                    conn.Open();
                    string addStmt = "Insert into medicine (Company, Medicine_Name, Expiration) values( pCompany,pMedName,pExpDt)";
                    OleDbParameter paramCompany = new OleDbParameter("pCompany", OleDbType.VarChar);
                    paramCompany.Value = med.company;
                    OleDbParameter paramMedName = new OleDbParameter("pMedName", OleDbType.VarChar);
                    paramMedName.Value = med.medName;
                    OleDbParameter paramExpDate = new OleDbParameter("pExpDt", OleDbType.Date);
                    paramExpDate.Value = med.expDt;
                    OleDbCommand command = new OleDbCommand(addStmt, conn);
                    command.Parameters.Add(paramCompany);
                    command.Parameters.Add(paramMedName);
                    command.Parameters.Add(paramExpDate);
                     
                    int insertCount = command.ExecuteNonQuery();
                    if (insertCount > 0)
                        return true;
                }
            }
            catch(Exception e) {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
