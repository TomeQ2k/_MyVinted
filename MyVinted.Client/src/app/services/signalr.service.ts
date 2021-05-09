import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ConnectionManager } from './connection-manager.service';

import * as signalr from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class Signalr {
  private static connectionId: string;

  private readonly hubApiUrl = environment.signalRUrl;
  private hubConnections: { connection: signalr.HubConnection, hubName: string }[] = [];

  constructor(private connectionManager: ConnectionManager) { }

  public startConnection = (hubName: string) => {
    this.hubConnections.push(
      {
        connection: new signalr.HubConnectionBuilder()
          .withUrl(this.hubApiUrl + hubName)
          .build(),
        hubName: hubName
      }
    );

    this.hubConnections.find(c => c.hubName === hubName)?.connection.start()
      .then(() => {
        this.initConnection(hubName);
        if (!environment.production) {
          console.log('SignalR: Connection started...');
        }
      })
      .catch(error => console.error('SignalR: ', error));
  }

  public subscribeAction = (actionName: string, hubName: string, action: (value?: any) => void) => {
    const hubConnectionIndex = this.hubConnections.findIndex(c => c.hubName.toUpperCase() === hubName.toUpperCase());
    if (this.hubConnections[hubConnectionIndex]) {
      this.hubConnections[hubConnectionIndex]?.connection.on(actionName, action);
    }
  }

  public closeConnection = (hubName: string) => {
    if (hubName) {
      this.hubConnections.find(c => c.hubName.toUpperCase() === hubName.toUpperCase())?.connection.stop();
    } else {
      this.hubConnections.forEach(c => c.connection.stop());
    }

    if (!environment.production) {
      console.log('SignalR: Connection closed...');
    }
  }

  private initConnection = (hubName: string) => {
    this.hubConnections.find(c => c.hubName.toUpperCase() === hubName.toUpperCase())?.connection.invoke(SIGNALR_ACTIONS.GET_CONNECTION_ID)
      .then((connectionId) => {
        Signalr.connectionId = connectionId;
        this.createConnection(hubName);
      });
  }

  private createConnection = (hubName: string) => {
    this.connectionManager.startConnection(Signalr.connectionId, hubName).subscribe(() => {
      if (!environment.production) {
        console.log('SignalR: Connection established and persisted in database...');
      }
    });
  }
}

export const SIGNALR_ACTIONS = {
  NOTIFICATION_RECEIVED: 'NotificationReceived',
  MESSAGE_RECEIVED: 'MessageReceived',
  GET_CONNECTION_ID: 'GetConnectionId'
};
