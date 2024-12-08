import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { AuctionComponent } from './pages/auction/auction.component';
import { ViewPlayersComponent } from './pages/view-players/view-players.component';
import { TeamsComponent } from './pages/teams/teams.component';
import { authGuard } from './services/auth.guard';
import { SingleTeamComponent } from './pages/single-team/single-team.component';
import { SinglePlayerComponent } from './pages/single-player/single-player.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';

export const routes: Routes = [

    {path: 'login', component: LoginComponent},
    {path: 'register', component : RegisterComponent },
    {path: 'auction', component : AuctionComponent, canActivate: [authGuard]},
    {path: 'players', component : ViewPlayersComponent, canActivate: [authGuard]},
    {path: 'teams', component: TeamsComponent, canActivate: [authGuard]},
    {path: 'teams/:id', component : SingleTeamComponent},
    {path: 'players/:id', component : SinglePlayerComponent},
    {path: 'dashboard', component : DashboardComponent, canActivate: [authGuard]},
    {path: '', component: HomeComponent},
    {path: '**',component : NotFoundComponent}
    
    
    
];
