using System.Net.Http.Headers;

namespace Myafim.FireflyIii.Client;

public class FireflyIiiClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FireflyIiiClientFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public FireflyIiiClient CreateClient(Uri baseUri, string token)
    {
        var httpClient = MakeFireflyIiiHttpClient(baseUri, token);
        return new FireflyIiiClient(httpClient);
    }

    private HttpClient MakeFireflyIiiHttpClient(Uri baseUri, string token)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = baseUri;
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return httpClient;
    }
}