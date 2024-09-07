﻿namespace Ordering.Infastructure.Data.Configurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oI => oI.Id);

        builder.Property(oi => oi.Id)
            .HasConversion(orderItemId => orderItemId.Value,
            dbId => OrderItemId.Of(dbId));

        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();
    }
}