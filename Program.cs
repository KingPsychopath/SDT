using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void drawTable(Table t)
        {
            Console.WriteLine("----");
            Console.WriteLine(t.cd);
            Console.WriteLine("----");
            Console.WriteLine(t.t);
            Console.WriteLine("----");
            String[] d;
            String cn = ""; //Column name
            foreach (String col in t.cd.Split('}'))
            {
                Console.Write(" | ");
                if (col != "")
                {
                    d = col.Split('¬');
                    cn = d[0];
                    Console.Write(getShort(d[0], 10));
                }
            }
            Console.WriteLine();
            foreach (String data in t.doQuery("select " + cn + " % \"\"").ToString().Split('}'))
            {
                Console.WriteLine();
                if (data != "")
                {
                    d = data.Split('¬');
                    foreach (String col in d)
                    {
                        Console.Write(" | ");
                        Console.Write(getShort(col, 10));
                    }
                    
                    //Console.WriteLine(d[t.getArrayNumber("FirstName")] + " " + d[t.getArrayNumber("SecondName")] + ", is " + d[t.getArrayNumber("Age")] + " years old and is ID " + d[t.getArrayNumber("id")] + ". " + d[t.getArrayNumber("info")]);
                }
            }
        }
        
        static void Main(string[] args)
        {
            Table t = new Table();
            t.doQuery("column FirstName,SecondName,Age,id"); //Creating and naming the columns
            t.doQuery("insert Liam¬Allan¬16¬!AI!"); //Inserting data and so on - BRB for a second
            t.doQuery("insert Cameron¬Davies¬16¬!AI!");
            t.doQuery("insert Connor¬Cumming¬14¬!AI!");
            t.doQuery("update Age id = \"1\" \"10\""); //Also able to update data.
            String inp = "";
            object outp;
            while (true)
            {
                Console.Clear();
                drawTable(t);
                Console.Write("\nQuery> "); inp = Console.ReadLine();
                outp = t.doQuery(inp);
                Console.WriteLine(outp.ToString());
                Console.ReadKey();
            }
            /*t.doQuery("column FirstName,SecondName,Age,id"); //Creating and naming the columns
            t.doQuery("insert Liam¬Allan¬16¬!AI!"); //Inserting data and so on - BRB for a second
            t.doQuery("update Age id = \"1\" \"10\""); //Also able to update data.
            t.doQuery("insert Cameron¬Davies¬16¬!AI!¬He's a cool guy.");
            t.doQuery("insert Connor¬Cumming¬14¬!AI!¬Has a lot of Steam games.");
            t.doQuery("insert Adam¬Martin¬19¬!AI!¬He's an Aussie.");
            t.doQuery("insert Sophie¬Hagon¬16¬!AI!¬She's my wonderful and beautiful girlfriend.");
            drawTable(t);
            Console.ReadKey();*/
        }
    }
}
