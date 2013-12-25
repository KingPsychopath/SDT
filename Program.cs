using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void drawTable(Table t, int size, String query)
        {
            /*Console.WriteLine("----");
            Console.WriteLine(t.cd);
            Console.WriteLine("----");
            Console.WriteLine(t.t);
            Console.WriteLine("----");*/
            String[] d;
            String cn = ""; //Column name
            foreach (String col in t.cd.Split('}'))
            {
                Console.Write(" | ");
                if (col != "")
                {
                    d = col.Split('¬');
                    cn = d[0];
                    Console.Write(getShort(d[0], size));
                }
            }
            Console.WriteLine();
            if (query == "")
            {
                query = "select " + cn + " % \"\"";
            }
            foreach (String data in t.doQuery(query).ToString().Split('}'))
            {
                Console.WriteLine();
                if (data != "")
                {
                    d = data.Split('¬');
                    foreach (String col in d)
                    {
                        Console.Write(" | ");
                        Console.Write(getShort(col, size));
                    }
                    //Console.WriteLine(d[t.getArrayNumber("FirstName")] + " " + d[t.getArrayNumber("SecondName")] + ", is " + d[t.getArrayNumber("Age")] + " years old and is ID " + d[t.getArrayNumber("id")] + ". " + d[t.getArrayNumber("info")]);
                }
            }
        }
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);
            Console.SetBufferSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);
            Console.SetWindowPosition(0, 0);
            Console.Title = "Sharp Data Table";
            Table t = new Table();
            t.importFromFile("table.txt");
            String inp = "";
            object outp;
            while (inp != "EXIT")
            {
                Console.Clear();
                if (inp.StartsWith("select"))
                {
                    drawTable(t, 11, inp);
                }
                else
                {
                    drawTable(t, 11, "");
                }
                Console.Write("\nQuery> "); inp = Console.ReadLine();
                if (!inp.StartsWith("select"))
                {
                    outp = t.doQuery(inp);
                    Console.WriteLine(outp.ToString());
                    Console.ReadKey();
                }
            }
            t.exportToFile("table.txt");
        }
    }
}
