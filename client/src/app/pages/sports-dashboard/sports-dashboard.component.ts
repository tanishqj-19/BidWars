import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Chart, ChartType, registerables, TooltipItem } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { catchError, forkJoin, throwError } from 'rxjs';
import { TeamService } from '../../services/team.service';
import { PlayerService } from '../../services/player.service';
import { Player } from '../../../models/Player';

Chart.register(...registerables);

@Component({
  selector: 'app-sports-dashboard',
  standalone: true,
  imports: [
    CommonModule, 
    HttpClientModule, 
    BaseChartDirective
  ],
  templateUrl: './sports-dashboard.component.html'
})
export class SportsDashboardComponent implements OnInit {
  // Loading and Error States
  isLoading = true;
  errorMessage = '';

  // Chart Data and Options
  playerPerformanceData: any = { labels: [], datasets: [] };
  playerPerformanceOptions: any = {
    responsive: true,
    plugins: {
      title: { 
        display: true, 
        text: 'Player Performance by Sport' 
      },
      tooltip: {
        callbacks: {
          title: (context: TooltipItem<ChartType>[]) => {
            return context[0].label;
          },
          label: (context: TooltipItem<ChartType>) => {
            return `${context.dataset.label}: ${context.formattedValue}`;
          }
        }
      }
    },
    scales: {
      y: {
        title: {
          display: true,
          text: 'Average Performance Metric'
        }
      }
    }
  };

  teamExpenditureData: any = { labels: [], datasets: [] };
  teamExpenditureOptions: any = {
    responsive: true,
    plugins: {
      title: { display: true, text: 'Team Expenditure' }
    }
  };

  bidComparisonData: any = { labels: [], datasets: [] };
  bidComparisonOptions: any = {
    responsive: true,
    plugins: {
      title: { display: true, text: 'Bid Comparison' }
    }
  };

  financeBreakdownData: any = { labels: [], datasets: [] };
  financeBreakdownOptions: any = {
    responsive: true,
    plugins: {
      title: { display: true, text: 'Finance Breakdown' }
    }
  };

  teamBudgetData : any = { labels: [], datasets: [] };
  teamBudgetOptions : any = {}



  players: Player[] = []
  constructor(private playerService: PlayerService, private teamService : TeamService) {}

  ngOnInit() {
    this.fetchAllData();
  }

