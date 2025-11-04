using Microsoft.EntityFrameworkCore;
using ParcoBackend.Model;

namespace ParcoBackend.Tests.TestHelpers
{
    public static class TestDbContextFactory
    {
        public static ParcoContext CreateContext()
        {
            // Cria um nome de banco aleatório para cada teste (isolamento total)
            var options = new DbContextOptionsBuilder<ParcoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ParcoContext(options);

            // Garante que o banco está limpo
            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(ParcoContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
