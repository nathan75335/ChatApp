using ChatApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DataAccess.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.HasIndex(x => x.UserName)
            .IsUnique();
        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);

        builder.HasData(new User
        {
            Id = -1,
            Name = "Pacha",
            Email = "Pacha@gmail.com",
            Password = "Password",
            UserName = "username",
            RoleId = 2
        });

        builder.ToTable("Users");
    }
}