  fetchAllData() {
    // Simulate API calls - replace with actual service methods
    forkJoin({
      players : this.playerService.getAllPlayers(),
      playerReports: this.playerService.getAllPerformances(),
      teams: this.teamService.getAllTeams(),
      finances: this.teamService.getAllFinances(),
      bids: this.teamService.getAllBids()
    }).pipe(
      catchError(error => {
        this.errorMessage = 'Failed to load data. Please try again.';
        this.isLoading = false;
        return throwError(error);
      })
    ).subscribe(data => {
      this.preparePlayerPerformanceCharts(data.playerReports, data.players);
      this.prepareTeamExpenditureChart(data.teams, data.finances);
      this.prepareBidComparisonChart(data.bids);
      this.prepareFinanceBreakdownChart(data.finances);
      this.prepareTeamBudgetChart(data.teams);
      this.isLoading = false;
    });
  }


cricketPerformanceData : any = { labels: [], datasets: [] };
footballPerformanceData : any = { labels: [], datasets: [] };

preparePlayerPerformanceCharts(playerReports: any[], players: any[]) {
  // Create a mapping of playerId to player details
  const playerMap = players.reduce((acc, player) => {
      acc[player.playerId] = {
          name: player.name,
          sport: player.sport
      };
      return acc;
  }, {});

  // Separate players by sport
  const cricketPlayers = playerReports
      .filter(report => playerMap[report.playerId]?.sport === 'Cricket')
      .map(report => report.playerId);
  const footballPlayers = playerReports
      .filter(report => playerMap[report.playerId]?.sport === 'Football')
      .map(report => report.playerId);

  // Deduplicate playerIds
  const uniqueCricketPlayers = [...new Set(cricketPlayers)];
  const uniqueFootballPlayers = [...new Set(footballPlayers)];

  // Cricket Chart Data
  this.cricketPerformanceData = {
      labels: uniqueCricketPlayers.map(id => playerMap[id]?.name || `Player ${id}`),
      datasets: [
          {
              label: 'Runs Scored',
              data: uniqueCricketPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Cricket'
                  );
                  return reports.reduce((sum, report) => sum + report.stats1, 0);
              }),
              backgroundColor: 'rgba(75, 192, 192, 0.6)'
          },
          {
              label: 'Balls Played',
              data: uniqueCricketPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Cricket'
                  );
                  return reports.reduce((sum, report) => sum + report.stats2, 0);
              }),
              backgroundColor: 'rgba(255, 99, 132, 0.6)'
          },
          {
              label: 'Wickets Taken',
              data: uniqueCricketPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Cricket'
                  );
                  return reports.reduce((sum, report) => sum + report.stats3, 0);
              }),
              backgroundColor: 'rgba(54, 162, 235, 0.6)'
          },
          {
              label: 'Balls Thrown',
              data: uniqueCricketPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Cricket'
                  );
                  return reports.reduce((sum, report) => sum + report.stats4, 0);
              }),
              backgroundColor: 'rgba(255, 206, 86, 0.6)'
          }
      ]
  };

  // Football Chart Data
  this.footballPerformanceData = {
      labels: uniqueFootballPlayers.map(id => playerMap[id]?.name || `Player ${id}`),
      datasets: [
          {
              label: 'Goals Scored',
              data: uniqueFootballPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Football'
                  );
                  return reports.reduce((sum, report) => sum + report.stats1, 0);
              }),
              backgroundColor: 'rgba(153, 102, 255, 0.6)'
          },
          {
              label: 'Assists',
              data: uniqueFootballPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Football'
                  );
                  return reports.reduce((sum, report) => sum + report.stats2, 0);
              }),
              backgroundColor: 'rgba(255, 99, 132, 0.6)'
          },
          {
              label: 'Passes Completed',
              data: uniqueFootballPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Football'
                  );
                  return reports.reduce((sum, report) => sum + report.stats3, 0);
              }),
              backgroundColor: 'rgba(75, 192, 192, 0.6)'
          },
          {
              label: 'Tackles Made',
              data: uniqueFootballPlayers.map(playerId => {
                  const reports = playerReports.filter(report =>
                      report.playerId === playerId && playerMap[playerId]?.sport === 'Football'
                  );
                  return reports.reduce((sum, report) => sum + report.stats4, 0);
              }),
              backgroundColor: 'rgba(54, 162, 235, 0.6)'
          }
      ]
  };

  // Chart Options
  this.playerPerformanceOptions = {
      responsive: true,
      plugins: {
          title: { display: true, text: 'Player Performance Metrics' },
          tooltip: {
              callbacks: {
                  title: (context: any[]) => context[0].label,
                  label: (context: any) => `${context.dataset.label}: ${context.formattedValue}`
              }
          }
      },
      scales: {
          y: {
              title: { display: true, text: 'Performance Metric' }
          }
      }
  };
}



  prepareTeamExpenditureChart(teams: any[], finances: any[]) {
    const teamExpenditures = teams.map(team => {
      return {
        teamName: team.name,
        expenditure: team.totalExpenditure
      };
    });

    this.teamExpenditureData = {
      labels: teamExpenditures.map(te => te.teamName),
      datasets: [{
        data: teamExpenditures.map(te => te.expenditure),
        backgroundColor: [
          'rgba(255, 99, 132, 0.6)',
          'rgba(54, 162, 235, 0.6)',
          'rgba(255, 206, 86, 0.6)',
          'rgba(75, 192, 192, 0.6)'
        ]
      }]
    };
  }

  prepareBidComparisonChart(bids: any[]) {
    // Fetch the teams and map teamId to teamName
    this.teamService.getAllTeams().subscribe((teams: any[]) => {
        const teamNamesMap = teams.reduce((map, team) => {
            map[team.id] = team.name; // Assuming team object has `id` and `name` properties
            return map;
        }, {});

        // Calculate bid summary
        const teamBidSummary = bids.reduce((acc, bid) => {
            if (!acc[bid.teamId]) {
                acc[bid.teamId] = {
                    totalBidAmount: 0,
                    bidCount: 0
                };
            }
            acc[bid.teamId].totalBidAmount += bid.bidAmount;
            acc[bid.teamId].bidCount++;
            return acc;
        }, {});

        // Use the mapped team names for labels
        this.bidComparisonData = {
            labels: Object.keys(teamBidSummary).map(teamId => teamNamesMap[teamId] || `Team ${teamId}`),
            datasets: [
                {
                    label: 'Total Bid Amount',
                    data: Object.values(teamBidSummary).map((summary: any) => summary.totalBidAmount),
                    backgroundColor: 'rgba(75, 192, 192, 0.6)'
                }
            ]
        };
    });
}
  prepareFinanceBreakdownChart(finances: any[]) {
    const financeBreakdown = finances.reduce((acc, finance) => {
      if (!acc[finance.transactionType]) {
        acc[finance.transactionType] = 0;
      }
      acc[finance.transactionType] += finance.amount;
      return acc;
    }, {});

    this.financeBreakdownData = {
      labels: Object.keys(financeBreakdown),
      datasets: [{
        data: Object.values(financeBreakdown),
        backgroundColor: [
          'rgba(255, 99, 132, 0.6)',
          'rgba(54, 162, 235, 0.6)',
          'rgba(255, 206, 86, 0.6)',
          'rgba(75, 192, 192, 0.6)'
        ]
      }]
    };
  }




