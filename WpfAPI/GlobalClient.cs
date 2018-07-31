using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WpfAPI
{
    public static class  GlobalClient
    {
        public static HttpClient httpClient = new HttpClient();

        static GlobalClient()
        {
            try
            {
                httpClient.BaseAddress = new Uri("http://localhost:52174/api/");
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
