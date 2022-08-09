using Microsoft.EntityFrameworkCore;

namespace CQRSWebAPI_Demo.Models
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}