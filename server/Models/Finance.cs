using System.Text.Json.Serialization;

namespace server.Models
{
    public class Finance
    {

      
        public int FinanceId { get; set; }
        public int TeamId { get; set; } // FK to Team
        public string TransactionType { get; set; } // Purchase, Fee, etc.
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public Team Team { get; set; }
    }
}
