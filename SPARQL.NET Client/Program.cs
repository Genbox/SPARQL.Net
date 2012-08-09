using System;
using SPARQLNET;
using SPARQLNET.Enums;
using SPARQLNET.Objects;
using SPARQLNET.Misc;

namespace SPARQLNETClient
{
	class Program
	{
		static void Main(string[] args)
		{
			//Set the endpoint
		    QueryClient queryClient = new QueryClient("http://dbpedia.org/sparql");

			//Create a query that finds people who were born in Berlin before 1900
			QueryResult result = queryClient.Query("PREFIX : <http://dbpedia.org/resource/>" +
												"PREFIX dbo: <http://dbpedia.org/ontology/>" +
												"SELECT ?name ?birth ?death ?person WHERE {" +
												"     ?person dbo:birthPlace :Berlin ." +
												"     ?person dbo:birthDate ?birth ." +
												"     ?person foaf:name ?name ." +
												"     ?person dbo:deathDate ?death ." +
												"     FILTER (?birth < \"1900-01-01\"^^xsd:date) ." +
												"} ORDER BY ?birth LIMIT 10");

			Console.WriteLine(result.GetOutput(OutputFormat.CSV));

			Console.WriteLine("Press a key to continue");
			Console.ReadLine();
		}
	}
}
