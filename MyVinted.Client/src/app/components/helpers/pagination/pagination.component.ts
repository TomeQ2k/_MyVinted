import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Align } from 'src/app/enums/align.enum';
import { Pagination } from 'src/app/models/helpers/pagination';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent {
  @Input() items: any[] = [];
  @Input() pagination: Pagination;

  @Input() align: Align;
  @Input() displayCurrentPageInfo: boolean;
  @Input() displayPageSizeInfo: boolean;
  @Input() displayTotalPagesInfo: boolean;
  @Input() displayTotalCountInfo: boolean;

  @Output() pageChanged = new EventEmitter<number>();

  alignEnum = Align;

  public page(index: number) {
    if (this.pagination.totalPages >= index) {
      this.pagination.currentPage = index;
      this.pageChanged.emit(this.pagination.currentPage);
    }
  }

  public nextPage() {
    if (this.pagination.currentPage + 1 <= this.pagination.totalPages) {
      this.pagination.currentPage++;
      this.pageChanged.emit(this.pagination.currentPage);
    }
  }

  public previousPage() {
    if (this.pagination.currentPage - 1 >= 1) {
      this.pagination.currentPage--;
      this.pageChanged.emit(this.pagination.currentPage);
    }
  }

  public firstPage() {
    this.pagination.currentPage = 1;
    this.pageChanged.emit(this.pagination.currentPage);
  }

  public lastPage() {
    this.pagination.currentPage = this.pagination.totalPages;
    this.pageChanged.emit(this.pagination.currentPage);
  }
}
