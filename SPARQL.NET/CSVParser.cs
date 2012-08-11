using System.IO;
using RestSharp;
using SPARQLNET.Objects;

namespace SPARQLNET
{
    public static class CSVParser
    {
        private const char Separator = '\t';

        public static Table Parse(IRestResponse response)
        {
            Table table = new Table();
            StringReader reader = new StringReader(response.Content);
            string readLine = reader.ReadLine();

            if (readLine != null)
            {
                string[] collection = readLine.Split(Separator);
                foreach (string column in collection)
                {
                    table.Columns.Add(column.TrimStart('"').TrimEnd('"'));
                }
            }

            string line = reader.ReadLine();

            while (!string.IsNullOrEmpty(line))
            {
                Row row = new Row(line);
                table.Rows.Add(row);
                line = reader.ReadLine();
            }

            return table;
        }
    }
}