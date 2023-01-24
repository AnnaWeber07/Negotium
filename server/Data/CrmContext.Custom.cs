using Microsoft.EntityFrameworkCore;
using InDrivoHRM.Models;

namespace InDrivoHRM.Data
{
    public partial class CrmContext
    {
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }
    }
}
