namespace CinemaApp.Data.Seeding
{
    using System.Text.Json;

    using Models;
    using Interfaces;
    using CinemaApp.Data.Utilities.Interfaces;
    using static Common.OutputMessages.ErrorMessages;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class MoviesSeeder : BaseSeeder<MoviesSeeder>, IEntitySeeder
    {
        private readonly CinemaDbContext dbContext;

        public MoviesSeeder(CinemaDbContext dbContext, IValidator entityValidator, 
            ILogger<MoviesSeeder> logger)
            : base(entityValidator, logger)
        {
            this.dbContext = dbContext;
        }

        public override string FilePath
            => Path.Combine(AppContext.BaseDirectory, "Files", "movies.json");

        public async Task SeedEntityData()
        {
            await this.ImportMoviesFromJson();
        }

        private async Task ImportMoviesFromJson()
        {
            string moviesStr = await File.ReadAllTextAsync(this.FilePath);
            var movies = JsonSerializer.Deserialize<List<Movie>>(moviesStr);

            if (movies != null && movies.Count > 0)
            {
                List<Guid> moviesIds = movies.Select(m => m.Id).ToList();
                if (await dbContext.Movies.AnyAsync(m => moviesIds.Contains(m.Id)) == false)
                {
                    await dbContext.Movies.AddRangeAsync(movies);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    // Log warning message
                    this.Logger.LogWarning(EntityInstanceAlreadyExist);
                }
            }
        }
    }
}
