using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using RoboIAZoho.Models;

namespace RoboIAZoho.classes
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient _client;
        protected readonly ZohoApiSettings _apiSettings;

        protected BaseApiClient(HttpClient client, IOptions<ZohoApiSettings> apiSettingsOptions, string baseAddress)
        {
            _client = client;
            _apiSettings = apiSettingsOptions.Value;

            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiSettings.AccessToken);
        }
    }
}