import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {NotificationClient, NotificationDto, NotificationTypeDto} from "../../services/apiClient/api.service";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit {
  private notifications: NotificationDto[] = [];
  public unconfirmedNotifications: NotificationDto[] = [];
  public confirmedNotifications: NotificationDto[] = [];

  public constructor(private router: Router, private notificationClient: NotificationClient) {
  }

  public ngOnInit(): void {
    this.getNotifications();
  }

  public acknowledgeButtonClicked(notificationDto: NotificationDto): void {
    this.acknowledgeNotification(notificationDto);
    this.sortNotifications();
  }

  public NavigateButtonClicked(notificationDto: NotificationDto): void {
    this.router.navigate(['/notificationdetail'], {state: {currentNotification: notificationDto}});
  }

  public getClassForNotification(notificationDto: NotificationDto): string {
    if (!notificationDto.acknowledged && notificationDto.type == NotificationTypeDto.Error) {
      return 'error-frame'
    }
    return 'notification-frame'
  }

  private acknowledgeNotification(notificationDto: NotificationDto): void {
    notificationDto.acknowledged = !notificationDto.acknowledged;

    this.notificationClient.update(notificationDto)
      .subscribe({
        error: error => console.error(error)
      });
  }

  private getNotifications(): void {
    this.notificationClient.getAll()
      .subscribe({
        next: result => {
          this.notifications = result;
          this.sortNotifications();
        },
        error: error => console.error(error)
      });
  }

  private sortNotifications(): void {
    this.unconfirmedNotifications = this.notifications.filter(notification => !notification.acknowledged)
      .sort((a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime());
    this.confirmedNotifications = this.notifications.filter(notification => notification.acknowledged)
      .sort((a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime());
  }
}
