using System.Text.Json.Serialization;

namespace server.Models
{
    public class PerformanceReport
    {
        public int ReportId { get; set; } // Primary Key
        public int PlayerId { get; set; } // FK to Player
        public DateTime MatchDate { get; set; }
        public string Sport { get; set; } // Cricket, Football
        public string Tournament { get; set; }
        public string Opponent { get; set; } // Team or Player the match was against
        public string MatchType { get; set; } // T20, ODI, Test, League, etc.

        public int Stats1 { get; set; } // Batting
        public int Stats2 { get; set; } // Batting
        public int Stats3 { get; set; } // Bowling
        public int Stats4 { get; set; } // Bowling
        // Navigation Properties

        public double Rating { get; set; } // Performance rating out of 10
        public int AnalystId { get; set; }  // FK to User (Analyst) = 

        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]

        public User Analyst { get; set; }
    }
}
