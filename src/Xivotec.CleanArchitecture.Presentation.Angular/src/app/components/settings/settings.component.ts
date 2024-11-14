import {Component, OnInit} from '@angular/core';
import {SettingsClient} from "../../services/apiClient/api.service";
import {ThemeService} from "../../services/theme/theme.service";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent implements OnInit {
  public isDarkMode: boolean = false;
  public dbHost: string = '';
  public dbPort: string = '';
  public dbName: string = '';

  public constructor(private settingsClient: SettingsClient, private themeService: ThemeService) {
  }

  public ngOnInit(): void {
    this.GetAll();
    this.isDarkMode = this.themeService.getDarkThemeState();
  }

  private GetAll(): void {
    this.settingsClient.get().subscribe({
      next: result => {
        this.dbHost = result[0];
        this.dbPort = result[1];
        this.dbName = result[2];
      },
      error: error => console.error(error),
    });
  }

  public toggleTheme(): void {
    this.themeService.toggleTheme();
  }
}
