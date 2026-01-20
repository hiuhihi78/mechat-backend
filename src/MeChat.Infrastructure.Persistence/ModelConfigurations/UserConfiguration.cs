using MeChat.Common.Shared.Constants;
using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Infrastructure.Persistence.ModelConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        #region Main Properties
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(50);
        builder.Property(x => x.Username).HasMaxLength(50);
        builder.Property(x => x.Password).HasMaxLength(50);
        builder.Property(x => x.Email);
        builder.Property(x => x.Status);
        builder.Property(x => x.CoverPhoto);
        #endregion

        #region Audit properties
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiedDate);
        #endregion

        #region Constraints
        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .IsRequired();
        #endregion

        #region Initial data
        builder.HasData(new User[]
        {
            // User 1
            User.CreateForDumbDataInDatabase(
                id: Guid.Parse("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                username: "test",
                password: "test",
                fullname: "test",
                roleId: AppConstants.Role.User,
                email: "mechat.mail@gmail.com",
                avatar: "https://me-chat.s3.ap-southeast-1.amazonaws.com/hieuld.jpg",
                coverPhoto: "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg",
                status: AppConstants.User.Status.Activate,
                createdDate: new DateTime(2024, 1, 1, 0, 0, 0),
                modifiedDate: new DateTime(2024, 1, 1, 0, 0, 0)
            ),

            // User 2
            User.CreateForDumbDataInDatabase(
                id: Guid.Parse("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                username: "test1",
                password: "test1",
                fullname: "test1",
                roleId: AppConstants.Role.User,
                email: "leduchieu2001x@gmail.com",
                avatar: "https://me-chat.s3.ap-southeast-1.amazonaws.com/hieuld02.jpg",
                coverPhoto: "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg",
                status: AppConstants.User.Status.Activate,
                createdDate: new DateTime(2024, 1, 1, 0, 0, 0),
                modifiedDate: new DateTime(2024, 1, 1, 0, 0, 0)
            ),

            // User 3
            User.CreateForDumbDataInDatabase(
                id: Guid.Parse("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                username: "test2",
                password: "test2",
                fullname: "test2",
                roleId: AppConstants.Role.User,
                email: "hieuldhe150703@fpt.edu.vn",
                avatar: "https://me-chat.s3.ap-southeast-1.amazonaws.com/thanhtt.jpg",
                coverPhoto: "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg",
                status: AppConstants.User.Status.Activate,
                createdDate: new DateTime(2024, 1, 1, 0, 0, 0),
                modifiedDate: new DateTime(2024, 1, 1, 0, 0, 0)
            )
        });
        #endregion

    }
}
