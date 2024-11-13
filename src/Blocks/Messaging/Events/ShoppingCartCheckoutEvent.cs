namespace Messaging.Events;

//Used primitive types for serialization or deserialization between services
public record ShoppingCartCheckoutEvent : IntegrationEvent
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = Guid.Empty!;
    public decimal TotalPrice { get; set; } = default!;

    //For Addresses
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    //For Payment
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string Cvv { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;
}