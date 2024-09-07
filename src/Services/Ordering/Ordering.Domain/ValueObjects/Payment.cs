﻿namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;

        //TODO Create PaymentMethod Enum
        public PaymentMethod PaymentMethod { get; } = PaymentMethod.CreditCard;

        protected Payment() { }

        private Payment(string cardName, string cardNumber, string expiration, string cvv, PaymentMethod paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, PaymentMethod paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}