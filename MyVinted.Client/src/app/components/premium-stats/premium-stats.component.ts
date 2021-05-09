import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StatsModel } from 'src/app/models/helpers/stats-model';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-premium-stats',
  templateUrl: './premium-stats.component.html',
  styleUrls: ['./premium-stats.component.scss']
})
export class PremiumStatsComponent implements OnInit {
  statsModel: StatsModel;
  rating: number;

  moneyMultiplier = constants.moneyMultiplier;

  constructor(private route: ActivatedRoute, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.statsModel = data.statsResponse.statsModel);
    this.rating = this.authService.currentUser.rating;
    this.listener.changeCurrentNavbarFormVisible(false);
  }
}
