using Ordering.Domain.ValueObjects;

namespace Ordering.Infastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        //Convert customerId value read from db
        builder.Property(c => c.Id).HasConversion(customerId => customerId.Value, dbId => CustomerId.Of(dbId));

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        //Preventing the creation of a customer with same email
        builder.HasIndex(c => c.Email).IsUnique();
    }
}