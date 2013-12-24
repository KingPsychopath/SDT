using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDT
{
    class Table
    {
        public String cd = ""; //column details Name¬0}name¬1}
        public String t = ""; //Rows - Starting from 0+ DATA¬MOREDATA}
        public int rows = 0;

        public int getArrayNumber(String name)
        {
            int outp = -1;
            String[] d;
            foreach (String data in cd.Split('}'))
            {
                d = data.Split('¬');
                if (data.StartsWith(name + "¬"))
                {
                    outp = int.Parse(d[1]);
                }
            }
            return outp;
        }

        public object doQuery(String query)
        {
            String[] words = query.Split(' ');
            String[] speech = query.Split('"');
            String[] d;
            int l = 0;
            switch (words[0].ToLower())
            {
                case "select": //SELECT name (=, %) "Liam" || Select COLUMN (=, %) "VALUE"
                    String outps = "";
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            switch (words[2])
                            {
                                case "%":
                                    if (d[getArrayNumber(words[1])].Contains(speech[1]))
                                    {
                                        outps += row + "}";
                                    }
                                    break;
                                case "=":
                                    if (d[getArrayNumber(words[1])] == speech[1])
                                    {
                                        outps += row + "}";
                                    }
                                    break;
                            }
                            
                        }
                        catch { }
                    }
                    return outps.Trim();
                case "delete":
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            switch (words[2])
                            {
                                case "%":
                                    if (d[getArrayNumber(words[1])].Contains(speech[1]))
                                    {
                                        t = t.Replace(row + "}", "");
                                    }
                                    break;
                                case "=":
                                    if (d[getArrayNumber(words[1])] == speech[1])
                                    {
                                        t = t.Replace(row + "}", "");
                                    }
                                    break;
                            }
                            
                        }
                        catch { }
                    }
                    return true;
                case "insert": //insert Liam¬Allan¬1997
                    int a = 0; //Array number
                    String str = "";
                    String w;
                    rows++;
                    foreach (String word in words) {
                        if (a != 0)
                        {
                            w = word.Replace("!AI!", rows.ToString());
                            str += w + " ";
                        }
                        a++;
                    }
                    str = str.Trim();
                    if (!str.EndsWith("¬")) { str += "¬"; }
                    t += str + "}";
                    return true;
                case "row":
                    int outpi = 0;
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            switch (words[2])
                            {
                                case "%":
                                    if (d[getArrayNumber(words[1])].Contains(speech[1]))
                                    {
                                        outpi++;
                                    }
                                    break;
                                case "=":
                                    if (d[getArrayNumber(words[1])] == speech[1])
                                    {
                                        outpi++;
                                    }
                                    break;
                            }
                        }
                        catch { }
                    }
                    return outpi;
                case "update":
                    String ns = "";
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            switch (words[3])
                            {
                                case "%":
                                    if (d[getArrayNumber(words[2])].Contains(speech[1])) //update age FirstName = "Liam" "10"
                                    {
                                        t = t.Replace(d[getArrayNumber(words[1])] + "¬", speech[3] + "¬");
                                    }
                                    break;
                                case "=":
                                    if (d[getArrayNumber(words[2])].Equals(speech[1]))
                                    {
                                        ns = row.Replace(d[getArrayNumber(words[1])] + "¬", speech[3] + "¬");
                                        t = t.Replace(row, ns);
                                    }
                                    break;
                            }
                            
                        }
                        catch { }
                    }
                    return true;
                case "column":
                    l = 0;
                    foreach (String col in words[1].Split(','))
                    {
                        l = cd.Split('}').Length - 1;
                        cd += col + "¬" + l + "}";
                    }
                    return true;
                case "nc":
                    l = 0;
                    foreach (String col in words[1].Split(','))
                    {
                        l = cd.Split('}').Length - 1;
                        cd += col + "¬" + l + "}";
                        if (t != "")
                        {
                            foreach (String row in t.Split('}'))
                            {
                                if (row != "")
                                {
                                    t = t.Replace(row, row + "NULL¬");
                                }
                            }
                        }
                    }
                    return true;
                case "empty":
                    t = "";
                    cd = "";
                    return true;
                default:
                    return false;
            }
        }
    }
}
