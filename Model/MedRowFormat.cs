using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    public class MedRowFormat
    {
        public string company;
        public string medName;
        public DateTime expDt;
        int id;

        public MedRowFormat(string company, string medicine, DateTime dt, int id)
        {
            this.company = company;
            this.medName = medicine;
            this.expDt = dt;
            this.id = id;
        }
    }
}
