using SPARQLNET.Misc;

namespace SPARQLNET.Enums
{
    public enum Format
    {
		[StringValue("application/sparql-results+xml")]
		XML,
		[StringValue("application/sparql-results+json")]
        JSON
    }
}