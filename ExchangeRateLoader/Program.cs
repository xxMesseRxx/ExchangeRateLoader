using ExchangeRateLoader.Library;
using ExchangeRateLoader.Services;

Console.WriteLine("Hello I can load and save currency rate to DB,");

do
{
    DateOnly date = GetDate();
    CurrencyType currencyType = GetCurrencyType();

    CurrencyRateService currencyRateService = new CurrencyRateService(date, currencyType);

    try
    {
        await currencyRateService.LoadCurrencyRateToDBAsync();
        Console.WriteLine("The data has been successfully added to the DB");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
while (WantContinue());

DateOnly GetDate()
{
    DateOnly date;

    Console.Write("Please enter date (dd.MM.yyyy): ");
    string dateStr = Console.ReadLine();

    while (!DateOnly.TryParse(dateStr, out date))
    {
        Console.WriteLine("Your date isn't correct, enter again: ");
        dateStr = Console.ReadLine();
    }

    return date;
}

CurrencyType GetCurrencyType()
{
    CurrencyType type;

    Console.Write("Please enter currency type (EUR/USD/etc.): ");
    string currencyType = Console.ReadLine();

    while (!CurrencyType.TryParse(currencyType.ToUpper(), out type))
    {
        Console.WriteLine("Your type isn't correct, enter again: ");
        currencyType = Console.ReadLine();
    }

    return type;
}

bool WantContinue()
{
    Console.Write("Do you want continue? (y/n): ");
    string desire;

    while (true)
    {
        switch (desire = Console.ReadLine())
        {
            case "y": return true;
            case "n": return false;
            default:
                Console.WriteLine("You entered not 'y' or 'n', try again: ");
                break;
        }
    }
}