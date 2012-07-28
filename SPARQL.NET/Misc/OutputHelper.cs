using System;
using System.Collections.Generic;
using System.Text;
using SPARQLNET.Enums;
using SPARQLNET.Objects;
using System.Linq;

namespace SPARQLNET.Misc
{
	public static class OutputHelper
	{

		public static string GetOutput(this QueryResult res, OutputFormat format, int tableWidth = 77, string separator = ",")
		{
			switch (format)
			{
				case OutputFormat.Text:
					return GetTextOutput(res, tableWidth);
				case OutputFormat.HTML:
					return GetHTLMOutput(res, tableWidth);
				case OutputFormat.CSV:
					return GetCSVOutput(res, separator);
				case OutputFormat.DataList:
					return GetDataListOutput(res, separator);
				default:
					throw new ArgumentOutOfRangeException("format");
			}
		}

		private static string GetDataListOutput(QueryResult res, string separator)
		{
			if (res != null)
			{
				StringBuilder sb = new StringBuilder();

				if (res.Results != null)
				{
					foreach (Result result in res.Results.Results)
					{
						foreach (Binding binding in result.Results)
						{
							sb.AppendLine(binding.Name + separator + binding.Value);
						}
					}
				}

				return sb.ToString();
			}

			return string.Empty;
		}

		private static string GetCSVOutput(QueryResult res, string separator)
		{
			if (res != null)
			{
				StringBuilder sb = new StringBuilder();

				if (res.Head != null)
				{
					string[] columns = res.Head.Variables.Select(c => c.Name).ToArray();
					sb.AppendLine(string.Join(separator, columns));
				}

				if (res.Results != null)
				{
					foreach (Result result in res.Results.Results)
					{
						List<string> columns = result.Results.Select(bindings => bindings.Value).ToList();
						sb.AppendLine(string.Join(separator, columns));
					}
				}

				return sb.ToString();
			}

			return string.Empty;
		}

		private static string GetTextOutput(QueryResult res, int tableWidth)
		{
			if (res != null)
			{
				StringBuilder sb = new StringBuilder();

				if (res.Head != null)
				{
					sb.AppendLine(PrintLine(tableWidth));
					string[] columns = res.Head.Variables.Select(c => c.Name).ToArray();
					sb.AppendLine(PrintRow(tableWidth, columns));
				}

				sb.AppendLine(PrintLine(tableWidth));

				if (res.Results != null)
				{
					foreach (Result result in res.Results.Results)
					{
						List<string> columns = result.Results.Select(bindings => bindings.Value).ToList();
						sb.AppendLine(PrintRow(tableWidth, columns.ToArray()));
					}
				}

				sb.AppendLine(PrintLine(tableWidth));

				return sb.ToString();
			}

			return string.Empty;
		}

		private static string GetHTLMOutput(QueryResult res, int tableWidth)
		{
			if (res != null)
			{
				StringBuilder sb = new StringBuilder();

				sb.AppendLine("<table width=\"" + tableWidth + "\">");

				if (res.Head != null)
				{
					sb.AppendLine("<tr>");
					string[] columns = res.Head.Variables.Select(c => c.Name).ToArray();
					string width = (tableWidth - columns.Length) / columns.Length + "px";

					foreach (string column in columns)
					{
						sb.AppendLine("    <td width=\"" + width + "\">" + column + "</td>");
					}

					sb.AppendLine("</tr>");
				}

				if (res.Results != null)
				{
					sb.AppendLine("<tr>");

					foreach (Result result in res.Results.Results)
					{
						string[] columns = result.Results.Select(bindings => bindings.Value).ToArray();
						string width = (tableWidth - columns.Length) / columns.Length + "px";

						foreach (string column in columns)
						{
							sb.AppendLine("    <td width=\"" + width + "\">" + column + "</td>");
						}

						sb.AppendLine("</tr>");
					}
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
