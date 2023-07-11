namespace ExchangeRateLoader.DAL;

using System.Collections.Generic;

public class RequiredRate
{
	public string Date { get; }
	public string Bank { get; }
	public int BaseCurrency { get; }
	public string BaseCurrencyLit { get; }
	public IEnumerable<CurrencyRate> ExchangeRate { get; }

	public RequiredRate(string date, string bank,
						int baseCurrency, string baseCurrencyLit,
						IEnumerable<CurrencyRate> exchangeRate)
	{
		Date = date;
		Bank = bank;
		BaseCurrency = baseCurrency;
		BaseCurrencyLit = baseCurrencyLit;
		ExchangeRate = exchangeRate;
	}
}
