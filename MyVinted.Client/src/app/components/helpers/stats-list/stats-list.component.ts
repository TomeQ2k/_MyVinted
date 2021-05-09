import { Component, Input } from '@angular/core';
import { StatsType } from 'src/app/enums/stats-type.enum';
import { StatsModel } from 'src/app/models/helpers/stats-model';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-stats-list',
  templateUrl: './stats-list.component.html',
  styleUrls: ['./stats-list.component.scss']
})
export class StatsListComponent {
  @Input() statsModel: StatsModel;
  @Input() rating?: number;
  @Input() statsType: StatsType = StatsType.Premium;

  type = StatsType;

  moneyMultiplier = constants.moneyMultiplier;
}
