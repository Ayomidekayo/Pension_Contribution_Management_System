

namespace PensionContributionMgmt.Domain.Entitie
{
  public  class Benefit
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public decimal AccruedInterest { get; set; }
        public decimal TotalBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
