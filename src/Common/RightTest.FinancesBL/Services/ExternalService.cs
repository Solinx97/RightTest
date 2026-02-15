using RightTest.FinancesBL.DTOs;
using RightTest.FinancesBL.Interfaces;
using System.Xml.Serialization;

namespace RightTest.FinancesBL.Services;

internal class ExternalService(HttpClient httpClient) : IExternalService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<ValuteDto>> GrabDataAsync(CancellationToken ct)
    {
        var response = await _httpClient.GetAsync("http://www.cbr.ru/scripts/XML_daily.asp", ct);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(ct);

        var serializer = new XmlSerializer(typeof(ExternalCurrencyDto));
        var result = (ExternalCurrencyDto)serializer.Deserialize(stream);

        return result.Valutes;
    }
}
