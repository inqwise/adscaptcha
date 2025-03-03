using System;

namespace Inqwise.AdsCaptcha.Common
{
    public enum PaymentMethod
    {
        CreditCard = 12001,
        BankWire = 12002,
        PayPal = 12003,
        Check = 12004,
        Manual = 12005,
    }

    public enum BillingMethod
    {
        Prepay = 21001,
        Postpay = 21002,
        Undefined = 21003,
    }

    public interface IAdvertiser
    {
        int Id { get; }
        string Email { get; }
        DateTime JoinDate { get; }
        Status Status { get; }
        BillingMethod BillingMethod { get; }
        PaymentMethod PaymentMethod { get; }
        bool EmailAnnouncements { get; }
        bool EmailNewsletters { get; }
        decimal MinimumBillingAmount { get; }
    }
}