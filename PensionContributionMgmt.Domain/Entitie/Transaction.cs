

namespace PensionContributionMgmt.Domain.Entitie
{
public  class Transaction
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public int RetryCount { get; set; } = 0;
    }
}
