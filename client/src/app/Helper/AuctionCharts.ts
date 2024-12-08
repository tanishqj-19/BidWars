import { Chart, ChartConfiguration } from 'chart.js';
import { PerformanceReport } from '../../models/PerformanceReport';
import { Finance } from '../../models/Finance';
import { Player } from '../../models/Player';
import { Bid } from '../../models/Bid';


interface TeamBidSum {
    [teamId: number]: number;
  }
// 1. Player Performance Trend Chart
export function createPlayerPerformanceTrendChart(performanceReports: PerformanceReport[]) {
  // Group performance reports by match date and aggregate stats
  const sortedReports = performanceReports.sort((a, b) => 
    new Date(a.matchDate).getTime() - new Date(b.matchDate).getTime()
  );
  var label1 = "Runs Scored";
  var label2 = "Balls Faced";
  var label3 = "Wicket Taken";
  var label4 = "Catches";
  if(performanceReports[0].sport == "Football"){
    label1 = "Goals Scored";
    label2 = "Assists"
    label3 = "Passes Completed";
    label4 = "Tackles Made";
  }
  const lineConfig: ChartConfiguration = {
    type: 'line',
    data: {
      labels: sortedReports.map(report => 
        new Date(report.matchDate).toLocaleDateString()
      ),
      datasets: [
        {
          label: label1,
          data: sortedReports.map(report => report.stats1),
          borderColor: 'blue',
          tension: 0.1
        },
        {
          label: label2,
          data: sortedReports.map(report => report.stats2),
          borderColor: 'green',
          tension: 0.1
        },
        {
          label: label3,
          data: sortedReports.map(report => report.stats3),
          borderColor: 'red',
          tension: 0.1
        },
        {
            label: label4,
            data: sortedReports.map(report => report.stats4),
            borderColor: 'yellow',
            tension: 0.1
        }
      ]
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: 'Player Performance Trend'
        }
      },
      scales: {
        y: {
          beginAtZero: true
        }
      }
    }
  };
  return new Chart('playerPerformanceTrend', lineConfig);
}

// 2. Team Financial Overview Chart
export function createTeamFinancialChart(financeRecords: Finance[]) {
  // Categorize transactions and sum amounts
  const transactionTypes = ['Auction Fee', 'Player Purchase', 'Bid'];
  let transactionSums = [0, 0, 0];
  
  for (let i = 0; i < financeRecords.length; i++) {
    const record = financeRecords[i];
    // console.log(record.amount);
    if (record.transactionType.toLowerCase() == transactionTypes[0].toLowerCase())
        transactionSums[0] += record.amount;
      else if (record.transactionType.toLowerCase() == transactionTypes[1].toLowerCase())
        transactionSums[1] += record.amount;
      else
        transactionSums[2] += record.amount;
      
  }

//   console.log(transactionTypes);
//   console.log(transactionSums);

  const pieConfig: ChartConfiguration = {
    type: 'pie',
    data: {
      labels: transactionTypes,
      datasets: [{
        data: transactionSums,
        backgroundColor: [
          'rgba(255, 99, 132, 0.8)',
          'rgba(54, 162, 235, 0.8)',
          'rgba(255, 206, 86, 0.8)',
          'rgba(75, 192, 192, 0.8)'
        ]
      }]
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: 'Team Financial Transaction Overview'
        }
      }
    }
  };
  return new Chart('teamFinancialChart', pieConfig);
}

// 3. Bidding Intensity Chart
export function createBiddingIntensityChart(bids: Bid[]) {
  // Group bids by team and calculate total bid amounts
//   const teamBidSums: TeamBidSum = bids.reduce((acc: TeamBidSum, bid) => {
//     acc[bid.teamId] = (acc[bid.teamId] || 0) + bid.bidAmount;
//     return acc;
//   }, {});

    const teamBidSums = new Map<number, number>();

    // Populate the map
    bids.forEach(bid => {
    const currentTotal = teamBidSums.get(bid.teamId) || 0;
    teamBidSums.set(bid.teamId, currentTotal + bid.bidAmount);
    });



    const teamIds = Array.from(teamBidSums.keys());
    const bidAmounts = Array.from(teamBidSums.values());

  const barConfig: ChartConfiguration = {
    type: 'bar',
    data: {
      labels: teamIds,
      datasets: [{
        label: 'Total Bid Amount by Team',
        data: bidAmounts,
        backgroundColor: 'rgba(54, 162, 235, 0.6)'
      }]
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: 'Bidding Intensity Across Teams'
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: 'Total Bid Amount'
          }
        }
      }
    }
  };
  return new Chart('biddingIntensityChart', barConfig);
}

interface HourlyBidCount {
    [hour: number]: number;
}
// 4. Temporal Bidding Analysis
export function createBiddingTimeSeriesChart(bids: Bid[]) {
  // Group bids by time periods and track bid volumes
  const sortedBids = bids.sort((a, b) => 
    new Date(a.bidTime).getTime() - new Date(b.bidTime).getTime()
  );

  
  
  const hourlyBidCounts: HourlyBidCount = sortedBids.reduce((acc: HourlyBidCount, bid) => {
    const hour = new Date(bid.bidTime).getHours();
    acc[hour] = (acc[hour] || 0) + 1;
    return acc;
  }, {});

  const barConfig: ChartConfiguration = {
    type: 'bar',
    data: {
      labels: Object.keys(hourlyBidCounts),
      datasets: [{
        label: 'Bid Frequency by Hour',
        data: Object.values(hourlyBidCounts),
        backgroundColor: 'rgba(255, 99, 132, 0.6)'
      }]
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: 'Bidding Activity Throughout the Day'
        }
      },
      scales: {
        x: {
          title: {
            display: true,
            text: 'Hour of Day'
          }
        },
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: 'Number of Bids'
          }
        }
      }
    }
  };
  return new Chart('biddingTimeSeriesChart', barConfig);
}

