using Microsoft.EntityFrameworkCore;
using server.Models;
using System.Drawing;

namespace server.Data
{
    public class SportsDbContext : DbContext
    {
        public SportsDbContext(DbContextOptions<SportsDbContext> options) : base(options) { }   
        

        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PerformanceReport> PerformanceReports{ get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AuctionResult> AuctionResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PerformanceReport>(entity=> entity.HasKey(p=> p.ReportId));



            modelBuilder.Entity<AuctionResult>(entity =>
            {
                entity.HasKey(ar => ar.ResultId);

                
                entity.HasOne(ar => ar.Player)
                    .WithMany()
                    .HasForeignKey(ar => ar.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict); 
                entity.HasOne(ar => ar.Auction)
                    .WithMany(a => a.AuctionResults)
                    .HasForeignKey(ar => ar.AuctionId)
                    .OnDelete(DeleteBehavior.Cascade);

               
                entity.HasOne(ar => ar.WinningTeam)
                    .WithMany()
                    .HasForeignKey(ar => ar.WinningTeamId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });


            modelBuilder.Entity<Bid>(entity =>
            {
                
                entity.HasOne(b => b.Team)
                    .WithMany(t => t.Bids)
                    .HasForeignKey(b => b.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);  
                entity.HasOne(b => b.Player)
                    .WithMany()
                    .HasForeignKey(b => b.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Team>(entity =>
            {
                
                entity.HasMany(t => t.Bids)
                    .WithOne(b => b.Team)
                    .HasForeignKey(b => b.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);  
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.ContractId);
                entity.HasOne(c => c.Player)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(a => a.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<Auction>(entity =>
            {
                
                entity.HasKey(a => a.AuctionId);

              
                entity.HasOne(a => a.Player)
                    .WithMany() 
                    .HasForeignKey(a => a.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict); 

                
                entity.HasOne(a => a.Auctioneer)
                    .WithMany()
                    .HasForeignKey(a => a.AuctioneerId)
                    .OnDelete(DeleteBehavior.Restrict); 
                entity.Property(a => a.Sport)
                    .IsRequired()
                    .HasMaxLength(50); 

                entity.Property(a => a.Status)
                    .IsRequired()
                    .HasMaxLength(20); 

                entity.Property(a => a.StartTime)
                    .IsRequired();

                entity.Property(a => a.EndTime)
                    .IsRequired();
            }); 

            modelBuilder.Entity<PerformanceReport>(entity =>
            {
                entity.HasOne(r => r.Analyst)
                .WithMany(a => a.PerformanceReports)
                .HasForeignKey(r  => r.AnalystId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Team>().HasData(
        // Cricket Teams
            new Team
            {
                TeamId = 1,
                Name = "Chennai Super Kings",
                Sport = "Cricket",
                ManagerId = 6,
                Budget = 1600000,
                Region = "India",
                RosterSize = 0,
                TotalExpenditure = 0
            },
            new Team
            {
                TeamId = 2,
                Name = "Royal Challengers Bangalore",
                Sport = "Cricket",
                ManagerId = 7,
                Budget = 1750000,
                Region = "India",
                RosterSize = 0,
                TotalExpenditure = 0
            },
            new Team
            {
                TeamId = 3,
                Name = "Mumbai Indians",
                Sport = "Cricket",
                ManagerId = 8,
                Budget = 1550000,
                Region = "India",
                RosterSize = 0,
                TotalExpenditure = 0
            },

            // Football Teams
            new Team
            {
                TeamId = 4,
                Name = "Real Madrid",
                Sport = "Football",
                ManagerId = 9,
                Budget = 1600000,
                Region = "Spain",
                RosterSize = 0,
                TotalExpenditure = 0
            },
            new Team
            {
                TeamId = 5,
                Name = "Manchester City",
                Sport = "Football",
                ManagerId = 10,
                Budget = 1650000,
                Region = "England",
                RosterSize = 0,
                TotalExpenditure = 0
            }
        );


            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "David Agent", Email = "davidagent@gmail.com", Password = "$2a$10$f8g/nLj9O/SibpWRhz3GjOJe4zXPvkGkz7k609g7BZXyntrcOP5By", Role = "Player Agent", IsActive = true }
                );

            modelBuilder.Entity<Player>().HasData(
                // Cricket Players
                new Player { PlayerId = 1, Name = "Virat Kohli", Sport = "Cricket", Age = 34, Country = "India", Position = "Batsman", BasePrice = 200000, Status = "Available", AgentId = 1 },
                new Player { PlayerId = 2, Name = "Rohit Sharma", Sport = "Cricket", Age = 36, Country = "India", Position = "Batsman", BasePrice = 180000, Status = "Available", AgentId = 2 },
                new Player { PlayerId = 3, Name = "Steve Smith", Sport = "Cricket", Age = 33, Country = "Australia", Position = "Batsman", BasePrice = 190000, Status = "Available", AgentId = 3 },
                new Player { PlayerId = 4, Name = "Kane Williamson", Sport = "Cricket", Age = 33, Country = "New Zealand", Position = "Batsman", BasePrice = 185000, Status = "Available", AgentId = 4 },
                new Player { PlayerId = 5, Name = "Jasprit Bumrah", Sport = "Cricket", Age = 30, Country = "India", Position = "Bowler", BasePrice = 150000, Status = "Available", AgentId = 5 },
                new Player { PlayerId = 6, Name = "Mitchell Starc", Sport = "Cricket", Age = 33, Country = "Australia", Position = "Bowler", BasePrice = 160000, Status = "Available", AgentId = 1 },
                new Player { PlayerId = 7, Name = "Trent Boult", Sport = "Cricket", Age = 34, Country = "New Zealand", Position = "Bowler", BasePrice = 155000, Status = "Available", AgentId = 2 },
                new Player { PlayerId = 8, Name = "Rashid Khan", Sport = "Cricket", Age = 25, Country = "Afghanistan", Position = "Bowler", BasePrice = 140000, Status = "Available", AgentId = 3 },
                new Player { PlayerId = 9, Name = "Jos Buttler", Sport = "Cricket", Age = 32, Country = "England", Position = "Wicketkeeper-Batsman", BasePrice = 175000, Status = "Available", AgentId = 4 },
                new Player { PlayerId = 10, Name = "MS Dhoni", Sport = "Cricket", Age = 41, Country = "India", Position = "Wicketkeeper-Batsman", BasePrice = 190000, Status = "Available", AgentId = 5 },
                new Player { PlayerId = 11, Name = "Ben Stokes", Sport = "Cricket", Age = 32, Country = "England", Position = "All-Rounder", BasePrice = 200000, Status = "Available", AgentId = 1 },
                new Player { PlayerId = 12, Name = "Ravindra Jadeja", Sport = "Cricket", Age = 35, Country = "India", Position = "All-Rounder", BasePrice = 180000, Status = "Available", AgentId = 2 },
                new Player { PlayerId = 13, Name = "Pat Cummins", Sport = "Cricket", Age = 30, Country = "Australia", Position = "All-Rounder", BasePrice = 170000, Status = "Available", AgentId = 3 },

                // Football Players
                new Player { PlayerId = 14, Name = "Lionel Messi", Sport = "Football", Age = 36, Country = "Argentina", Position = "Forward", BasePrice = 300000, Status = "Available", AgentId = 4 },
                new Player { PlayerId = 15, Name = "Cristiano Ronaldo", Sport = "Football", Age = 38, Country = "Portugal", Position = "Forward", BasePrice = 290000, Status = "Available", AgentId = 5 },
                new Player { PlayerId = 16, Name = "Neymar Jr.", Sport = "Football", Age = 31, Country = "Brazil", Position = "Forward", BasePrice = 280000, Status = "Available", AgentId = 1 },
                new Player { PlayerId = 17, Name = "Kevin De Bruyne", Sport = "Football", Age = 32, Country = "Belgium", Position = "Midfielder", BasePrice = 270000, Status = "Available", AgentId = 2 },
                new Player { PlayerId = 18, Name = "Kylian Mbappé", Sport = "Football", Age = 25, Country = "France", Position = "Forward", BasePrice = 295000, Status = "Available", AgentId = 3 },
                new Player { PlayerId = 19, Name = "Robert Lewandowski", Sport = "Football", Age = 35, Country = "Poland", Position = "Forward", BasePrice = 280000, Status = "Available", AgentId = 4 },
                new Player { PlayerId = 20, Name = "Erling Haaland", Sport = "Football", Age = 23, Country = "Norway", Position = "Forward", BasePrice = 320000, Status = "Available", AgentId = 5 },
                new Player { PlayerId = 21, Name = "Luka Modrić", Sport = "Football", Age = 38, Country = "Croatia", Position = "Midfielder", BasePrice = 250000, Status = "Available", AgentId = 1 },
                new Player { PlayerId = 22, Name = "Virgil van Dijk", Sport = "Football", Age = 32, Country = "Netherlands", Position = "Defender", BasePrice = 270000, Status = "Available", AgentId = 2 },
                new Player { PlayerId = 23, Name = "Pedri González", Sport = "Football", Age = 21, Country = "Spain", Position = "Midfielder", BasePrice = 240000, Status = "Available", AgentId = 3 },
                new Player { PlayerId = 24, Name = "Cole Palmer", Sport = "Football", Age = 22, Country = "England", Position = "Midfielder", BasePrice = 250000, Status = "Available", AgentId = 4 },
                new Player { PlayerId = 25, Name = "Vinicious Junior", Sport = "Football", Age = 24, Country = "Brazil", Position = "Forward", BasePrice = 300000, Status = "Available", AgentId = 5 }
                );

            modelBuilder.Entity<PerformanceReport>().HasData(
    // Virat Kohli - Cricket Performances
    new PerformanceReport
    {
        ReportId = 1,
        PlayerId = 1,
        MatchDate = new DateTime(2024, 1, 15),
        Sport = "Cricket",
        Tournament = "IPL",
        Opponent = "Royal Challengers Bangalore",
        MatchType = "T20",
        Stats1 = 75,  // Runs Scored
        Stats2 = 45,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.5,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 2,
        PlayerId = 1,
        MatchDate = new DateTime(2024, 2, 10),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Australia",
        MatchType = "ODI",
        Stats1 = 110,  // Runs Scored
        Stats2 = 95,   // Balls Played
        Stats3 = 0,    // Wickets Taken
        Stats4 = 0,    // Balls Thrown
        Rating = 9.2,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 3,
        PlayerId = 1,
        MatchDate = new DateTime(2024, 3, 5),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "England",
        MatchType = "Test",
        Stats1 = 85,  // Runs Scored
        Stats2 = 180, // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.0,
        AnalystId = 13
    },

    // Rohit Sharma - Cricket Performances
    new PerformanceReport
    {
        ReportId = 4,
        PlayerId = 2,
        MatchDate = new DateTime(2024, 1, 20),
        Sport = "Cricket",
        Tournament = "Domestic League",
        Opponent = "Mumbai Indians",
        MatchType = "T20",
        Stats1 = 65,  // Runs Scored
        Stats2 = 40,  // Balls Played
        Stats3 = 1,   // Wickets Taken
        Stats4 = 12,  // Balls Thrown
        Rating = 7.8,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 5,
        PlayerId = 2,
        MatchDate = new DateTime(2024, 2, 15),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "New Zealand",
        MatchType = "ODI",
        Stats1 = 95,  // Runs Scored
        Stats2 = 80,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.7,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 6,
        PlayerId = 2,
        MatchDate = new DateTime(2024, 3, 10),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "South Africa",
        MatchType = "Test",
        Stats1 = 70,  // Runs Scored
        Stats2 = 150, // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 7.5,
        AnalystId = 13
    },

    // Steve Smith - Cricket Performances
    new PerformanceReport
    {
        ReportId = 7,
        PlayerId = 3,
        MatchDate = new DateTime(2024, 1, 25),
        Sport = "Cricket",
        Tournament = "Big Bash League",
        Opponent = "Sydney Sixers",
        MatchType = "T20",
        Stats1 = 55,  // Runs Scored
        Stats2 = 35,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 7.5,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 8,
        PlayerId = 3,
        MatchDate = new DateTime(2024, 2, 20),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "India",
        MatchType = "ODI",
        Stats1 = 80,  // Runs Scored
        Stats2 = 70,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.3,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 9,
        PlayerId = 3,
        MatchDate = new DateTime(2024, 3, 15),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "West Indies",
        MatchType = "Test",
        Stats1 = 90,  // Runs Scored
        Stats2 = 200, // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.5,
        AnalystId = 13
    },

    // Kane Williamson - Cricket Performances
    new PerformanceReport
    {
        ReportId = 10,
        PlayerId = 4,
        MatchDate = new DateTime(2024, 1, 30),
        Sport = "Cricket",
        Tournament = "Super Smash",
        Opponent = "Auckland Aces",
        MatchType = "T20",
        Stats1 = 60,  // Runs Scored
        Stats2 = 45,  // Balls Played
        Stats3 = 1,   // Wickets Taken
        Stats4 = 12,  // Balls Thrown
        Rating = 8.0,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 11,
        PlayerId = 4,
        MatchDate = new DateTime(2024, 2, 25),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Pakistan",
        MatchType = "ODI",
        Stats1 = 85,  // Runs Scored
        Stats2 = 75,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.5,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 12,
        PlayerId = 4,
        MatchDate = new DateTime(2024, 3, 20),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "Bangladesh",
        MatchType = "Test",
        Stats1 = 75,  // Runs Scored
        Stats2 = 180, // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 7.8,
        AnalystId = 13
    },

    // Jasprit Bumrah - Cricket Performances
    new PerformanceReport
    {
        ReportId = 13,
        PlayerId = 5,
        MatchDate = new DateTime(2024, 1, 5),
        Sport = "Cricket",
        Tournament = "IPL",
        Opponent = "Chennai Super Kings",
        MatchType = "T20",
        Stats1 = 15,  // Runs Scored
        Stats2 = 10,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 8.7,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 14,
        PlayerId = 5,
        MatchDate = new DateTime(2024, 2, 5),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Sri Lanka",
        MatchType = "ODI",
        Stats1 = 10,  // Runs Scored
        Stats2 = 8,   // Balls Played
        Stats3 = 2,   // Wickets Taken
        Stats4 = 60,  // Balls Thrown
        Rating = 9.0,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 15,
        PlayerId = 5,
        MatchDate = new DateTime(2024, 3, 1),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "England",
        MatchType = "Test",
        Stats1 = 20,  // Runs Scored
        Stats2 = 15,  // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 120, // Balls Thrown
        Rating = 8.5,
        AnalystId = 13
    },
    new PerformanceReport
    {
        ReportId = 16,
        PlayerId = 6,
        MatchDate = new DateTime(2024, 1, 12),
        Sport = "Cricket",
        Tournament = "Big Bash League",
        Opponent = "Sydney Thunder",
        MatchType = "T20",
        Stats1 = 20,  // Runs Scored
        Stats2 = 15,  // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 8.5,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 17,
        PlayerId = 6,
        MatchDate = new DateTime(2024, 2, 8),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "India",
        MatchType = "ODI",
        Stats1 = 15,  // Runs Scored
        Stats2 = 10,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 60,  // Balls Thrown
        Rating = 8.7,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 18,
        PlayerId = 6,
        MatchDate = new DateTime(2024, 3, 3),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "England",
        MatchType = "Test",
        Stats1 = 25,  // Runs Scored
        Stats2 = 20,  // Balls Played
        Stats3 = 5,   // Wickets Taken
        Stats4 = 120, // Balls Thrown
        Rating = 9.0,
        AnalystId = 13
    },

    // Trent Boult - Cricket Performances
    new PerformanceReport
    {
        ReportId = 19,
        PlayerId = 7,
        MatchDate = new DateTime(2024, 1, 18),
        Sport = "Cricket",
        Tournament = "Super Smash",
        Opponent = "Canterbury Kings",
        MatchType = "T20",
        Stats1 = 15,  // Runs Scored
        Stats2 = 12,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 8.0,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 20,
        PlayerId = 7,
        MatchDate = new DateTime(2024, 2, 12),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Pakistan",
        MatchType = "ODI",
        Stats1 = 10,  // Runs Scored
        Stats2 = 8,   // Balls Played
        Stats3 = 2,   // Wickets Taken
        Stats4 = 55,  // Balls Thrown
        Rating = 8.3,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 21,
        PlayerId = 7,
        MatchDate = new DateTime(2024, 3, 7),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "South Africa",
        MatchType = "Test",
        Stats1 = 20,  // Runs Scored
        Stats2 = 15,  // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 110, // Balls Thrown
        Rating = 8.5,
        AnalystId = 13
    },

    // Rashid Khan - Cricket Performances
    new PerformanceReport
    {
        ReportId = 22,
        PlayerId = 8,
        MatchDate = new DateTime(2024, 1, 22),
        Sport = "Cricket",
        Tournament = "Afghanistan Premier League",
        Opponent = "Kabul Zwanan",
        MatchType = "T20",
        Stats1 = 35,  // Runs Scored
        Stats2 = 25,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 8.7,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 23,
        PlayerId = 8,
        MatchDate = new DateTime(2024, 2, 16),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Sri Lanka",
        MatchType = "ODI",
        Stats1 = 25,  // Runs Scored
        Stats2 = 20,  // Balls Played
        Stats3 = 2,   // Wickets Taken
        Stats4 = 48,  // Balls Thrown
        Rating = 8.5,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 24,
        PlayerId = 8,
        MatchDate = new DateTime(2024, 3, 11),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "Bangladesh",
        MatchType = "Test",
        Stats1 = 40,  // Runs Scored
        Stats2 = 30,  // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 96,  // Balls Thrown
        Rating = 9.0,
        AnalystId = 13
    },

    // Jos Buttler - Cricket Performances
    new PerformanceReport
    {
        ReportId = 25,
        PlayerId = 9,
        MatchDate = new DateTime(2024, 1, 25),
        Sport = "Cricket",
        Tournament = "IPL",
        Opponent = "Rajasthan Royals",
        MatchType = "T20",
        Stats1 = 80,  // Runs Scored
        Stats2 = 50,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.8,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 26,
        PlayerId = 9,
        MatchDate = new DateTime(2024, 2, 20),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Australia",
        MatchType = "ODI",
        Stats1 = 105, // Runs Scored
        Stats2 = 85,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 9.2,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 27,
        PlayerId = 9,
        MatchDate = new DateTime(2024, 3, 15),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "West Indies",
        MatchType = "Test",
        Stats1 = 75,  // Runs Scored
        Stats2 = 160, // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.0,
        AnalystId = 13
    },

    // MS Dhoni - Cricket Performances
    new PerformanceReport
    {
        ReportId = 28,
        PlayerId = 10,
        MatchDate = new DateTime(2024, 1, 7),
        Sport = "Cricket",
        Tournament = "IPL",
        Opponent = "Chennai Super Kings",
        MatchType = "T20",
        Stats1 = 50,  // Runs Scored
        Stats2 = 35,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.3,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 29,
        PlayerId = 10,
        MatchDate = new DateTime(2024, 2, 3),
        Sport = "Cricket",
        Tournament = "Domestic Match",
        Opponent = "Mumbai Indians",
        MatchType = "ODI",
        Stats1 = 65,  // Runs Scored
        Stats2 = 55,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.5,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 30,
        PlayerId = 10,
        MatchDate = new DateTime(2024, 3, 6),
        Sport = "Cricket",
        Tournament = "Legends League",
        Opponent = "India Maharajas",
        MatchType = "T20",
        Stats1 = 45,  // Runs Scored
        Stats2 = 30,  // Balls Played
        Stats3 = 0,   // Wickets Taken
        Stats4 = 0,   // Balls Thrown
        Rating = 8.0,
        AnalystId = 13
    },

    new PerformanceReport
    {
        ReportId = 31,
        PlayerId = 11,
        MatchDate = new DateTime(2024, 1, 14),
        Sport = "Cricket",
        Tournament = "The Hundred",
        Opponent = "Manchester Originals",
        MatchType = "T20",
        Stats1 = 65,  // Runs Scored
        Stats2 = 40,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 9.0,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 32,
        PlayerId = 11,
        MatchDate = new DateTime(2024, 2, 9),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Australia",
        MatchType = "ODI",
        Stats1 = 85,  // Runs Scored
        Stats2 = 70,  // Balls Played
        Stats3 = 2,   // Wickets Taken
        Stats4 = 48,  // Balls Thrown
        Rating = 9.2,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 33,
        PlayerId = 11,
        MatchDate = new DateTime(2024, 3, 4),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "India",
        MatchType = "Test",
        Stats1 = 75,  // Runs Scored
        Stats2 = 160, // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 120, // Balls Thrown
        Rating = 8.8,
        AnalystId = 13
    },

    // Ravindra Jadeja - Cricket Performances
    new PerformanceReport
    {
        ReportId = 34,
        PlayerId = 12,
        MatchDate = new DateTime(2024, 1, 19),
        Sport = "Cricket",
        Tournament = "IPL",
        Opponent = "Chennai Super Kings",
        MatchType = "T20",
        Stats1 = 45,  // Runs Scored
        Stats2 = 30,  // Balls Played
        Stats3 = 2,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 8.5,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 35,
        PlayerId = 12,
        MatchDate = new DateTime(2024, 2, 14),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "Pakistan",
        MatchType = "ODI",
        Stats1 = 55,  // Runs Scored
        Stats2 = 40,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 48,  // Balls Thrown
        Rating = 8.7,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 36,
        PlayerId = 12,
        MatchDate = new DateTime(2024, 3, 9),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "England",
        MatchType = "Test",
        Stats1 = 40,  // Runs Scored
        Stats2 = 80,  // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 96,  // Balls Thrown
        Rating = 8.3,
        AnalystId = 13
    },

    // Pat Cummins - Cricket Performances
    new PerformanceReport
    {
        ReportId = 37,
        PlayerId = 13,
        MatchDate = new DateTime(2024, 1, 23),
        Sport = "Cricket",
        Tournament = "Big Bash League",
        Opponent = "Sydney Sixers",
        MatchType = "T20",
        Stats1 = 25,  // Runs Scored
        Stats2 = 15,  // Balls Played
        Stats3 = 4,   // Wickets Taken
        Stats4 = 24,  // Balls Thrown
        Rating = 8.6,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 38,
        PlayerId = 13,
        MatchDate = new DateTime(2024, 2, 17),
        Sport = "Cricket",
        Tournament = "World Cup",
        Opponent = "New Zealand",
        MatchType = "ODI",
        Stats1 = 35,  // Runs Scored
        Stats2 = 25,  // Balls Played
        Stats3 = 3,   // Wickets Taken
        Stats4 = 60,  // Balls Thrown
        Rating = 8.8,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 39,
        PlayerId = 13,
        MatchDate = new DateTime(2024, 3, 12),
        Sport = "Cricket",
        Tournament = "Test Series",
        Opponent = "South Africa",
        MatchType = "Test",
        Stats1 = 40,  // Runs Scored
        Stats2 = 30,  // Balls Played
        Stats3 = 5,   // Wickets Taken
        Stats4 = 120, // Balls Thrown
        Rating = 9.0,
        AnalystId = 13
    }, new PerformanceReport
    {
        ReportId = 40,
        PlayerId = 14,
        MatchDate = new DateTime(2024, 1, 15),
        Sport = "Football",
        Tournament = "Leagues Cup",
        Opponent = "Inter Miami",
        MatchType = "League",
        Stats1 = 2,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 35,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 9.0,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 41,
        PlayerId = 14,
        MatchDate = new DateTime(2024, 2, 10),
        Sport = "Football",
        Tournament = "Coppa America",
        Opponent = "Brazil",
        MatchType = "International",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 45,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.7,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 42,
        PlayerId = 14,
        MatchDate = new DateTime(2024, 3, 5),
        Sport = "Football",
        Tournament = "Champions League",
        Opponent = "Real Madrid",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 40,  // Passes Completed
        Stats4 = 3,   // Tackles Made
        Rating = 8.5,
        AnalystId = 13
    },

    // Cristiano Ronaldo - Football Performances
    new PerformanceReport
    {
        ReportId = 43,
        PlayerId = 15,
        MatchDate = new DateTime(2024, 1, 20),
        Sport = "Football",
        Tournament = "Saudi Pro League",
        Opponent = "Al Nassr",
        MatchType = "League",
        Stats1 = 3,   // Goals Scored
        Stats2 = 0,   // Assists
        Stats3 = 25,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.8,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 44,
        PlayerId = 15,
        MatchDate = new DateTime(2024, 2, 15),
        Sport = "Football",
        Tournament = "Club World Cup",
        Opponent = "Al Hilal",
        MatchType = "International Cup",
        Stats1 = 2,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 30,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 9.0,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 45,
        PlayerId = 15,
        MatchDate = new DateTime(2024, 3, 10),
        Sport = "Football",
        Tournament = "Saudi Pro League",
        Opponent = "Al Ittihad",
        MatchType = "League",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 35,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.5,
        AnalystId = 13
    },

    // Neymar Jr. - Football Performances
    new PerformanceReport
    {
        ReportId = 46,
        PlayerId = 16,
        MatchDate = new DateTime(2024, 1, 25),
        Sport = "Football",
        Tournament = "Ligue 1",
        Opponent = "Paris Saint-Germain",
        MatchType = "League",
        Stats1 = 2,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 40,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 8.7,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 47,
        PlayerId = 16,
        MatchDate = new DateTime(2024, 2, 12),
        Sport = "Football",
        Tournament = "Copa Libertadores",
        Opponent = "Flamengo",
        MatchType = "International Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 45,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.5,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 48,
        PlayerId = 16,
        MatchDate = new DateTime(2024, 3, 7),
        Sport = "Football",
        Tournament = "Champions League",
        Opponent = "Barcelona",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 38,  // Passes Completed
        Stats4 = 3,   // Tackles Made
        Rating = 8.3,
        AnalystId = 13
    },

    // Kevin De Bruyne - Football Performances
    new PerformanceReport
    {
        ReportId = 49,
        PlayerId = 17,
        MatchDate = new DateTime(2024, 1, 18),
        Sport = "Football",
        Tournament = "Premier League",
        Opponent = "Manchester City",
        MatchType = "League",
        Stats1 = 0,   // Goals Scored
        Stats2 = 3,   // Assists
        Stats3 = 55,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 8.9,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 50,
        PlayerId = 17,
        MatchDate = new DateTime(2024, 2, 20),
        Sport = "Football",
        Tournament = "FA Cup",
        Opponent = "Chelsea",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 48,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.7,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 51,
        PlayerId = 17,
        MatchDate = new DateTime(2024, 3, 15),
        Sport = "Football",
        Tournament = "Champions League",
        Opponent = "Real Madrid",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 50,  // Passes Completed
        Stats4 = 3,   // Tackles Made
        Rating = 8.5,
        AnalystId = 13
    },

    // Kylian Mbappé - Football Performances
    new PerformanceReport
    {
        ReportId = 52,
        PlayerId = 18,
        MatchDate = new DateTime(2024, 1, 22),
        Sport = "Football",
        Tournament = "Ligue 1",
        Opponent = "Paris Saint-Germain",
        MatchType = "League",
        Stats1 = 2,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 35,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 9.0,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 53,
        PlayerId = 18,
        MatchDate = new DateTime(2024, 2, 16),
        Sport = "Football",
        Tournament = "Champions League",
        Opponent = "Barcelona",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 40,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 8.8,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 54,
        PlayerId = 18,
        MatchDate = new DateTime(2024, 3, 11),
        Sport = "Football",
        Tournament = "Coupe de France",
        Opponent = "Lyon",
        MatchType = "Cup",
        Stats1 = 3,   // Goals Scored
        Stats2 = 0,   // Assists
        Stats3 = 30,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 9.2,
        AnalystId = 13
    },

    // Robert Lewandowski - Football Performances
    new PerformanceReport
    {
        ReportId = 55,
        PlayerId = 19,
        MatchDate = new DateTime(2024, 1, 28),
        Sport = "Football",
        Tournament = "La Liga",
        Opponent = "Barcelona",
        MatchType = "League",
        Stats1 = 2,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 25,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.6,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 56,
        PlayerId = 19,
        MatchDate = new DateTime(2024, 2, 25),
        Sport = "Football",
        Tournament = "Copa del Rey",
        Opponent = "Real Madrid",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 30,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 8.4,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 57,
        PlayerId = 19,
        MatchDate = new DateTime(2024, 3, 16),
        Sport = "Football",
        Tournament = "Champions League",
        Opponent = "Manchester City",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 28,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.5,
        AnalystId = 13
    },

    // Erling Haaland - Football Performances
    new PerformanceReport
    {
        ReportId = 58,
        PlayerId = 20,
        MatchDate = new DateTime(2024, 1, 30),
        Sport = "Football",
        Tournament = "Premier League",
        Opponent = "Manchester United",
        MatchType = "League",
        Stats1 = 3,   // Goals Scored
        Stats2 = 0,   // Assists
        Stats3 = 20,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 9.3,
        AnalystId = 11
    },
    new PerformanceReport
    {
        ReportId = 59,
        PlayerId = 20,
        MatchDate = new DateTime(2024, 2, 21),
        Sport = "Football",
        Tournament = "FA Cup",
        Opponent = "Chelsea",
        MatchType = "Cup",
        Stats1 = 2,   // Goals Scored
        Stats2 = 1,   // Assists
        Stats3 = 25,  // Passes Completed
        Stats4 = 2,   // Tackles Made
        Rating = 9.1,
        AnalystId = 12
    },
    new PerformanceReport
    {
        ReportId = 60,
        PlayerId = 20,
        MatchDate = new DateTime(2024, 3, 13),
        Sport = "Football",
        Tournament = "Champions League",
        Opponent = "Real Madrid",
        MatchType = "Cup",
        Stats1 = 1,   // Goals Scored
        Stats2 = 2,   // Assists
        Stats3 = 30,  // Passes Completed
        Stats4 = 1,   // Tackles Made
        Rating = 8.9,
        AnalystId = 13
    },
    new PerformanceReport { ReportId = 70, PlayerId = 21, MatchDate = new DateTime(2023, 1, 15), Sport = "Football", Tournament = "UEFA Champions League", Opponent = "Real Madrid", MatchType = "Knockout", Stats1 = 1, Stats2 = 2, Stats3 = 50, Stats4 = 3, Rating = 8.5, AnalystId = 11 },
    new PerformanceReport { ReportId = 71, PlayerId = 21, MatchDate = new DateTime(2023, 2, 20), Sport = "Football", Tournament = "La Liga", Opponent = "Barcelona", MatchType = "League", Stats1 = 0, Stats2 = 1, Stats3 = 45, Stats4 = 4, Rating = 7.8, AnalystId = 12 },
    new PerformanceReport { ReportId = 72, PlayerId = 21, MatchDate = new DateTime(2023, 3, 10), Sport = "Football", Tournament = "Copa del Rey", Opponent = "Atletico Madrid", MatchType = "Semifinal", Stats1 = 1, Stats2 = 1, Stats3 = 55, Stats4 = 2, Rating = 8.3, AnalystId = 13 },
    new PerformanceReport { ReportId = 73, PlayerId = 21, MatchDate = new DateTime(2023, 4, 25), Sport = "Football", Tournament = "La Liga", Opponent = "Sevilla", MatchType = "League", Stats1 = 2, Stats2 = 0, Stats3 = 60, Stats4 = 5, Rating = 9.0, AnalystId = 11 },
    new PerformanceReport { ReportId = 74, PlayerId = 21, MatchDate = new DateTime(2023, 5, 30), Sport = "Football", Tournament = "UEFA Super Cup", Opponent = "Chelsea", MatchType = "Final", Stats1 = 1, Stats2 = 1, Stats3 = 48, Stats4 = 3, Rating = 8.7, AnalystId = 12 },

    // Player ID 22 - Virgil van Dijk (Football)
    new PerformanceReport { ReportId = 75, PlayerId = 22, MatchDate = new DateTime(2023, 1, 10), Sport = "Football", Tournament = "Premier League", Opponent = "Manchester United", MatchType = "League", Stats1 = 0, Stats2 = 0, Stats3 = 40, Stats4 = 10, Rating = 8.5, AnalystId = 13 },
    new PerformanceReport { ReportId = 76, PlayerId = 22, MatchDate = new DateTime(2023, 2, 15), Sport = "Football", Tournament = "UEFA Champions League", Opponent = "Real Madrid", MatchType = "Knockout", Stats1 = 0, Stats2 = 0, Stats3 = 42, Stats4 = 9, Rating = 8.2, AnalystId = 11 },
    new PerformanceReport { ReportId = 77, PlayerId = 22, MatchDate = new DateTime(2023, 3, 5), Sport = "Football", Tournament = "FA Cup", Opponent = "Chelsea", MatchType = "Quarterfinal", Stats1 = 0, Stats2 = 0, Stats3 = 45, Stats4 = 12, Rating = 8.7, AnalystId = 12 },
    new PerformanceReport { ReportId = 78, PlayerId = 22, MatchDate = new DateTime(2023, 4, 20), Sport = "Football", Tournament = "Premier League", Opponent = "Tottenham", MatchType = "League", Stats1 = 1, Stats2 = 0, Stats3 = 50, Stats4 = 8, Rating = 9.0, AnalystId = 13 },
    new PerformanceReport { ReportId = 79, PlayerId = 22, MatchDate = new DateTime(2023, 5, 25), Sport = "Football", Tournament = "UEFA Europa League", Opponent = "Sevilla", MatchType = "Final", Stats1 = 0, Stats2 = 0, Stats3 = 55, Stats4 = 7, Rating = 8.8, AnalystId = 11 },

    // Player ID 23 - Pedri González (Football)
    new PerformanceReport { ReportId = 80, PlayerId = 23, MatchDate = new DateTime(2023, 1, 5), Sport = "Football", Tournament = "La Liga", Opponent = "Real Madrid", MatchType = "League", Stats1 = 1, Stats2 = 3, Stats3 = 50, Stats4 = 2, Rating = 8.4, AnalystId = 12 },
    new PerformanceReport { ReportId = 81, PlayerId = 23, MatchDate = new DateTime(2023, 2, 25), Sport = "Football", Tournament = "Copa del Rey", Opponent = "Atletico Madrid", MatchType = "Semifinal", Stats1 = 0, Stats2 = 2, Stats3 = 40, Stats4 = 3, Rating = 8.1, AnalystId = 13 },
    new PerformanceReport { ReportId = 82, PlayerId = 23, MatchDate = new DateTime(2023, 3, 15), Sport = "Football", Tournament = "UEFA Champions League", Opponent = "Bayern Munich", MatchType = "Quarterfinal", Stats1 = 1, Stats2 = 1, Stats3 = 48, Stats4 = 2, Rating = 8.6, AnalystId = 11 },
    new PerformanceReport { ReportId = 83, PlayerId = 23, MatchDate = new DateTime(2023, 4, 10), Sport = "Football", Tournament = "La Liga", Opponent = "Valencia", MatchType = "League", Stats1 = 2, Stats2 = 0, Stats3 = 55, Stats4 = 1, Rating = 9.0, AnalystId = 12 },
    new PerformanceReport { ReportId = 84, PlayerId = 23, MatchDate = new DateTime(2023, 5, 20), Sport = "Football", Tournament = "UEFA Super Cup", Opponent = "Inter Milan", MatchType = "Final", Stats1 = 1, Stats2 = 2, Stats3 = 47, Stats4 = 2, Rating = 8.9, AnalystId = 13 },

    // Player ID 24 - Cole Palmer (Football)
    new PerformanceReport { ReportId = 85, PlayerId = 24, MatchDate = new DateTime(2023, 1, 7), Sport = "Football", Tournament = "Premier League", Opponent = "Manchester United", MatchType = "League", Stats1 = 2, Stats2 = 1, Stats3 = 35, Stats4 = 5, Rating = 8.8, AnalystId = 11 },
    new PerformanceReport { ReportId = 86, PlayerId = 24, MatchDate = new DateTime(2023, 2, 17), Sport = "Football", Tournament = "FA Cup", Opponent = "Liverpool", MatchType = "Semifinal", Stats1 = 0, Stats2 = 3, Stats3 = 40, Stats4 = 4, Rating = 8.5, AnalystId = 12 },
    new PerformanceReport { ReportId = 87, PlayerId = 24, MatchDate = new DateTime(2023, 3, 22), Sport = "Football", Tournament = "Carabao Cup", Opponent = "Arsenal", MatchType = "Final", Stats1 = 1, Stats2 = 2, Stats3 = 42, Stats4 = 3, Rating = 8.7, AnalystId = 13 },
    new PerformanceReport { ReportId = 88, PlayerId = 24, MatchDate = new DateTime(2023, 4, 15), Sport = "Football", Tournament = "Premier League", Opponent = "Chelsea", MatchType = "League", Stats1 = 2, Stats2 = 1, Stats3 = 45, Stats4 = 2, Rating = 9.1, AnalystId = 11 },
    new PerformanceReport { ReportId = 89, PlayerId = 24, MatchDate = new DateTime(2023, 5, 27), Sport = "Football", Tournament = "UEFA Europa League", Opponent = "Sevilla", MatchType = "Final", Stats1 = 1, Stats2 = 2, Stats3 = 50, Stats4 = 1, Rating = 8.9, AnalystId = 12 },

    // Player ID 25 - Vinicius Junior (Football)
    new PerformanceReport
    {
        ReportId = 90,
        PlayerId = 25,
        MatchDate = new DateTime

(2023, 1, 12),
        Sport = "Football",
        Tournament = "La Liga",
        Opponent = "Barcelona",
        MatchType = "League",
        Stats1 = 2,
        Stats2 = 0,
        Stats3 = 60,
        Stats4 = 2,
        Rating = 9.3,
        AnalystId = 13
    },
    new PerformanceReport { ReportId = 91, PlayerId = 25, MatchDate = new DateTime(2023, 2, 23), Sport = "Football", Tournament = "UEFA Champions League", Opponent = "Manchester City", MatchType = "Semifinal", Stats1 = 1, Stats2 = 1, Stats3 = 58, Stats4 = 3, Rating = 8.7, AnalystId = 11 },
    new PerformanceReport { ReportId = 92, PlayerId = 25, MatchDate = new DateTime(2023, 3, 14), Sport = "Football", Tournament = "Copa del Rey", Opponent = "Valencia", MatchType = "Final", Stats1 = 2, Stats2 = 2, Stats3 = 62, Stats4 = 4, Rating = 9.5, AnalystId = 12 },
    new PerformanceReport { ReportId = 93, PlayerId = 25, MatchDate = new DateTime(2023, 4, 18), Sport = "Football", Tournament = "La Liga", Opponent = "Sevilla", MatchType = "League", Stats1 = 1, Stats2 = 1, Stats3 = 54, Stats4 = 2, Rating = 8.9, AnalystId = 13 },
    new PerformanceReport { ReportId = 94, PlayerId = 25, MatchDate = new DateTime(2023, 5, 11), Sport = "Football", Tournament = "UEFA Super Cup", Opponent = "PSG", MatchType = "Final", Stats1 = 3, Stats2 = 1, Stats3 = 68, Stats4 = 1, Rating = 9.7, AnalystId = 11 }



);


        }

    }
}
