namespace CinemaApp.Data.Seeding
{
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    using Interfaces;

    public class ApplicationDbContextSeeder : IDbSeeder
    {
        private readonly IServiceProvider serviceProvider;

        private ICollection<IEntitySeeder> entitySeeders = null!;
        
        public ApplicationDbContextSeeder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            this.InitializeDbSeeders();
        }

        public async Task SeedData()
        {
            foreach (IEntitySeeder entitySeeder in this.entitySeeders)
            {
                await entitySeeder.SeedEntityData();
            }
        }

        private void InitializeDbSeeders()
        {
            this.entitySeeders = new List<IEntitySeeder>();
            Type[] seedersToRequest = this.DiscoverDbSeeders(typeof(CinemaMovieSeeder).Assembly);
            foreach (Type seederType in seedersToRequest)
            {
                object seederInstanceObj = this.serviceProvider
                    .GetRequiredService(seederType);
                if (seederInstanceObj is IEntitySeeder seederInstanceCast)
                {
                    this.entitySeeders.Add(seederInstanceCast);
                }
            }
        }

        private Type[] DiscoverDbSeeders(Assembly seedersAssembly)
        {
            Type[] seederTypes = seedersAssembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.Name.Equals(nameof(IEntitySeeder))))
                .ToArray();

            return seederTypes;
        }
    }
}
