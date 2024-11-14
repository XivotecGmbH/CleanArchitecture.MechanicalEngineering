import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {NotificationClient, NotificationDto} from "../../../services/apiClient/api.service";

@Component({
  selector: 'app-notification-details',
  templateUrl: './notification-details.component.html',
  styleUrl: './notification-details.component.css'
})
export class NotificationDetailsComponent {
  public currentNotification: NotificationDto = {} as NotificationDto;

  public constructor(private router: Router, private notificationClient: NotificationClient) {
    this.currentNotification = this.router.getCurrentNavigation()?.extras.state?.['currentNotification'];
  }

  public acknowledgeButtonClicked(): void {
    this.acknowledgeCurrentNotification();
  }

  public navigateBackButtonClicked(): void {
    this.router.navigate(['/notification']);
  }

  private acknowledgeCurrentNotification(): void {
    this.currentNotification.acknowledged = true;
    this.notificationClient.update(this.currentNotification)
      .subscribe({
        error: error => console.error(error)
      });
  }
}
