import { Component, Input, OnInit } from '@angular/core';
import { OrderType } from 'src/app/enums/order-type.enum';
import { Order } from 'src/app/models/domain/order';
import { Listener } from 'src/app/services/listener.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  @Input() orders: Order[];
  @Input() adminView: boolean;

  moneyMultiplier = constants.moneyMultiplier;

  constructor(private listener: Listener) { }

  ngOnInit(): void {
    this.listener.currentFilteredOrders.subscribe(orders => this.orders = orders);
  }

  public getItemType = (type: OrderType) => {
    switch (type) {
      case OrderType.Offer: return 'OFFER';
      case OrderType.Premium: return 'PREMIUM'
      default: return 'OFFER';
    }
  };
}
