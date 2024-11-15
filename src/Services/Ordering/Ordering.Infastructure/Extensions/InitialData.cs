using Ordering.Domain.Enums;

namespace Ordering.Infastructure.Extensions;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("7A0FC92A-CC43-4083-9305-4CD90DE66E6F")),"samet","smt@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("E9586C1F-478E-4B20-9A1B-91790AA15C38")),"ali","ali@outlook.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")),"IPhone X",500),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")),"Samsung 10",400),
        Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")),"Huawei Plus",650),
        Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")),"Xiaomi Mi 9",450),
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
            order1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 500);
            order1.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 650);

            var order2 = Order.Create
                (
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("E9586C1F-478E-4B20-9A1B-91790AA15C38")),
                    orderName: OrderName.Of("Order 2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment: payment2
                );

            order2.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 400);
            order2.Add(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 3, 450);

            return new List<Order> { order1, order2 };
        }
    }
}