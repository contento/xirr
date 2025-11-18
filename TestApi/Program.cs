using System.Net.Http;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var apiUrl = "http://localhost:5268/xirr";

        var cashFlows = new (string Date, double Amount)[]
        {
            ("2012-06-01", 0.01),
            ("2012-07-23", 3042626.18),
            ("2012-11-07", -491356.62),
            ("2012-11-30", 631579.92),
            ("2012-12-01", 19769.5),
            ("2013-01-16", 1551771.47),
            ("2013-02-08", -304595),
            ("2013-03-26", 3880609.64),
            ("2013-03-31", -4331949.61)
        };

        var json = JsonSerializer.Serialize(new { CashFlows = cashFlows });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("XIRR Result: " + result);
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }
}
