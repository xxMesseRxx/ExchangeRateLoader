namespace ExchangeRateLoader.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using ExchangeRateLoader.DAL;

public class RequestToPrivatBank
{
	private readonly string _date;

	private static HttpClient _httpClient;
	private static string _baseUrl;

	static RequestToPrivatBank()
	{
		_httpClient = new HttpClient();

		IConfiguration config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		_baseUrl = config["URLs:PrivatBank"];
	}
	public RequestToPrivatBank(DateOnly date)
	{
		_date = date.ToShortDateString();
	}

	public async Task<List<CurrencyRate>> GetCurrencyRatesAsync()
	{
		List<CurrencyRate> currencyRates = new List<CurrencyRate>();

		try
		{
			var requiredRate = await _httpClient.GetFromJsonAsync<RequiredRate>(_baseUrl + _date);
			currencyRates = requiredRate?.ExchangeRate.ToList();
		}
		catch (Exception)
		{
			throw new InvalidOperationException("Date is invalid or resource isn't available");
		}

		return currencyRates;
	}
}
