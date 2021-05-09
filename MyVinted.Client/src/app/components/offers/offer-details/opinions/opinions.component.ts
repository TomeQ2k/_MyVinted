import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Opinion } from 'src/app/models/domain/opinion';

@Component({
  selector: 'app-opinions',
  templateUrl: './opinions.component.html',
  styleUrls: ['./opinions.component.scss']
})
export class OpinionsComponent {
  @Input() opinions: Opinion[];

  @Output() opinionDeleted = new EventEmitter<Opinion[]>();

  public onOpinionDeleted(opinionId: string) {
    this.opinions = this.opinions.filter(o => o.id !== opinionId);
    this.opinionDeleted.emit(this.opinions);
  }
}
