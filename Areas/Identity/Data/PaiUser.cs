using Microsoft.AspNetCore.Identity;
using Pai.DatabaseModels;
using System.Collections.Generic;

namespace Pai.Areas.Identity.Data
{
    public class PaiUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        public virtual ICollection<TournamentUser> TournamentUser { get; set; }
    }
}
