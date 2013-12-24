using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SDT
{
    class Table
    {
        public String cd = ""; //column details Name¬0}name¬1}
        public String t = ""; //Rows - Starting from 0+ DATA¬MOREDATA}
        public int rows = 0;

        public String cleanString(String value)
        {
            return value.Replace("~", "").Replace("¬", "").Replace("}", "");
        }

        public void importFromFile(String fileloc)
        {
            if (File.Exists(fileloc))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(fileloc))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            doQuery(line.Replace("~", "¬"));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void exportToFile(String fileloc)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileloc);
            String[] d;
            String sb = "column ";
            if (cd.Trim() != "")
            {
                foreach (String col in cd.Split('}'))
                {
                    d = col.Split('¬');
                    sb += d[0] + ",";
                }
                sb = sb.TrimEnd(',');
                file.WriteLine(sb);
            }
            if (!t.Equals(""))
            {
                foreach (String row in t.Split('}'))
                {
                    if (!row.Equals(""))
                    {
                        sb = "insert ";
                        d = row.Split('¬');
                        foreach (String value in d)
                        {
                            sb += value.Replace("~", "") + "~";
                        }
                        sb = sb.TrimEnd('~');
                        file.WriteLine(sb);
                    }
                }
            }
            file.Close();
        }

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

        public bool isTrue(String op, String a, String b)
        {
            bool outp = false;
            switch (op)
            {
                case "%":
                    if (a.Contains(b))
                    {
                        outp = true;
                    }
                    break;
                case "=":
                    if (a.Equals(b))
                    {
                        outp = true;
                    }
                    break;
                case "!":
                    if (Double.Parse(a) != Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case ">":
                    if (Double.Parse(a) > Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case "<":
                    if (Double.Parse(a) < Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case ">=":
                    if (Double.Parse(a) >= Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case "<=":
                    if (Double.Parse(a) <= Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
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
                            if (isTrue(words[2], d[getArrayNumber(words[1])], speech[1]))
                            {
                                outps += row + "}";
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
                            if (isTrue(words[2], d[getArrayNumber(words[1])], speech[1]))
                            {
                                t = t.Replace(row + "}", "");
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
                    String sb = "";
                    foreach (String word in words)
                    {
                        if (a != 0)
                        {
                            w = word.Replace("!AI!", rows.ToString());
                            foreach (String value in w.Split('¬'))
                            {
                                sb += cleanString(value) + "¬";
                            }
                            str += sb + " ";
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
                            if (isTrue(words[2], d[getArrayNumber(words[1])], speech[1]))
                            {
                                outpi++;
                            }
                        }
                        catch { }
                    }
                    return outpi;
                case "update":
                    String ns = "";
                    if (speech[1] != "")
                    {
                        try
                        {
                            foreach (String row in t.Split('}'))
                            {
                                d = row.Split('¬');
                                if (isTrue(words[3], d[getArrayNumber(words[2])], speech[1]))
                                {
                                    ns = row.Replace(d[getArrayNumber(words[1])] + "¬", speech[3] + "¬");
                                    ns = cleanString(ns);
                                    t = t.Replace(row, ns);
                                }
                            }
                            return true;
                        }
                        catch { return false; }
                    }
                    else
                    {
                        return false;
                    }
                case "column":
                    l = 0;
                    foreach (String col in words[1].Split(','))
                    {
                        l = cd.Split('}').Length - 1;
                        cd += cleanString(col) + "¬" + l + "}";
                    }
                    return true;
                case "nc":
                    l = 0;
                    foreach (String col in words[1].Split(','))
                    {
                        l = cd.Split('}').Length - 1;
                        cd += cleanString(col) + "¬" + l + "}";
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
