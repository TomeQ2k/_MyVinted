import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-checkout-summary',
  templateUrl: './checkout-summary.component.html',
  styleUrls: ['./checkout-summary.component.scss']
})
export class CheckoutSummaryComponent implements OnInit {
  orderId: string;

  isUpgradeAccount: boolean;

  constructor(private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.orderId = params.orderId;
      this.isUpgradeAccount = params.isUpgradeAccount ? params.isUpgradeAccount.toLowerCase() === 'true' : false;

      if (this.isUpgradeAccount) {
        this.authService.signOut();
      }
    });
  }
}
