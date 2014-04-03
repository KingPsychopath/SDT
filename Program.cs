using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using SharpDataTable;

namespace SDT //Sharp Data Table
{
    class Program
    {
        public static string getShort(String inp, int len)
        {
            string outp = "";
            if (inp.Length > len)
            {
                outp = inp.Substring(0, len);
            }
            else if (inp.Length < len)
            {
                outp = inp;
                int a = 0;
                int b = len - inp.Length;
                while (a < b)
                {
                    outp += " ";
                    a++;
                }
            }
            else
            {
                outp = inp;
            }
            return outp;
        }
        
        static void Main(string[] args)
        {
            String[] arg;
            object o = null;
            SharpDataTable.Table Table = new SharpDataTable.Table();
            String inp = "";
            while (true)
            {
                Console.Clear();
                Console.Write("|");
                foreach (String column in Table.Columns)
                {
                    Console.Write(" " + getShort(column, 14) + " |");
                }
                Console.WriteLine();

                Console.Write("|");
                foreach (String column in Table.Columns)
                {
                    Console.Write(" " + getShort("--------------------------", 14) + " |");
                }
                Console.WriteLine();

                foreach (SharpDataTable.Row row in Table.Rows)
                {
                    Console.Write("|");
                    foreach (String value in row.RowValues)
                    {
                        Console.Write(" " + getShort(value, 14) + " |");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("> "); inp = Console.ReadLine(); arg = inp.Split(' ');
                switch (arg[0])
                {
                    case "import":
                        string[] lines = File.ReadAllLines(@"output.sdt");
                        Table.fromList(lines.ToList());
                        break;
                    case "export":
                        if (File.Exists(@"output.sdt")) File.Delete(@"output.sdt");
                        StreamWriter file = new StreamWriter("output.sdt", true);
                        foreach (String line in Table.toList()) {
                            file.WriteLine(line);
                        }
                        file.Close();
                        break;
                    default:
                        o = Table.doQuery(inp);
                        break;
                }
                if (o != null) Console.Title = o.ToString();
            }
        }
    }
}
