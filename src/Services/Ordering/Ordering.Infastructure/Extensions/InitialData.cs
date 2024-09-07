using Ordering.Domain.Enums;

namespace Ordering.Infastructure.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("7A0FC92A-CC43-4083-9305-4CD90DE66E6F")),"samet","smt@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("E9586C1F-478E-4B20-9A1B-91790AA15C38")),"ali","ali@outlook.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("7F1AE2FE-79BA-4E7F-9718-90A65BDF39CA")),"IPhone X",500),
        Product.Create(ProductId.Of(new Guid("29051BBA-6D37-41E8-8924-4D33F21B32A4")),"Samsung 10",400),
        Product.Create(ProductId.Of(new Guid("A34951CD-6A25-408B-80AE-41117946A86A")),"Huawei Plus",650),
        Product.Create(ProductId.Of(new Guid("5B97ECC1-EA91-46FE-9AD5-A02E44E39967")),"Xiaomi Mi",450),
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("samet", "tnrkl", "smt@gmail.com", "Ankara,Yenimahalle", "Yenimahalle", "Ankara", "06000");
            var address2 = Address.Of("ali", "mehmet", "ali@outlook.com", "Yedigoller Kamp Alani", "Yedigolle", "Bolu", "14000");

            var payment1 = Payment.Of("samet", "1234567891234567", "06/30", "123", PaymentMethod.CreditCard);
            var payment2 = Payment.Of("ali", "9876543211234567", "05/30", "987", PaymentMethod.DebitCard);

            var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("7A0FC92A-CC43-4083-9305-4CD90DE66E6F")),
                    orderName: OrderName.Of("Order 1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment: payment1
                );
            order1.Add(ProductId.Of(new Guid("7F1AE2FE-79BA-4E7F-9718-90A65BDF39CA")), 2, 500);
            order1.Add(ProductId.Of(new Guid("A34951CD-6A25-408B-80AE-41117946A86A")), 1, 650);

            var order2 = Order.Create
                (
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("E9586C1F-478E-4B20-9A1B-91790AA15C38")),
                    orderName: OrderName.Of("Order 2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment: payment2
                );

            order2.Add(ProductId.Of(new Guid("29051BBA-6D37-41E8-8924-4D33F21B32A4")), 1, 400);
            order2.Add(ProductId.Of(new Guid("5B97ECC1-EA91-46FE-9AD5-A02E44E39967")), 3, 450);

            return new List<Order> { order1, order2 };
        }
    }
}