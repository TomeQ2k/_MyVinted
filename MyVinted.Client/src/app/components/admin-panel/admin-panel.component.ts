import { Component, OnInit } from '@angular/core';
import { Listener } from 'src/app/services/listener.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss']
})
export class AdminPanelComponent implements OnInit {

  constructor(private listener: Listener) { }

  ngOnInit(): void {
    this.listener.changeCurrentNavbarFormVisible(false);
  }
}
