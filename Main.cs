using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restful_Names
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var validator = new NameValidator();
            if (validator.Ping())
            {
                validator.Check();
            }
        }
    }

    public class NameValidator
    {
        public bool Ping()
        {
            string URL = "https://api.nameapi.org/rest/v5.3/system/ping";
            string urlParameters = "?apiKey=25f475319964503e048cb29e780f3e4b-ClickAlgo";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
               var result = response.Content.ReadAsStringAsync().Result;

                if (result == "pong")
                    return true;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return false;
        }

        public async Task Check()
        {
            HttpClient client = new HttpClient();

            string URL = "https://api.nameapi.org/rest/v5.3/parser/personnameparser";
            string urlParameters = "?apiKey=25f475319964503e048cb29e780f3e4b-ClickAlgo";

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var tmp = @" {""context"" : {
                    ""priority"" : ""REALTIME"",
                     ""properties"" : [ ]
                   },
                  ""inputPerson"" : {
                                    ""type"" : ""NaturalInputPerson"",
                    ""personName"" : {
                                        ""nameFields"" : [ {
                                            ""string"" : ""fred"",
                        ""fieldType"" : ""GIVENNAME""
                                        }, {
                                            ""string"" : ""mm"",
                        ""fieldType"" : ""SURNAME""
                                     } ]
                    },
                    ""gender"" : ""UNKNOWN""
                  }
            }";

            //string person = "{ ""context"" : {""priority"" : ""REALTIME"", 'properties' : [ ]},'inputPerson' : {'type' : 'NaturalInputPerson','personName' : {'nameFields' : [ {'string' : 'Petra','fieldType' : 'GIVENNAME'}, { 'string' : 'Meyer', 'fieldType' : 'SURNAME'} ]},'gender' : 'UNKNOWN'} }    ";

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(tmp, Encoding.UTF8, "application/json");

            // List data response.
            var response = await client.PostAsync(urlParameters, httpContent);  // Blocking call! Program will wait here until a response is received or a timeout occurs.


            // If the response contains content we want to read it!
            if (response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
            }

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
