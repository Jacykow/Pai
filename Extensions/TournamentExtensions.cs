using Pai.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pai.Extensions
{
    public static class TournamentExtensions
    {
        public static string EntryStatus(this Tournament tournament)
        {
            return tournament.AssignedPlayersAmount + "/" + tournament.EntryLimit;
        }

        public static string TimeFormatted(this Tournament tournament)
        {
            return tournament.Time.Value.ToString("dd.MM.yyyy");
        }
    }
}
