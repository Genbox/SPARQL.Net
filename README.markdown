# SPARQL.NET - A simple read-only SPARQL API

### Features

* Based on RestSharp (http://restsharp.org) to deserialize XML into objects
* Can send SPARQL queries to a SPARQL enabled endpoints
* Parses the output from the endpoint into objects
* HTML table output
* Text table output

### Examples

```csharp
static void Main(string[] args)
{
	//Set the endpoint
	QueryClient client = new QueryClient("http://dbpedia.org/sparql");

	//Create a query that finds people who were born in Berlin before 1900
	QueryResult result = client.Query("PREFIX : <http://dbpedia.org/resource/>" +
										"PREFIX dbo: <http://dbpedia.org/ontology/>" +
										"SELECT ?name ?birth ?death ?person WHERE {" +
										"     ?person dbo:birthPlace :Berlin ." +
										"     ?person dbo:birthDate ?birth ." +
										"     ?person foaf:name ?name ." +
										"     ?person dbo:deathDate ?death ." +
										"     FILTER (?birth < \"1900-01-01\"^^xsd:date) ." +
										"} ORDER BY ?birth LIMIT 10");

	Console.WriteLine(result.GetOutput(OutputFormat.Text, 77));
}
```

Output:
```
-----------------------------------------------------------------------------
|       name       |      birth       |      death       |      person      |
-----------------------------------------------------------------------------
|Dorothea of Bra...|    1420-02-09    |    1491-01-19    |http://dbpedia....|
|Anna of Branden...|    1487-08-27    |    1514-05-03    |http://dbpedia....|
|Sigismund Of Br...|    1538-12-11    |    1566-09-13    |http://dbpedia....|
|Erdmuthe of Bra...|    1561-06-26    |    1623-11-13    |http://dbpedia....|
|Sigismund, John...|    1572-11-08    |    1619-12-23    |http://dbpedia....|
|Magdalene of Br...|    1582-01-07    |    1616-05-04    |http://dbpedia....|
|Agnes of Brande...|    1584-07-17    |    1629-03-26    |http://dbpedia....|
|Margrave of Bra...|    1591-11-20    |    1615-11-29    |http://dbpedia....|
|Margravine Loui...|    1617-09-13    |    1676-08-29    |http://dbpedia....|
|Frederick William |    1620-02-16    |    1688-04-29    |http://dbpedia....|
-----------------------------------------------------------------------------
```

For more examples, take a look at the SPARQL.NET Client included in the proejct.