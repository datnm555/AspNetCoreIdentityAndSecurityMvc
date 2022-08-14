using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityAndSecurityMvc.Data
{
    public class DataDbContext: IdentityDbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {
        }

        protected DataDbContext()
        {
        }
    }
}
