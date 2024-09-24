namespace VooltAPI.Domain.Entities
{
    public class Ad
    {
        public int AdId { get; set; }
        public string AdDescription { get; set; }
        public DateTime AdCreationDate { get; set; }
        public AdStatusEnum AdStatus { get; set; }
        public decimal? AdBalance { get; set; }
        public string AdExternalId { get; set; }
        public int AdTotalLeads { get; set; }
    }
}
