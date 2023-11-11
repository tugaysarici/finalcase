using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Oms.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oms.Data.Domain;

[Table("User", Schema = "dbo")]
public class User : BaseModel
{
    public int UserNumber { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }                       // info@tsrc.com.tr

    public string FirstName { get; set; }                   // TSRC
    public string LastName { get; set; }                    // Pharmaceutical Company
    public string Address { get; set; }                   // Gölbaşı, Ankara, 6380
    public decimal ProfitMargin { get; set; }               // 0 for admin. x for dealers.
    public decimal OpenAccountBalance { get; set; }         // her bayi için 500₺ bakiye olsun, bu limite kadar açık hesap ödemesi seçilebilir. 500 - x 
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }

    public virtual List<Order> Orders { get; set; }
    
    //public virtual List<InvoiceDetail> InvoiceDetails { get; set; }
    
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.InsertUserId).IsRequired();
        builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.UserNumber).IsRequired();
        builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired().HasDefaultValue("dealer");    // # of admin = 1. # of dealer >= 1
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ProfitMargin).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.OpenAccountBalance).IsRequired().HasDefaultValue((double)500);
        builder.Property(x => x.LastActivityDate).IsRequired();
        builder.Property(x => x.PasswordRetryCount).IsRequired().HasDefaultValue(0);

        builder.HasIndex(x => x.UserNumber).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);


        builder.HasMany(x => x.Orders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired(true);

        /*
        builder.HasMany(x => x.InvoiceDetails)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired(true);
        */
    }
}