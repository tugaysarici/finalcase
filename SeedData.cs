using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oms.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

INSERT INTO [dbo].[User] ([UserNumber],[Password],[Role],[Email],[FirstName],[LastName],[Address],[ProfitMargin],[OpenAccountBalance],[LastActivityDate],[PasswordRetryCount]
                         ,[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES (1, '5bb5aff891d4f5d841b26a6cb8122156', 'admin', 'info@tsrc.com.tr', 'TSRC', 'Pharmaceutical Company','Ankara Gölbaşı 6150', 0, 0, '10-10-2023',0,0,'10-10-2023',0,'10-10-2023',1)

INSERT INTO [dbo].[User] ([UserNumber],[Password],[Role],[Email],[FirstName],[LastName],[Address],[ProfitMargin],[OpenAccountBalance],[LastActivityDate],[PasswordRetryCount]
                         ,[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES (100001, 'b4137233c9bba7bf1d7d6eb0fc653d1a', 'dealer', 'tugaysarici@gmail.com', 'Tugay', 'SARICI','Zeytinburnu İstanbul 34015', 10, 500, '10-10-2023',0,0,'10-10-2023',0,'10-10-2023',1)

INSERT INTO [dbo].[User] ([UserNumber],[Password],[Role],[Email],[FirstName],[LastName],[Address],[ProfitMargin],[OpenAccountBalance],[LastActivityDate],[PasswordRetryCount]
                         ,[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES (100002, '572afcb68a9cf6dc7cec4d588070f9cf', 'dealer', 'cemyilmaz@gmail.com', 'Cem', 'YILMAZ','Kocaeli Gebze 41020', 20, 500, '10-10-2023',0,0,'10-10-2023',0,'10-10-2023',1)

INSERT INTO [dbo].[User] ([UserNumber],[Password],[Role],[Email],[FirstName],[LastName],[Address],[ProfitMargin],[OpenAccountBalance],[LastActivityDate],[PasswordRetryCount]
                         ,[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES (100003, '46c031bcf1bfcc2bfa34150db68bc7dd', 'dealer', 'browndan@gmail.com', 'Dan', 'BROWN','İzmir Bornova 35040', 15, 500, '10-10-2023',0,0,'10-10-2023',0,'10-10-2023',1)





INSERT INTO [dbo].[Product] ([Name],[BasePrice],[Stock],[Description],[MinimumQuantity],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('Acetaminophen', 5.99, 100, 'Ağrı kesici ve ateş düşürücü', 10, 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Product] ([Name],[BasePrice],[Stock],[Description],[MinimumQuantity],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('Ibuprofen', 7.99, 200, 'Anti-inflamatuar ilaç', 20, 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Product] ([Name],[BasePrice],[Stock],[Description],[MinimumQuantity],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('Simvastatin', 14.99, 200, 'Kolesterol düşürücü', 15, 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Product] ([Name],[BasePrice],[Stock],[Description],[MinimumQuantity],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('Clonazepam', 7.99, 45, 'Nöbet ve panik bozukluğu ilacı', 5, 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Product] ([Name],[BasePrice],[Stock],[Description],[MinimumQuantity],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('Oxycodone', 25.00, 30, 'Güçlü ağrı kesici', 5, 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Product] ([Name],[BasePrice],[Stock],[Description],[MinimumQuantity],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('Tamsulosin', 8.55, 22, 'Prostat büyümesi tedavisi', 20, 0, '01-01-2023', 0, '01-01-2023', 1)




INSERT INTO [dbo].[Chat] ([SenderEmail],[ReceiverEmail],[Message],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('tugaysarici@gmail.com', 'info@tsrc.com.tr', 'Merhaba. Siparişimi onaylar mısınız?', 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Chat] ([SenderEmail],[ReceiverEmail],[Message],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('info@tsrc.com.tr', 'tugaysarici@gmail.com', 'İnceliyorum.', 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Chat] ([SenderEmail],[ReceiverEmail],[Message],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('info@tsrc.com.tr', 'tugaysarici@gmail.com', 'Siparişiniz onaylanmıştır.', 0, '01-01-2023', 0, '01-01-2023', 1)

INSERT INTO [dbo].[Chat] ([SenderEmail],[ReceiverEmail],[Message],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('info@tsrc.com.tr', 'cemyilmaz@gmail.com', 'Cem Bey siparişiniz stok yetersizliğinden dolayı iptal edilmiştir.', 0, '01-01-2023', 0, '01-01-2023', 1)





            ");
            /*INSERT INTO [dbo].[Order] ([UserId],[PaymentMethod],[OrderStatus],[OrderItems],[InsertUserId],[InsertDate],[UpdateUserId],[UpdateDate],[IsActive])
VALUES ('2', 'ElectronicFundsTransfer', 0, null, 0, '01-01-2023', 0, '01-01-2023', 1)*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}