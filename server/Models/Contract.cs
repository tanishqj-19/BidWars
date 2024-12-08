namespace server.Models
{
    public class Contract
    {
        public string ContractId { get; set; } = Guid.NewGuid().ToString();
        public int PlayerId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Salary { get; set; }
        public decimal Bonuses { get; set; }
        public string Details { get; set; }

        // Navigation Properties
        public Player Player { get; set; }

    }
}
