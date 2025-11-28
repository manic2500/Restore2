namespace Restore.Common.Utilities;

using System.Globalization;

public static class MoneyFormatter
{
    // You can configure currency code (e.g., "INR") and culture info
    private static readonly CultureInfo Culture = new CultureInfo("en-US");
    private const string CurrencyCode = "INR";

    /// <summary>
    /// Format cents to human-readable currency string.
    /// E.g., 49900 -> "₹499.00"
    /// </summary>
    /// <param name="amountInCents">Amount in cents</param>
    /// <returns>Formatted currency string</returns>
    public static string Format(long amountInCents)
    {
        decimal amount = amountInCents / 100m; // convert cents to full units
        return string.Format(Culture, "{0:C}", amount);
    }

    /// <summary>
    /// Overload for decimal values (already in full units)
    /// </summary>
    public static string Format(decimal amount)
    {
        return string.Format(Culture, "{0:C}", amount);
    }
}

/* 

Make currency configurable via appsettings.json.

Add CultureInfo support for multiple locales.

Centralize in a shared library so both admin and API projects can use it.
 */