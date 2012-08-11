using RestSharp;
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
        }

        /// <summary>
        /// Set to true to use HTTPS instead of HTTP.
        /// </summary>
        public bool UseTLS
        {
            get { return _useTls; }
            set
            {
                _useTls = value;
                _client.BaseUrl = value ? _client.BaseUrl.Replace("http://", "https://") : _client.BaseUrl.Replace("https://", "http://");
            }
        }

        public bool DebugMode { get; set; }

        /// <summary>
        /// Timeout in miliseconds
        /// </summary>
        public int TimeOut { get; set; }

        public string DefaultGraphUri { get; set; }

        public Table Query(string sparql)
        {
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "text/html, application/xhtml+xml, */*");

            //Required
            request.AddParameter("query", sparql);

            //Optional
            if (!string.IsNullOrEmpty(DefaultGraphUri))
                request.AddParameter("default-graph-uri", DefaultGraphUri);

            request.AddParameter("format", "text/tab-separated-values");

            if (TimeOut != 0)
                request.AddParameter("timeout", TimeOut);

            request.AddParameter("debug", DebugMode ? "on" : "off");

            //Output
            RestResponse response = (RestResponse)_client.Execute(request);
            return CSVParser.Parse(response);
        }
    }
}