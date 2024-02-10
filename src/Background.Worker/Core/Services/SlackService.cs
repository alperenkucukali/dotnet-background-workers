using Background.Worker.Core.Options;
using Background.Worker.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Background.Worker.Core.Services
{
    public class SlackService : ISlackService
    {
        private readonly HttpClient _client;
        private readonly SlackOptions _slackOptions;
        public SlackService(IHttpClientFactory httpClientFactory, IOptions<SlackOptions> slackOptions)
        {
            _slackOptions = slackOptions.Value;
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri(_slackOptions.Uri);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _slackOptions.AccessToken);
        }
        public async Task SendMessage(string message, string channelKey = "Default")
        {
            await Send(new PostMessage(GetChannelId(channelKey), message));
        }
        private string GetChannelId(string channelKey)
        {
            _slackOptions.Channels.TryGetValue(channelKey, out var channelId);
            if (channelId is null)
                _slackOptions.Channels.TryGetValue("Default", out channelId);
            return channelId!;
        }
        private async Task<bool> Send(PostMessage postMessage)
        {
            var response = await _client.PostAsync($"api/chat.postMessage?channel={HttpUtility.UrlEncode(postMessage.channel)}&text={HttpUtility.UrlEncode(postMessage.text)}&pretty={postMessage.pretty}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Slack service raise an error with status code {response.StatusCode}!");
            return true;
        }
        private class PostMessage
        {
            public PostMessage(string channelId, string message)
            {
                channel = channelId;
                text = message;
            }
            public string channel { get; private set; } = null!;
            public string text { get; private set; } = null!;
            public int pretty { get { return 1; } }
        }
    }
}
