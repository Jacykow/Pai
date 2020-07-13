using Pai.DatabaseModels;
using System;
using System.Linq;

namespace Pai.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Tournament.Count() > 0)
            {
                return;
            }

            context.Tournament.RemoveRange(context.Tournament);

            var firstTournament = new Tournament
            {
                Title = "First Tournament",
                Discipline = "Throwing",
                Location = "Poznań",
                Time = DateTime.Parse("2005-09-01"),
                EntryDateLimit = DateTime.Parse("2005-07-01"),
                AssignedPlayersAmount = 1,
                EntryLimit = 3
            };
            context.Tournament.Add(firstTournament);
            context.Tournament.Add(new Tournament
            {
                Title = "Second Tournament",
                Discipline = "Jumping",
                Location = "Bydgoszcz",
                Time = DateTime.Parse("2015-09-01"),
                EntryDateLimit = DateTime.Parse("2015-08-01"),
                AssignedPlayersAmount = 0,
                EntryLimit = 5
            });
            var r = new Random();
            for (int x = 0; x < 20; x++)
            {
                context.Tournament.Add(new Tournament
                {
                    Title = "Repeated Tournament",
                    Discipline = "Repetition",
                    Location = "Gdańsk",
                    Time = DateTime.Parse("2000-11-01"),
                    EntryDateLimit = DateTime.Parse("2000-10-01"),
                    AssignedPlayersAmount = r.Next(0, 3),
                    EntryLimit = r.Next(2, 5)
                });
            }
            context.SaveChanges();

            context.Sponsor.RemoveRange(context.Sponsor);

            context.Sponsor.Add(new Sponsor
            {
                ImageUrl = "https://static.dezeen.com/uploads/2019/04/ikea-logo-new-hero-1.jpg",
                Tournament = firstTournament
            });
            context.Sponsor.Add(new Sponsor
            {
                ImageUrl = "https://v.wpimg.pl/ODU5ODcxYCU0Vzl3TANtMHcPbS0KWmNmIBd1Zkw3YHJhATd2WkxgIHtCPy0OH2E0ORp2clFNenFnAnppTkl5cmwMf3NXT3drOVooK04SNjc-GyU0BFoy",
                Tournament = firstTournament
            });
            context.SaveChanges();
        }
    }
}
