using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MamApi.Helpers
{
    public class HttpHelper
    {
        //private static readonly HttpClient client = new HttpClient();

        //public async Task<string> Test()
        //{ 
        //    var values = new Dictionary<string, string>
        //        {
        //           { "thing1", "hello" },
        //           { "thing2", "world" }
        //        };

        //    var content = new FormUrlEncodedContent(values);

        //    var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

        //    var responseString = await response.Content.ReadAsStringAsync();

        //    return responseString;
        //}

        public string ConsumeWebApiViaPost(string baseUrl, string resource, object body)
        {
            var client = new RestClient(baseUrl);

            var request = new RestRequest(resource, Method.POST);

            request.RequestFormat = DataFormat.Json;

            string serverKey = @"AIzaSyA8kqRMX00Pt6uR2ubA6gERIvdm5BEwd6M";

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "key=" + serverKey);
            //request.AddHeader("Content-Type", "application/json");
            //request.Parameters.Clear();
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            request.AddJsonBody(body);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            // execute the request
            IRestResponse response = client.Execute(request);

            var content = response.Content; // raw content as string

            return response.Content;
        }

        public string TestPost()
        {
            var client = new RestClient("http://localhost:5000");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("/api/auth/login", Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(new 
            {
                userName = "ST12511",
                passWord = "1234C@l",
                IMEI = "123456789012345",
                firebaseToken = "abcdefghijklmnopqrstuvwxyz"
            });

            //var jsonOj = new Object { };

            //JsonConvert.SerializeObject(yourobject);

            //request.AddParameter("application/json; charset=utf-8", jsonOj, ParameterType.RequestBody);

            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method

            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            //// easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            //{
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();

            return response.Content;
        }

        public string TestGet()
        {
            var client = new RestClient("http://localhost:5000");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            
            var request = new RestRequest("api/masters/titles", Method.GET);

            request.RequestFormat = DataFormat.Json;

            //request.AddBody(new
            //{
            //    userName = "ST12511",
            //    passWord = "1234C@l",
            //    IMEI = "123456789012345",
            //    firebaseToken = "abcdefghijklmnopqrstuvwxyz"
            //});

            //var jsonOj = new Object { };

            //JsonConvert.SerializeObject(yourobject);

            //request.AddParameter("application/json; charset=utf-8", jsonOj, ParameterType.RequestBody);

            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method

            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            var accessToken = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTVDEyNTExIiwianRpIjoiMTIzNDU2Nzg5MDEyMzQ1IiwidXNlck5hbWUiOiLguJnguLLguIfguKrguLLguKfguKrguLTguKPguLTguJfguLTguJ7guKLguYwg4LiX4Li04Lie4Lii4LmA4LiB4Lip4LijIiwicG9zaXRpb25JZCI6IlBPUzAwMDAwMDQiLCJkZXBhcnRtZW50SWQiOiJERVBUMDAwMDAyIiwiYnJhbmNoSWQiOiIwMSIsImdyb3VwTGV2ZWxJZCI6IkdMMDAwMDAwMDEiLCJleHAiOjE1Mjg0ODA5ODAsImlzcyI6Im1hbS5jaW1idGhhaWF1dG8uY29tIiwiYXVkIjoibWFtLmNpbWJ0aGFpYXV0by5jb20ifQ.56ub3NYlmp2fgJBgffXEWj0nbiYKZU4Z4i-OXN9X2Bw";

            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddHeader("Accept", "application/json");

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            //// easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            //{
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();

            return response.Content;
        }
    }
}
