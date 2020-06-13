using Pai.Models;
using System.Linq;

namespace Pai.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Tournaments.Any())
            {
                return;
            }

            context.Tournaments.Add(new Tournament { Title = "First Tournament" });
            context.Tournaments.Add(new Tournament { Title = "Second Tournament" });
            context.Tournaments.Add(new Tournament { Title = "Third Tournament" });
            context.SaveChanges();
        }
    }
}
