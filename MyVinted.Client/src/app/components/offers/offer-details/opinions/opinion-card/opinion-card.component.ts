import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Opinion } from 'src/app/models/domain/opinion';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OpinionService } from 'src/app/services/opinion.service';

@Component({
  selector: 'app-opinion-card',
  templateUrl: './opinion-card.component.html',
  styleUrls: ['./opinion-card.component.scss']
})
export class OpinionCardComponent implements OnInit {
  @Input() opinion: Opinion;

  @Output() opinionDeleted = new EventEmitter<string>();

  currentUserId: string;
  stars: number[] = [];

  constructor(private opinionService: OpinionService, private notifier: Notifier, private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser.id;
    this.insertStars();
  }

  public deleteOpinion() {
    this.opinionService.deleteOpinion(this.opinion.id, this.opinion.userId).subscribe(() => {
      this.notifier.push('Opinion deleted');
      this.opinionDeleted.emit(this.opinion.id);
    }, error => this.notifier.push(error, 'error'));
  }

  private insertStars() {
    for (let i = 1; i <= this.opinion.rating; i++) {
      this.stars.push(i);
    }
  }
}
