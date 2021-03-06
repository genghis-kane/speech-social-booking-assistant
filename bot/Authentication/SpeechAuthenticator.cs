﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreBot.Authentication
{
    public class SpeechAuthenticator
    {
        private string _subscriptionKey;
        private readonly string _token;

        public SpeechAuthenticator(string subscriptionKey, string region)
        {
            var fetchTokenUri = $"https://{region}.api.cognitive.microsoft.com/sts/v1.0/issueToken";
            _subscriptionKey = subscriptionKey;
            _token = FetchTokenAsync(fetchTokenUri, subscriptionKey).Result;
        }

        public string GetAccessToken()
        {
            return _token;
        }

        private async Task<string> FetchTokenAsync(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(fetchUri);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
