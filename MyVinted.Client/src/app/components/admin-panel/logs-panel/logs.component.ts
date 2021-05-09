import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Align } from 'src/app/enums/align.enum';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { LogModel } from 'src/app/models/domain/log-model';
import { Pagination } from 'src/app/models/helpers/pagination';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { LogsRequest } from 'src/app/resolvers/requests/logs-request';
import { Listener } from 'src/app/services/listener.service';
import { LogsService } from 'src/app/services/logs.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-logs-panel',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit, Validatable {
  logs: LogModel[];
  pagination: Pagination;

  private logsRequest = new LogsRequest();

  filterForm: FormGroup;

  private firstLoaded = true;

  maxDate: Date;
  align = Align;

  constructor(private logsService: LogsService, private notifier: Notifier, private formBuilder: FormBuilder, private route: ActivatedRoute,
    private listener: Listener) { }

  ngOnInit(): void {
    this.subscribeData();

    this.createFilterForm();
    this.listener.changeCurrentNavbarFormVisible(false);

    this.firstLoaded = false;
  }

  public filterLogs() {
    if (this.filterForm.valid && !this.firstLoaded) {
      this.logsRequest = Object.assign(this.logsRequest, this.filterForm.value);
      this.setTimesInLogsRequest();

      this.logsService.getLogs(this.logsRequest).subscribe(response => {
        this.logs = response.result.logs;
        this.pagination = response.pagination;
      }, error => this.notifier.push(error, 'error'));
    }
  }

  public clearFilters() {
    this.logsRequest = new LogsRequest();
    this.createFilterForm();
    this.filterLogs();
  }

  public onPageChanged(index: number) {
    this.logsRequest.pageNumber = index;
    this.filterLogs();
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.filterForm);

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.logs = data.logsResponse.result.logs;
      this.pagination = data.logsResponse.pagination;
    });

    this.maxDate = new Date(new Date());
    this.maxDate.setDate(this.maxDate.getDate() - 1);
  }

  private createFilterForm() {
    this.filterForm = this.formBuilder.group({
      date: [this.maxDate, [Validators.required]],
      message: [''],
      level: [null],
      requestMethod: [null],
      requestPath: [''],
      statusCode: [null],
      exception: [''],
      minTime: [null],
      maxTime: [null],
      sortType: [0]
    });
  }

  private setTimesInLogsRequest() {
    const dateNumber: number = this.filterForm.get('date').value;
    const minTimeNumber: number = this.filterForm.get('minTime').value;
    const maxTimeNumber: number = this.filterForm.get('maxTime').value;

    this.logsRequest.date = new Date(dateNumber);

    if (minTimeNumber) {
      this.logsRequest.minTime = new Date(this.logsRequest.date.toDateString() + ' ' + minTimeNumber);
    }

    if (maxTimeNumber) {
      this.logsRequest.maxTime = new Date(this.logsRequest.date.toDateString() + ' ' + maxTimeNumber);
    }
  }
}
