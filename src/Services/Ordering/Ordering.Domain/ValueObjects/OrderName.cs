namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int NameLength = 5;

        public string Value { get; set; }

        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, NameLength);

            return new OrderName(value);
        }
    }
}