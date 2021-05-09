import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { arrayReducer } from 'src/app/helpers/array-reducer';
import { Opinion } from 'src/app/models/domain/opinion';
import { User } from 'src/app/models/domain/user';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user: User;

  currentUserId: string;

  constructor(private route: ActivatedRoute, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.userResponse.user;

      this.listener.changeCurrentNavbarFormVisible(false);
    });

    this.currentUserId = this.authService.currentUser.id;
  }

  public onOpinionAdded(response: { opinion: Opinion, newRating: number }) {
    this.user.opinions.unshift(response.opinion);
    this.user.rating = response.newRating;
  }

  public onOpinionDeleted(opinions: Opinion[]) {
    this.user.opinions = opinions;
    this.user.rating = this.calculateNewRating();
  }

  public onOfferDeleted(offerId: string) {
    this.user.offers = this.user.offers.filter(o => o.id !== offerId);
  }

  public currentUserOpinionExists = () => this.user.opinions.some(o => o.creatorId === this.currentUserId);

  private calculateNewRating = () => {
    const allRatings: number[] = this.user.opinions.map(o => o.rating);
    return this.user.opinions.length !== 0 ? allRatings.reduce(arrayReducer) / this.user.opinions.length : 0;
  };
}
