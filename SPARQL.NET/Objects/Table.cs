using System.Collections.Generic;

namespace SPARQLNET.Objects
{
    public class Table
    {
        public Table()
        {
            Columns = new List<string>();
            Rows = new List<Row>();
        }

        public List<string> Columns { get; set; }
        public List<Row> Rows { get; set; }
    }

    public class Row : List<string>
    {
        private const char Separator = '\t';

        public Row(string rowLine)
        {
            string[] rowCollection = rowLine.Split(Separator);
            foreach (string row in rowCollection)
            {
                Add(row.TrimStart('"').TrimEnd('"'));
            }
        }
    }
}
