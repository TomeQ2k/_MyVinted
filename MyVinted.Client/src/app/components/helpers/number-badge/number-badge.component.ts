import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-number-badge',
  templateUrl: './number-badge.component.html',
  styleUrls: ['./number-badge.component.scss']
})
export class NumberBadgeComponent {
  @Input() number: number;
}
