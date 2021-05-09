import { Component, Input } from '@angular/core';
import { Align } from 'src/app/enums/align.enum';

@Component({
  selector: 'app-letters-limiter',
  templateUrl: './letters-limiter.component.html',
  styleUrls: ['./letters-limiter.component.scss']
})
export class LettersLimiterComponent {
  @Input() inputData: { count: number, maxCount: number };
  @Input() align: Align = Align.RIGHT;

  alignEnum = Align;
}
