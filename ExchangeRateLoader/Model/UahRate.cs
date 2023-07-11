namespace ExchangeRateLoader.Model;

using System;
using System.Collections.Generic;

public partial class UahRate
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public string Currency { get; set; } = null!;

    public decimal Rate { get; set; }
}
