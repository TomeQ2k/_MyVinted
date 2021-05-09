
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/models/domain/order';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OrderService } from 'src/app/services/order.service';
@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.scss']
})
export class UserOrdersComponent implements OnInit {
  orders: Order[];

  sortForm: FormGroup;

  constructor(private orderService: OrderService, private route: ActivatedRoute, private formBuilder: FormBuilder,
    private notifier: Notifier, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.orders = data.ordersResponse.orders;
      this.listener.changeCurrentFilteredOrders(this.orders);
    });

    this.createSortForm();
    this.listener.changeCurrentNavbarFormVisible(false);
  }

  public onSortChanged() {
    this.orderService.getUserOrders(this.sortForm.get('sortType').value).subscribe(response => {
      if (response.isSucceeded) {
        this.orders = response.orders;
        this.listener.changeCurrentFilteredOrders(this.orders);
      }
    }, error => this.notifier.push(error, 'error'));
  }

  private createSortForm() {
    this.sortForm = this.formBuilder.group({
      sortType: [0]
    });
  }
}
