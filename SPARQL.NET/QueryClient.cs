using RestSharp;
using RestSharp.Deserializers;
using SPARQLNET.Enums;
using SPARQLNET.Misc;
using SPARQLNET.Objects;

namespace SPARQLNET
{
	public class QueryClient
	{
		private static RestClient _client = new RestClient();
		private bool _useTls;

		public QueryClient(string endpoint)
		{
			_client.BaseUrl = endpoint;
			_client.Proxy = null;
		}

		/// <summary>
		/// Set to true to use HTTPS instead of HTTP.
		/// </summary>
		public bool UseTLS
		{
			get { return _useTls; }
			set
			{
				if (value)
					_client.BaseUrl = _client.BaseUrl.Replace("http://", "https://");
				else
					_client.BaseUrl = _client.BaseUrl.Replace("https://", "http://");

				_useTls = value;
			}
		}

		/// <summary>
		/// Gets or sets the format to use.
		/// </summary>
		public Format Format { get; set; }

		public bool DebugMode { get; set; }

		/// <summary>
		/// Timeout in miliseconds
		/// </summary>
		public int TimeOut { get; set; }

		public string DefaultGraphUri { get; set; }

		public QueryResult Query(string sparql)
		{

			RestRequest request = new RestRequest(Method.GET);
			request.AddHeader("Accept", "text/html, application/xhtml+xml, */*");

			//Required
			request.AddParameter("query", sparql);

			//Optional
			if (!string.IsNullOrEmpty(DefaultGraphUri))
				request.AddParameter("default-graph-uri", DefaultGraphUri);

			request.AddParameter("format", Format.GetStringValue());

			if (TimeOut != 0)
				request.AddParameter("timeout", TimeOut);

			request.AddParameter("debug", DebugMode ? "on" : "off");

			//Output
			RestResponse response = (RestResponse)_client.Execute(request);

			IDeserializer deserializer;

			switch (Format)
			{
				case Format.XML:
					deserializer = new XmlAttributeDeserializer();
					break;
				case Format.JSON:
					deserializer = new JsonDeserializer();
					break;
				default:
					deserializer = new XmlAttributeDeserializer();
					break;
			}

			QueryResult results = deserializer.Deserialize<QueryResult>(response);
			return results;
		}
	}
}