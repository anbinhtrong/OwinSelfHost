using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/values").Result;
                
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                var config = new HttpConfiguration();
                IApiExplorer apiExplorer = Startup.MyConfiguration.Services.GetApiExplorer();
                foreach (var apiDescription in apiExplorer.ApiDescriptions)
                {
                    Console.WriteLine("Uri path: {0}", apiDescription.RelativePath);
                    Console.WriteLine("HTTP method: {0}", apiDescription.HttpMethod);
                    foreach (ApiParameterDescription parameter in apiDescription.ParameterDescriptions)
                    {
                        Console.WriteLine("Parameter: {0} - {1}", parameter.Name, parameter.Source);
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadLine(); 
        }
    }
}
