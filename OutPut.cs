using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restful_Names
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Gender
    {
        public string gender { get; set; }
        public double confidence { get; set; }
    }

    public class Match
    {
        public ParsedPerson parsedPerson { get; set; }
        public List<object> parserDisputes { get; set; }
        public double likeliness { get; set; }
        public double confidence { get; set; }
    }

    public class OutputPersonName
    {
        public List<Term> terms { get; set; }
    }

    public class ParsedPerson
    {
        public string personType { get; set; }
        public string personRole { get; set; }
        public List<string> mailingPersonRoles { get; set; }
        public Gender gender { get; set; }
        public string addressingGivenName { get; set; }
        public string addressingSurname { get; set; }
        public OutputPersonName outputPersonName { get; set; }
    }

    public class Term
    {
        public string @string { get; set; }
        public string termType { get; set; }
    }


}
