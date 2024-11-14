import {Component, OnInit} from '@angular/core';
import {NotificationService} from "../services/notification/notification.service";
import {ThemeService} from "../services/theme/theme.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public notificationText: string = 'No Notifications';

  public constructor(private notificationService: NotificationService, private themeService: ThemeService) {
  }

  public ngOnInit(): void {
    this.notificationService.startConnection();
    this.notificationService.notificationSubject.subscribe({
      next: (message) => {
        this.notificationText = 'Notification: ' + message;
      }
    });
  }
}