prepareTeamBudgetChart(teams: any[]) {
  // Prepare the data for the chart
  this.teamBudgetData = {
      labels: teams.map(team => team.name), // Team names as labels
      datasets: [
          {
              label: 'Team Budget',
              data: teams.map(team => team.budget), // Team budgets as data points
              backgroundColor: teams.map(() =>
                  `rgba(${Math.floor(Math.random() * 256)}, ${Math.floor(Math.random() * 256)}, ${Math.floor(Math.random() * 256)}, 0.6)`
              ) // Randomized colors for each team
          }
      ]
  };

  // Chart options
  this.teamBudgetOptions = {
      responsive: true,
      plugins: {
          title: {
              display: true,
              text: 'Team Budget Comparison'
          },
          tooltip: {
              callbacks: {
                  title: (context: any[]) => context[0].label,
                  label: (context: any) => `Budget: ${context.formattedValue}`
              }
          }
      },
      scales: {
          y: {
              beginAtZero: true,
              title: {
                  display: true,
                  text: 'Budget (in millions)'
              }
          }
      }
  };
  }
}

  // preparePlayerPerformanceChart(playerReports: any[], players: any[]) {
  //   // Create a mapping of playerId to player details
  //   const playerMap = players.reduce((acc, player) => {
  //     acc[player.playerId] = {
  //       name: player.name,
  //       sport: player.sport
  //     };
  //     return acc;
  //   }, {});
  
  //   // Get unique player IDs from reports
  //   const playerIds = [...new Set(playerReports.map(report => report.playerId))];
    
  //   // Prepare datasets based on sport
  //   const datasets = [];
  
  //   // Cricket Stats
  //   const cricketStatsDataset = {
  //     label: 'Runs Scored',
  //     data: playerIds
  //       .filter(playerId => playerMap[playerId]?.sport === 'Cricket')
  //       .map(playerId => {
  //         const playerReportsForPlayer = playerReports.filter(report => 
  //           report.playerId === playerId && report.sport === 'Cricket'
  //         );
  //         return playerReportsForPlayer.reduce((sum, report) => sum + report.stats1, 0)  || 0;
  //       }),
  //     backgroundColor: 'rgba(75, 192, 192, 0.6)'
  //   };
  
  //   const cricketBallsPlayedDataset = {
  //     label: 'Balls Played',
  //     data: playerIds
  //       .filter(playerId => playerMap[playerId]?.sport === 'Cricket')
  //       .map(playerId => {
  //         const playerReportsForPlayer = playerReports.filter(report => 
  //           report.playerId === playerId && report.sport === 'Cricket'
  //         );
  //         return playerReportsForPlayer.reduce((sum, report) => sum + report.stats2, 0);
  //       }),
  //     backgroundColor: 'rgba(255, 99, 132, 0.6)'
  //   };
  
  //   const cricketWicketsDataset = {
  //     label: 'Wickets Taken',
  //     data: playerIds
  //       .filter(playerId => playerMap[playerId]?.sport === 'Cricket')
  //       .map(playerId => {
  //         const playerReportsForPlayer = playerReports.filter(report => 
  //           report.playerId === playerId && report.sport === 'Cricket'
  //         );
  //         return playerReportsForPlayer.reduce((sum, report) => sum + report.stats3, 0);
  //       }),
  //     backgroundColor: 'rgba(54, 162, 235, 0.6)'
  //   };
  
  //   // Football Stats
  //   const footballGoalsDataset = {
  //     label: 'Goals Scored',
  //     data: playerIds
  //       .filter(playerId => playerMap[playerId]?.sport === 'Football')
  //       .map(playerId => {
  //         const playerReportsForPlayer = playerReports.filter(report => 
  //           report.playerId === playerId && report.sport === 'Football'
  //         );
  //         return playerReportsForPlayer.reduce((sum, report) => sum + report.stats1, 0);
  //       }),
  //     backgroundColor: 'rgba(255, 206, 86, 0.6)'
  //   };
  
  //   const footballAssistsDataset = {
  //     label: 'Assists',
  //     data: playerIds
  //       .filter(playerId => playerMap[playerId]?.sport === 'Football')
  //       .map(playerId => {
  //         const playerReportsForPlayer = playerReports.filter(report => 
  //           report.playerId === playerId && report.sport === 'Football'
  //         );
  //         return playerReportsForPlayer.reduce((sum, report) => sum + report.stats2, 0);
  //       }),
  //     backgroundColor: 'rgba(153, 102, 255, 0.6)'
  //   };
  
  //   // Combine datasets
  //   const cricketPlayers = playerIds.filter(playerId => playerMap[playerId]?.sport === 'Cricket');
  //   const footballPlayers = playerIds.filter(playerId => playerMap[playerId]?.sport === 'Football');
  
  //   this.playerPerformanceData = {
  //     labels: [
  //       ...cricketPlayers.map(id => playerMap[id]?.name || `Cricket Player ${id}`),
  //       ...footballPlayers.map(id => playerMap[id]?.name || `Football Player ${id}`)
  //     ],
  //     datasets: [
  //       cricketStatsDataset,
  //       cricketBallsPlayedDataset,
  //       cricketWicketsDataset,
  //       footballGoalsDataset,
  //       footballAssistsDataset
  //     ]
  //   };
  
  //   // Update chart options to be more descriptive
  //   this.playerPerformanceOptions = {
  //     responsive: true,
  //     plugins: {
  //       title: { 
  //         display: true, 
  //         text: 'Player Performance by Sport' 
  //       },
  //       tooltip: {
  //         callbacks: {
  //           title: (context : TooltipItem<ChartType>[]) => {
  //             return context[0].label;
  //           },
  //           label: (context : TooltipItem<ChartType>) => {
  //             return `${context.dataset.label}: ${context.formattedValue}`;
  //           }
  //         }
  //       }
  //     },
  //     scales: {
  //       y: {
  //         title: {
  //           display: true,
  //           text: 'Average Performance Metric'
  //         }
  //       }
  //     }
  //   };
  // }
  
