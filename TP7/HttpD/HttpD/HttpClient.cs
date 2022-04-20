using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpD
{
    public class HttpClientStream
    {
        private readonly HttpClient _httpClient;

        private HttpMethod _method;
        private string url;

        // TODO: getter for _httpClient named `httpClient`
        public HttpClient httpClient
        {
            get => _httpClient;
        }

        // TODO: getter for _method named `Method`
        public HttpMethod Method
        {
            get => _method;
        }


        public HttpClientStream(HttpMethod method, string url = "http://127.0.0.1:8000")
        {
            _method = method;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public HttpClientStream(string url)
        {
            this.url = url;
        }

        public StreamReader SendMessage(string message = "")
        {
            var response = _httpClient.SendAsync(new HttpRequestMessage(Method, _httpClient.BaseAddress)
            {
                Content = new StringContent(message)
            }).Result;
            return new StreamReader(response.Content.ReadAsStream());

        }

        public string ResponseFromStream(StreamReader stream)
        {
            return stream.ReadToEnd();
        }
    }
}