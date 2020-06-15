namespace Pai.Models
{
    public class Sponsor
    {
        public int ID { get; set; }
        public int TournamentID { get; set; }
        public string ImageUrl { get; set; }

        public Tournament Tournament { get; set; }
    }
}
