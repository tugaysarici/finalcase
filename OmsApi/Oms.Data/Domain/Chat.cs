using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Oms.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oms.Data.Domain;

[Table("Chat", Schema = "dbo")]
public class Chat : BaseModel
{
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public string Message { get; set; }
}

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.Property(x => x.InsertUserId).IsRequired();
        builder.Property(x => x.UpdateUserId).HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).HasDefaultValue(null);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.Property(x => x.SenderEmail).IsRequired(true);
        builder.Property(x => x.ReceiverEmail).IsRequired(true);
        builder.Property(x => x.Message).IsRequired(true);
    }
}