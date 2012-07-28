using System.Collections.Generic;

namespace SPARQLNET.Objects
{
	public class Content
	{
		public bool Distinct { get; set; }
		public bool Ordered { get; set; }
		public List<Result> Results { get; set; } 
	}
}