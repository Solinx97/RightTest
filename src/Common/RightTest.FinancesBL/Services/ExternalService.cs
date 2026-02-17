using Microsoft.Extensions.Options;
using RightTest.FinancesBL.Options;
using RightTest.FinancesBL.DTOs;
using RightTest.FinancesBL.Interfaces;
using System.Xml.Serialization;

namespace RightTest.FinancesBL.Services;

internal class ExternalService(HttpClient httpClient, IOptions<ServersOptions> serversOptions) : IExternalService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ServersOptions _serversOptions = serversOptions.Value;

    public async Task<IEnumerable<ValuteDto>> GrabDataAsync(CancellationToken ct)
    {
        var response = await _httpClient.GetAsync(_serversOptions.ExternalServerUrl, ct);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(ct);

        var serializer = new XmlSerializer(typeof(ExternalCurrencyDto));
        var result = (ExternalCurrencyDto)serializer.Deserialize(stream);

        return result.Valutes;
    }
}
