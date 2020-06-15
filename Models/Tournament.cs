using System;
using System.Collections.Generic;

namespace Pai.Models
{
    public class Tournament
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Discipline { get; set; }
        public DateTime? Time { get; set; }
        public string? Location { get; set; }
        public int EntryLimit { get; set; }
        public DateTime? EntryDateLimit { get; set; }
        public int AssignedPlayersAmount { get; set; }
        public ICollection<Sponsor> Sponsors { get; set; }

        public string EntryStatus => AssignedPlayersAmount + "/" + EntryLimit;
        public string TimeFormatted => Time.Value.ToString("dd.MM.yyyy");
    }
}
