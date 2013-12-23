using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDT //Sharp Data Table
{
    class Program
    {
        static void Main(string[] args)
        {
            Table t = new Table();
            t.doQuery("column FirstName,SecondName,Age,id,info"); //Creating and naming the columns
            t.doQuery("insert Liam¬Allan¬16¬!AI!¬Created this program."); //Inserting data and so on - BRB for a second
            t.doQuery("update Age id = \"1\" \"10\""); //Also able to update data.
            t.doQuery("insert Cameron¬Davies¬16¬!AI!¬He's a cool guy.");
            t.doQuery("insert Connor¬Cumming¬14¬!AI!¬Has a lot of Steam games.");
            t.doQuery("insert Adam¬Martin¬19¬!AI!¬He's an Aussie.");
            t.doQuery("insert Sophie¬Hagon¬16¬!AI!¬She's my wonderful and beautiful girlfriend.");
            String[] d;
            foreach (String data in t.doQuery("select FirstName % \"\"").ToString().Split('}')) {
                if (data != "")
                {
                    d = data.Split('¬');
                    Console.WriteLine(d[t.getArrayNumber("FirstName")] + " " + d[t.getArrayNumber("SecondName")] + ", is " + d[t.getArrayNumber("Age")] + " years old and is ID " + d[t.getArrayNumber("id")] + ". " + d[t.getArrayNumber("info")]);
                }
            }
            Console.ReadKey();
        }
    }
}
