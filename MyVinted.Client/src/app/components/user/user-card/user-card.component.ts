import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/domain/user';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { UserService } from 'src/app/services/user.service';
import { roles } from 'src/environments/environment';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {
  @Input() user: User;
  @Input() isDetails: boolean;

  currentUserId: string;

  writeMessageMode: boolean = false;

  constructor(private userService: UserService, private notifier: Notifier, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser.id;
  }

  public followUser() {
    if (this.currentUserId !== this.user.id) {
      this.userService.followUser(this.user.id).subscribe(res => {
        const response: any = res.body;

        if (response.isFollowed) {
          this.notifier.push('User followed');
          this.user.followsCount++;
          this.user.followings.push(response.follow);
        } else {
          this.notifier.push('User unfollowed');
          this.user.followsCount--;
          this.user.followings = this.user.followings.filter(f => f.followerId !== this.currentUserId);
        }
      }, error => this.notifier.push(error, 'error'));
    }
  }

  public onMessageSent() {
    this.router.navigate(['/messenger/', this.user.id]);
  }

  public toggleBlockStatus() {
    this.userService.toggleBlockAccount(this.user.id).subscribe(() => {
      this.user.isBlocked = !this.user.isBlocked;
      this.notifier.push(this.user.isBlocked ? 'User has been blocked' : 'User has been unblocked', this.user.isBlocked ? 'warning' : 'success');
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public toggleWriteMessageMode = () => this.writeMessageMode = !this.writeMessageMode;

  public isFollowed = () => this.user.followings.some(f => f.followerId === this.currentUserId);

  public canToggleBlockAccount = () => this.authService.checkPermissions(roles.admin) && !this.user.isAdmin;
}
