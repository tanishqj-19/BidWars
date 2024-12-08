import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../../services/player.service';
import { PerformanceReport } from '../../../models/PerformanceReport';
import { createPlayerPerformanceTrendChart, createTeamFinancialChart } from '../../Helper/AuctionCharts';
import { Chart, registerables } from 'chart.js';
import { Finance } from '../../../models/Finance';
import { TeamService } from '../../services/team.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  playerChart: Chart | null = null;
  playerChart2: Chart | null = null;
  reports: PerformanceReport[] = [];
  finances: Finance[] = [];

  constructor(private playerService: PlayerService, private teamService: TeamService) {}

  async ngOnInit(): Promise<void> {
    // Fetch performance reports and finances
    await this.playerPerformanceReports();
    await this.getTeamFinances();

    // Ensure data is loaded before creating chart
    if (this.finances.length > 0) {
      await Chart.register(...registerables);
      this.playerChart = createTeamFinancialChart(this.finances); // Create chart after data is available
    }

    if(this.reports.length > 0){
      this.playerChart2 = createPlayerPerformanceTrendChart(this.reports);
    }
  }

  async playerPerformanceReports(): Promise<void> {
    const data = await this.playerService.getPerformanceReport(40).toPromise();
    this.reports = data ?? [];
  }

  async getTeamFinances(): Promise<void> {
    const data = await this.teamService.getTeamFinances(2).toPromise();
    this.finances = data ?? [];
  }
}