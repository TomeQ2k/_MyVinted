import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Align } from 'src/app/enums/align.enum';
import { User } from 'src/app/models/domain/user';
import { Pagination } from 'src/app/models/helpers/pagination';
import { UsersRequest } from 'src/app/resolvers/requests/users-request';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users: User[];
  pagination: Pagination;

  private usersRequest: UsersRequest;

  private firstLoaded = true;

  align = Align;

  constructor(private userService: UserService, private route: ActivatedRoute, private notifier: Notifier, private listener: Listener) { }

  ngOnInit(): void {
    this.subscribeData();

    this.firstLoaded = false;
  }

  public onPageChanged(index: number) {
    this.listener.changeCurrentUsersRequest({ ...this.usersRequest, pageNumber: index });
  }

  private getUsers() {
    if (!this.firstLoaded) {
      this.userService.getUsers(this.usersRequest).subscribe(response => {
        this.users = response.result.users;
        this.pagination = response.pagination;
      }, error => this.notifier.push(error, 'error'));
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.users = data.usersResponse.result.users;
      this.pagination = data.usersResponse.pagination;

      this.listener.changeCurrentNavbarFormVisible(false);
      this.listener.currentUsersRequest.subscribe(request => {
        this.usersRequest = request;
        this.getUsers();
      });
    });
  }
}
