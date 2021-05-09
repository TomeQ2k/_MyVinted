import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserProfile } from 'src/app/models/domain/user-profile';
import { Listener } from 'src/app/services/listener.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {
  currentUser: UserProfile;

  constructor(private route: ActivatedRoute, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.currentUser = data.profileResponse.userProfile);
    this.listener.changeCurrentNavbarFormVisible(false);
  }
}
