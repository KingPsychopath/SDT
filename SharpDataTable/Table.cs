using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDataTable
{
    class Table
    {
        public List<Row> Rows = new List<Row>();
        public List<String> Columns = new List<String>();

        public int getIndex(String ColumnValue)
        {
            int outp = 0;
            foreach (String name in Columns)
            {
                if (name == ColumnValue) return outp;
                outp++;
            }
            return -1;
        }

        //To add: Exporting and importing table

        public List<String> toList()
        {
            List<String> outp = new List<String>();
            String line = "column ";
            foreach (String column in Columns)
            {
                line += column + ",";
            }
            outp.Add(line.Trim(','));
            foreach (Row row in Rows)
            {
                line = "insert \"";
                foreach (String value in row.RowValues)
                {
                    line += value + "¬";
                }
                line += "\"";
                outp.Add(line);
            }
            return outp;
        }

        public void fromList(List<String> List)
        {
            foreach (String query in List)
            {
                doQuery(query);
            }
        }

        public object doQuery(String Query)
        {
            String[] args = Query.Split(' ');
            String[] speech = Query.Split('"');
            int dindex;
            switch (args[0].ToLower())
            {
                case "column": //column Value,Value,value
                    foreach (String name in args[1].Split(','))
                    {
                        if (name != "") Columns.Add(name);
                        foreach (Row row in Rows)
                        {
                            row.Add("NULL");
                        }
                    }
                    return true;
                case "insert": //insert "VALUE¬VALUE¬VALUE"
                    Row r = new Row();
                    foreach (String value in speech[1].Split('¬'))
                    {
                        if (value != "") r.Add(value);
                    }
                    Rows.Add(r);
                    return true;
                case "delete": //delete "Column name" =% "Value" //DELETE ALL ROWS WITH COLUMN CONTAINS/EQUALING VALUE
                    int outp = 0; //deleted amount
                    dindex = getIndex(speech[1]);
                    List<Row> remove = new List<Row>();
                    if (dindex >= 0)
                    {
                        foreach (Row row in Rows)
                        {
                            switch (speech[2].Trim())
                            {
                                case "=":
                                    if (row.RowValues[dindex] == speech[3])
                                    {
                                        remove.Add(row);
                                        outp++;
                                    }
                                    break;
                                case "%":
                                    if (row.RowValues[dindex].Contains(speech[3]))
                                    {
                                        remove.Add(row);
                                        outp++;
                                    }
                                    break;
                            }
                        }
                        foreach (Row row in remove)
                        {
                            Rows.Remove(row);
                        }
                        return outp;
                    }
                    else
                    {
                        Console.WriteLine(dindex.ToString());
                        return false;
                    }
                               //             1               3             4          5            7
                case "update": //update "COLUMNTOCHANGE" "COLUMNTOCHECK" OPERATOR "VALUETOMATCH" "NEWVALUE"
                    dindex = getIndex(speech[3]);
                    int cindex = getIndex(speech[1]);
                    if (dindex >= 0)
                    {
                        foreach (Row row in Rows)
                        {
                            switch (speech[4].Trim())
                            {
                                case "=":
                                    if (row.RowValues[dindex] == speech[5])
                                    {
                                        row.RowValues[cindex] = speech[7];
                                    }
                                    break;
                                case "%":
                                    if (row.RowValues[dindex].Contains(speech[5]))
                                    {
                                        row.RowValues[cindex] = speech[7];
                                    }
                                    break;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(dindex.ToString());
                        return false;
                    }
                case "get": //get "column name" = "value" "column to return"
                    dindex = getIndex(speech[1]);
                    if (dindex >= 0)
                    {
                        foreach (Row row in Rows)
                        {
                            switch (speech[2].Trim())
                            {
                                case "=":
                                    if (row.RowValues[dindex] == speech[3])
                                    {
                                        return row.RowValues[getIndex(speech[5])];
                                    }
                                    break;
                                case "%":
                                    if (row.RowValues[dindex].Contains(speech[3]))
                                    {
                                        return row.RowValues[getIndex(speech[6])];
                                    }
                                    break;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        Console.WriteLine(dindex.ToString());
                        return false;
                    }
                case "drop":
                    Columns.Clear();
                    Rows.Clear();
                    return true;
            }
            return false;
        }
    }
}
