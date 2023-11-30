using ChatApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired();
        builder.HasData(new List<Role>
        {
            new()
            {
                Id = 1,
                Name = "User",
                Description = "This is the user Role"
            },
            new()
            {
                Id= 2,
                Name = "Admin",
                Description = "This is the admin Role"
            }
        });

        builder.ToTable("Roles");
    }
}
