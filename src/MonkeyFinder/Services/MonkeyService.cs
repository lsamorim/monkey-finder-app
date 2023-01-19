using Microsoft.Maui.Storage;
using MonkeyFinder.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    private List<Monkey> monkeyList = new List<Monkey>();
    private HttpClient _httpClient;

    public MonkeyService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Monkey>> GetMonkeysAsync()
    {
        if (monkeyList.Any())
        {
            return monkeyList;
        }

        //var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
        //var response = await _httpClient.GetAsync(url);

        //if (response.IsSuccessStatusCode)
        //{
        //    monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        //}
        //else
        //{
            using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);
        //}

        return monkeyList;
    }
}
