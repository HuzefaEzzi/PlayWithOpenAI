using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithOpenAI
{
    internal class PineCone
    {
        private readonly string apiKey;

        PineCone(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public async Task Upsert(object obj)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://h-embedding-play-58eca3a.svc.us-west4-gcp.pinecone.io/vectors/upsert"),
                Headers =
                    {
                        { "accept", "application/json" },
                        { "Api-Key", "604cf1da-33aa-4a40-8e37-49fafbce31f2" },
                    },
                Content = new StringContent("{\"vectors\":[{\"values\":[0.22,0.55],\"metadata\":{\"id\":\"a\",\"name\":\"b\"}}]}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