//   preparePlayerPerformanceChart(playerReports: any[], players: any[]) {
//     // Create a mapping of playerId to player details
//     const playerMap = players.reduce((acc, player) => {
//         acc[player.playerId] = {
//             name: player.name,
//             sport: player.sport
//         };
//         return acc;
//     }, {});

//     // Get unique player IDs from reports
//     const playerIds = [...new Set(playerReports.map(report => report.playerId))];

//     // Prepare datasets based on sport
//     const datasets = [];

//     // Cricket Stats
//     const cricketStatsDataset = {
//         label: 'Runs Scored',
//         data: playerIds
//             .filter(playerId => playerMap[playerId]?.sport === 'Cricket')
//             .map(playerId => {
//                 const playerReportsForPlayer = playerReports.filter(report =>
//                     report.playerId === playerId && report.sport === 'Cricket'
//                 );
//                 return playerReportsForPlayer.reduce((sum, report) => sum + report.stats1, 0) || 0;
//             }),
//         backgroundColor: 'rgba(75, 192, 192, 0.6)'
//     };

//     // Football Stats
//     const footballGoalsDataset = {
//         label: 'Goals Scored',
//         data: playerIds
//             .filter(playerId => playerMap[playerId]?.sport === 'Football')
//             .map(playerId => {
//                 const playerReportsForPlayer = playerReports.filter(report =>
//                     report.playerId === playerId && report.sport === 'Football'
//                 );
//                 return playerReportsForPlayer.reduce((sum, report) => sum + report.stats1, 0) || 0;
//             }),
//         backgroundColor: 'rgba(255, 206, 86, 0.6)'
//     };

