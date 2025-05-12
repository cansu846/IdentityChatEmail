using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityChatEmail.Context
{
    public class EmailContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-3ADO5MC\\SQLEXPRESS; initial catalog=IdentityEmailChat; integrated security=true; trust server certificate=true ");
        }
        
        public DbSet<Message> Messages {  get; set; }  
    }
}
