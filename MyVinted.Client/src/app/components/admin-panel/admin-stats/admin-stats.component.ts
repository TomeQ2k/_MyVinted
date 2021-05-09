import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StatsType } from 'src/app/enums/stats-type.enum';
import { StatsModel } from 'src/app/models/helpers/stats-model';
import { Listener } from 'src/app/services/listener.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-admin-stats',
  templateUrl: './admin-stats.component.html',
  styleUrls: ['./admin-stats.component.scss']
})
export class AdminStatsComponent implements OnInit {
  statsModel: StatsModel;

  type = StatsType;

  moneyMultiplier = constants.moneyMultiplier;

  constructor(private route: ActivatedRoute, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.statsModel = data.statsResponse.statsModel);
    this.listener.changeCurrentNavbarFormVisible(false);
  }
}
