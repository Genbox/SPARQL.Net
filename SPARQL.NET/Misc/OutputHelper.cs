using System;
using System.Text;
using SPARQLNET.Enums;
using SPARQLNET.Objects;

namespace SPARQLNET.Misc
{
    public static class OutputHelper
    {
        public static string GetOutput(this Table table, OutputFormat format, int tableWidth = 77, string separator = ",")
        {
            switch (format)
            {
                case OutputFormat.Table:
                    return GetTextOutput(table, tableWidth);
                case OutputFormat.HTML:
                    return GetHTLMOutput(table, tableWidth);
                case OutputFormat.DataList:
                    return GetDataListOutput(table, separator);
                default:
                    throw new ArgumentOutOfRangeException("format");
            }
        }

        private static string GetDataListOutput(Table table, string separator)
        {
            if (table != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (Row row in table.Rows)
                {
                    sb.AppendLine(string.Join(separator, row));
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        private static string GetTextOutput(Table table, int tableWidth)
        {
            if (table != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(PrintLine(tableWidth));
                sb.AppendLine(PrintRow(tableWidth, table.Columns.ToArray()));
                sb.AppendLine(PrintLine(tableWidth));

                foreach (Row row in table.Rows)
                {
                    sb.AppendLine(PrintRow(tableWidth, row.ToArray()));
                }

                sb.AppendLine(PrintLine(tableWidth));

                return sb.ToString();
            }

            return string.Empty;
        }

        private static string GetHTLMOutput(Table table, int tableWidth)
        {
            if (table != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<table width=\"" + tableWidth + "\">");

                sb.AppendLine("<tr>");
                string[] columns = table.Columns.ToArray();
                string width = (tableWidth - columns.Length) / columns.Length + "px";

                foreach (string column in columns)
                {
                    sb.AppendLine("    <td width=\"" + width + "\">" + column + "</td>");
                }

                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");

                foreach (Row row in table.Rows)
                {
                    columns = row.ToArray();
                    width = (tableWidth - columns.Length) / columns.Length + "px";

                    foreach (string column in columns)
                    {
                        sb.AppendLine("    <td width=\"" + width + "\">" + column + "</td>");
                    }

                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table>");

                return sb.ToString();
            }

            return string.Empty;
        }

        private static string PrintLine(int tableWidth)
        {
            return new string('-', tableWidth);
        }

        private static string PrintRow(int tableWidth, params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            return row;
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
                return new string(' ', width);

            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}
