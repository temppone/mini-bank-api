using System.Text.Json;
using ApiTest.Model;

public class CNPJService(HttpClient httpClient)
{
    public async Task<CNPJInfoResponse> GetCompanyData(string cnpj)
    {
        var response = await httpClient.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");


        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            CNPJInfoResponse? companyInfo = JsonSerializer.Deserialize<CNPJInfoResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return companyInfo ?? throw new InvalidOperationException("Failed to deserialize company info");
        }

        throw new HttpRequestException($"Failed to retrieve data from API. Status code: {response.StatusCode}");
    }
}