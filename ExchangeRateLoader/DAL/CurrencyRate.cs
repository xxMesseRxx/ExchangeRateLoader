namespace ExchangeRateLoader.DAL;

public class CurrencyRate
{
	public string BaseCurrency { get; }
	public string Currency { get; }
	public decimal SaleRateNB { get; }
	public decimal PurchaseRateNB { get; }
	public decimal? SaleRate { get; }
	public decimal? PurchaseRate { get; }

	public CurrencyRate(string baseCurrency, string currency,
						decimal saleRateNB, decimal purchaseRateNB,
						decimal? saleRate = null, decimal? purchaseRate = null)
	{
		BaseCurrency = baseCurrency;
		Currency = currency;
		SaleRateNB = saleRateNB;
		PurchaseRateNB = purchaseRateNB;
		SaleRate = saleRate;
		PurchaseRate = purchaseRate;
	}
}
