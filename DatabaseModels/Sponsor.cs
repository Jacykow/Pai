using System;
using System.Collections.Generic;

namespace Pai.DatabaseModels
{
    public partial class Sponsor
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string ImageUrl { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
