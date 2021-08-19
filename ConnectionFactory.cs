using CoreServices.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreServices.Tests
{
    class ConnectionFactory
    {
        public ProductDBContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase(databaseName: "ProductDB").Options;

            var context = new ProductDBContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }
    }
}