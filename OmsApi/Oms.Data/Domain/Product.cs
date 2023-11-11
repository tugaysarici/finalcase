using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oms.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oms.Data.Domain;

[Table("Product", Schema = "dbo")]
public class Product : BaseModel
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; } // it will change according to dealers.
    public int Stock { get; set; }
    public string Description { get; set; }
    public int MinimumQuantity { get; set; } // If Stock < MinimumQuantity, alert
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.InsertUserId).IsRequired();
        builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.BasePrice).IsRequired();
        builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
        builder.Property(x => x.MinimumQuantity).IsRequired().HasDefaultValue(0);
    }
}