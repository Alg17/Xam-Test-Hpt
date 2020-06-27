using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestMovil.Services
{
    public class ApiClient
    {
        public static HttpClient httpClient { get; set; }

        public static void Init()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://www.rsfmovil.somee.com/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
