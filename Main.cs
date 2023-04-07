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
            if (Ping())
            {
                Check();
            }
        }

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

        public void Check()
        {
            HttpClient client = new HttpClient();

            string URL = "https://api.nameapi.org/rest/v5.3/parser/personnameparser";
            string urlParameters = "?apiKey=25f475319964503e048cb29e780f3e4b-ClickAlgo";

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new Root();
            var inputPerson = new InputPerson();
            inputPerson.type = "NaturalInputPerson";
            inputPerson.gender = "UNKNOWN";

            var person = new PersonName();
            var fields = new List<NameField>();
            var field = new NameField();
            field.@string = TxtName.Text;
            field.fieldType = "FULLNAME";
            fields.Add(field);

            person.nameFields = fields;
            inputPerson.personName = person;
            payload.inputPerson = inputPerson;
           

            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(payload);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            // List data response.
            var response = client.PostAsync(urlParameters, httpContent);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            response.Wait();

            // If the response contains content we want to read it!
            if (response.IsCompletedSuccessfully)
            {
                var responseContent = response.Result.Content.ReadAsStringAsync();
                txtResult.Text = responseContent.Result;

                // Parse the response body.
                var dataObjects = response.Result.Content.ReadAsAsync<OutPut>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                
                if(dataObjects.bestMatch == null || dataObjects.bestMatch.confidence < .5)
                {
                    lblResult.Text = "FAKE";
                }
                else
                {
                    lblResult.Text = "VALID";
                }
            }
            else
            {

            }

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