//     const footballAssistsDataset = {
//         label: 'Assists',
//         data: playerIds
//             .filter(playerId => playerMap[playerId]?.sport === 'Football')
//             .map(playerId => {
//                 const playerReportsForPlayer = playerReports.filter(report =>
//                     report.playerId === playerId && report.sport === 'Football'
//                 );
//                 return playerReportsForPlayer.reduce((sum, report) => sum + report.stats2, 0) || 0;
//             }),
//         backgroundColor: 'rgba(153, 102, 255, 0.6)'
//     };

//     // Combine datasets
//     const cricketPlayers = playerIds.filter(playerId => playerMap[playerId]?.sport === 'Cricket');
//     const footballPlayers = playerIds.filter(playerId => playerMap[playerId]?.sport === 'Football');

//     // Debug: Log filtered player data
//     console.log('Cricket Players:', cricketPlayers.map(id => playerMap[id]?.name));
//     console.log('Football Players:', footballPlayers.map(id => playerMap[id]?.name));

//     this.playerPerformanceData = {
//         labels: [
//             ...cricketPlayers.map(id => playerMap[id]?.name || `Cricket Player ${id}`),
//             ...footballPlayers.map(id => playerMap[id]?.name || `Football Player ${id}`)
//         ],
//         datasets: [
//             cricketStatsDataset,
//             footballGoalsDataset,
//             footballAssistsDataset
//         ]
//     };

//     // Update chart options to be more descriptive
//     this.playerPerformanceOptions = {
//         responsive: true,
//         plugins: {
//             title: { 
//                 display: true, 
//                 text: 'Player Performance by Sport' 
//             },
//             tooltip: {
//                 callbacks: {
//                     title: (context: any[]) => {
//                         return context[0].label;
//                     },
//                     label: (context: any) => {
//                         return `${context.dataset.label}: ${context.formattedValue}`;
//                     }
//                 }
//             }
//         },
//         scales: {
//             y: {
//                 title: {
//                     display: true,
//                     text: 'Performance Metric'
//                 }
//             }
//         }
//     };
// }