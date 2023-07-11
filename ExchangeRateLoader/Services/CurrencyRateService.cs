namespace ExchangeRateLoader.Services;

using ExchangeRateLoader.DAL;
using ExchangeRateLoader.Library;
using ExchangeRateLoader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

public class CurrencyRateService
{
    private readonly DateOnly _date;
    private readonly CurrencyType _currencyType;

    public CurrencyRateService(DateOnly date, CurrencyType currencyType)
    {
        _date = date;
        _currencyType = currencyType;
    }

    public async Task LoadCurrencyRateToDBAsync()
    {
        CurrencyRate selectedCurrency = await GetSelectedCurrencyRateAsync();

        if (selectedCurrency is null)
        {
            throw new ArgumentException("Currency type for this date isn't found");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ExchangeRateContext>();

        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);

        using (ExchangeRateContext dbContext = new ExchangeRateContext(optionsBuilder.Options))
        {
            UahRate uahRate = new UahRate()
            {
                Date = _date,
                Currency = selectedCurrency.Currency,
                Rate = selectedCurrency.PurchaseRateNB
            };

            await dbContext.UahRates.AddAsync(uahRate);
            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<CurrencyRate> GetSelectedCurrencyRateAsync()
    {
        List<CurrencyRate> currencyRates = new List<CurrencyRate>();
        RequestToPrivatBank request = new RequestToPrivatBank(_date);

        currencyRates = await request.GetCurrencyRatesAsync();
  
        if (currencyRates.Count == 0)
        {
            throw new ArgumentException("Date isn't correct");
        }

        CurrencyRate selectedCurrency = currencyRates.FirstOrDefault(c => c.Currency == _currencyType.ToString());

        return selectedCurrency;
    }
}
