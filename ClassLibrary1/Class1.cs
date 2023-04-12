using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace ClassLibrary1
{
    public class Class1
    {
        public listofUserDTO GetUserDTO()
        {

            var weatherAPIurL = "https://api.openweathermap.org/data/2.5/weather?q=bund&appid=39a9a737b07b4b703e3d1cd1e231eedc&units=metric";
            var restClient = new RestClient("https://api.openweathermap.org");
            var restRequest = new RestRequest("/data/2.5/weather?q=bund&appid=39a9a737b07b4b703e3d1cd1e231eedc&units=metric"); 
            restRequest.RequestFormat=DataFormat.Json;

            //IRestResponse response = restClient.Execute(restRequest);

            //var content= response.Content;
            IRestResponse response= restClient.Execute(restRequest);
            var content = response.Content;

            var users = JsonConvert.DeserializeObject<listofUserDTO>(content);
            return users;

        }
    }
}
