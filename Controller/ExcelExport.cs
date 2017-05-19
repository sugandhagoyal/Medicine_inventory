using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace Inventory.Controller
{
    class ExcelExport
    {
        public void ExportFile(string path, string fileName, DataTable resDt) {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workBook = app.Workbooks.Add(path); 
            workBook.Worksheets.
        }
    }
}
