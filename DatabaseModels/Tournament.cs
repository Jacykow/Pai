using System;
using System.Collections.Generic;

namespace Pai.DatabaseModels
{
    public partial class Tournament
    {
        public Tournament()
        {
            Sponsor = new HashSet<Sponsor>();
            TournamentUser = new HashSet<TournamentUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Discipline { get; set; }
        public DateTime? Time { get; set; }
        public string Location { get; set; }
        public int EntryLimit { get; set; }
        public DateTime? EntryDateLimit { get; set; }
        public int AssignedPlayersAmount { get; set; }

        public virtual ICollection<Sponsor> Sponsor { get; set; }
        public virtual ICollection<TournamentUser> TournamentUser { get; set; }
    }
}
