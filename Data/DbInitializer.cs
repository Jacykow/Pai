using Pai.Models;
using System;
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

            context.Tournaments.Add(new Tournament
            {
                Title = "First Tournament",
                Discipline = "Throwing",
                Location = "-",
                Time = DateTime.Parse("2005-09-01"),
                EntryDateLimit = DateTime.Parse("2005-07-01"),
                AssignedPlayersAmount = 1,
                EntryLimit = 3
            });
            context.Tournaments.Add(new Tournament
            {
                Title = "Second Tournament",
                Discipline = "Jumping",
                Location = "-",
                Time = DateTime.Parse("2015-09-01"),
                EntryDateLimit = DateTime.Parse("2015-08-01"),
                AssignedPlayersAmount = 0,
                EntryLimit = 5
            });
            context.SaveChanges();

            context.Sponsors.Add(new Sponsor
            {
                ImageUrl = "https://static.dezeen.com/uploads/2019/04/ikea-logo-new-hero-1.jpg",
                TournamentID = 1
            });
            context.Sponsors.Add(new Sponsor
            {
                ImageUrl = "https://v.wpimg.pl/ODU5ODcxYCU0Vzl3TANtMHcPbS0KWmNmIBd1Zkw3YHJhATd2WkxgIHtCPy0OH2E0ORp2clFNenFnAnppTkl5cmwMf3NXT3drOVooK04SNjc-GyU0BFoy",
                TournamentID = 1
            });
            context.SaveChanges();
        }
    }
}
