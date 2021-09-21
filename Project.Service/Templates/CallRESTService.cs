//using Microsoft.ApplicationInsights;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Project.Service.Templates
//{
//    public class CallRESTService<T, R>
//    {
//        public async Task<R> CallMethodPost(T request, R requestResponse, string methodName, AppSettings appSettings)
//        {
//            var payload = JsonSerializer.Serialize(request);

//            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

//            using (HttpClient client = new HttpClient())
//            {
//                var response = await client.PostAsync(appSettings.CamundaServiceUlr + methodName, content);

//                if (response.StatusCode != System.Net.HttpStatusCode.OK)
//                {
//                    var msg = "ERROR: Sending CartCheckedOut to Camunde.Service. received status code: " + response.StatusCode.ToString();
//                    throw new Exception(msg);
//                }
//                var resultString = await response.Content.ReadAsStringAsync();

//                var options = new JsonSerializerOptions
//                {
//                    PropertyNameCaseInsensitive = true,
//                };

//                requestResponse = JsonSerializer.Deserialize<R>(resultString, options);
//            }


//            return requestResponse;
//        }

//    }
//}
