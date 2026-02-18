using MicroSlate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MicroSlate.Infrastructure.AppConct
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
    {

        private readonly AesCryptoProvider _provider;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
           : base(options)
        {
            var key = Convert.FromBase64String(config["Encryption:Key"]);
            var iv = Convert.FromBase64String(config["Encryption:IV"]);

            _provider = new AesCryptoProvider(key, iv);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var codeEncrypt = new AesEncryptionConverter(_provider);


            modelBuilder.Entity<ApplicationUser>()
                .Property(x => x.Code)
                .HasConversion(codeEncrypt);



            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
