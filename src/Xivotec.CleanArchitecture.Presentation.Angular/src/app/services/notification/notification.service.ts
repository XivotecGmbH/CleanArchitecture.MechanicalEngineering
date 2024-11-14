import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Subject} from "rxjs";
import {NotificationDto} from "../apiClient/api.service";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationHubAddress: string = 'https://localhost:4200/api/hubs/notification';
  private hubConnection: HubConnection = {} as HubConnection;
  public notificationSubject = new Subject<string>();

  public startConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.notificationHubAddress)
      .build();

    this.hubConnection.start();

    this.hubConnection
      .on("NotificationMessage", (notification: NotificationDto) => {
        this.notificationSubject.next(notification.title);
      });
  }
}
