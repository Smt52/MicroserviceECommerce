﻿using Ordering.Domain.Enums;

namespace Ordering.Infastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(orderId => orderId.Value,
            dbId => OrderId.Of(dbId));

        builder
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        builder
            .ComplexProperty(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });
        builder
            .ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);
                addressBuilder.Property(a => a.State)
                .HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });
        builder
            .ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);
                addressBuilder.Property(a => a.State)
                .HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder
            .ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(a => a.CardNumber)
                .HasMaxLength(50)
                .IsRequired();

                paymentBuilder.Property(a => a.CardNumber)
                .HasMaxLength(50)
                .IsRequired();

                paymentBuilder.Property(a => a.Expiration)
                .HasMaxLength(50)
                .IsRequired();

                paymentBuilder.Property(a => a.CVV)
                .HasMaxLength(180)
                .IsRequired();

                //Performans onemli degilse bu sekilde saklanabilir.
                //paymentBuilder.Property(a => a.PaymentMethod)
                //.HasConversion(paymentMethod => paymentMethod.ToString(),//Enum string olarak saklanir.
                //dbValue => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), dbValue));//String deger dbden cekildikten sonra tekrar enuma cevrilir.;

                //Performans icin saklama
                paymentBuilder.Property(a => a.PaymentMethod)
                .HasConversion<int>();//Enum degeri int olarak saklanir.
            });

        //Performans onemli degilse okunabilirligi arttirmak icin mantikli.
        //builder
        //.Property(o => o.Status)
        //.HasDefaultValue(OrderStatus.Draft)
        //.HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus))

        //Search icin performans onemli oldugundan asagidaki sekidle kullanilmistir.Enum degeri integer olarak saklanacaktir.

        builder
            .Property(o => o.Status)
            .HasConversion<int>();

        builder.Property(o => o.TotalPrice);
    }
}