import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Filterable } from 'src/app/helpers/interfaces/filterable';
import { UsersRequest } from 'src/app/resolvers/requests/users-request';
import { Listener } from 'src/app/services/listener.service';

@Component({
  selector: 'app-users-filters-form',
  templateUrl: './users-filters-form.component.html',
  styleUrls: ['./users-filters-form.component.scss']
})
export class UsersFiltersFormComponent implements OnInit, Filterable {
  filterForm: FormGroup;

  private usersRequest: UsersRequest;

  constructor(private formBuilder: FormBuilder, private listener: Listener) { }

  ngOnInit(): void {
    this.createFilterForm();
    this.listener.currentUsersRequest.subscribe(request => this.usersRequest = request);
  }

  public emitFilters() {
    this.usersRequest = Object.assign(this.usersRequest, this.filterForm.value);
    this.listener.changeCurrentUsersRequest(this.usersRequest);
  }

  public clearFilters() {
    this.usersRequest = new UsersRequest();
    this.createFilterForm();

    this.listener.changeCurrentUsersRequest(this.usersRequest);
  }

  private createFilterForm() {
    this.filterForm = this.formBuilder.group({
      name: [''],
      followStatus: [0],
      sortType: [0],
      onlyVerified: [false]
    });
  }
}
