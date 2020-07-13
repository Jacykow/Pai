using Pai.Areas.Identity.Data;

namespace Pai.DatabaseModels
{
    public partial class TournamentUser
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string UserId { get; set; }
        public string LicenceNumber { get; set; }
        public int RankNumber { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual PaiUser IdentityUser { get; set; }
    }
}
