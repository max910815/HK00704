using HK_project.ViewModels;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace HK_project.Services
{
    public class APIService
    {
        public async Task Anser(TurboViewModel input)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7168/api/Similar");

            var send = $"[{{'chatId': '{input.ChatId}', 'temperature':'{input.temperature}', 'question':'{input.Question}', 'applicationId':'{input.Setting.ApplicationId}', 'dataId':'{input.DataId}'}}]";
            var content = new StringContent(send);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }
    }
}
