using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Oms.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oms.Data.Domain;

[Table("Order", Schema = "dbo")]
public class Order : BaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int OrderNumber { get; set; }
    public string PaymentMethod { get; set; }   //ElectronicFundsTransfer, WireTransfer, CreditCard, OpenAccount
    public decimal OrderAmount { get; set; }
    public bool OrderStatus { get; set; }       // not approved(false) - approved(true)
    public virtual List<OrderItem> OrderItems { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.InsertUserId).IsRequired();
        builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.OrderNumber).IsRequired(true);
        builder.Property(x => x.PaymentMethod).IsRequired(true);
        builder.Property(x => x.OrderAmount).IsRequired(true).HasDefaultValue(0);
        //builder.Property(x => x.OrderItems).IsRequired(false);
        builder.Property(x => x.OrderStatus).IsRequired(true).HasDefaultValue(false);

        builder.HasIndex(x => x.OrderNumber).IsUnique();
        builder.HasIndex(x => x.UserId);

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .IsRequired(true);
    }
}