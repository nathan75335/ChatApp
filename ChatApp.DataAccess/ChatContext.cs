using ChatApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess;

public class ChatContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Contact> Contacts { get; set; }
   
    public ChatContext(DbContextOptions options ) : base(options) 
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatContext).Assembly);
    }
}
