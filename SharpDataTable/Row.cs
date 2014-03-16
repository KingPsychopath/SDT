using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDataTable
{
    class Row
    {
        public List<String> RowValues = new List<String>();

        public void Add(String value)
        {
            RowValues.Add(value);
        }
    }
}
