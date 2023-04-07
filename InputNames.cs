using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restful_Names
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class InputPerson
    {
        public string type { get; set; }
        public PersonName personName { get; set; }
    }

    public class NameField
    {
        public string @string { get; set; }
        public string fieldType { get; set; }
    }

    public class PersonName
    {
        public List<NameField> nameFields { get; set; }
    }
}
