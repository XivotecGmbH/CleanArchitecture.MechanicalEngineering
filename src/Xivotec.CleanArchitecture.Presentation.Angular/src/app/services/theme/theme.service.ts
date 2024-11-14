import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private isThemeDark: boolean;

  public constructor() {
    const storedTheme: string | null = localStorage.getItem("isThemeDark");

    if (storedTheme !== null) {
      this.isThemeDark = storedTheme === 'true';
    } else {
      this.isThemeDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    }

    this.applyTheme();
  }

  public getDarkThemeState(): boolean {
    return this.isThemeDark;
  }

  public toggleTheme(): void {
    this.isThemeDark = !this.isThemeDark;
    localStorage.setItem('isThemeDark', this.isThemeDark.toString());

    this.applyTheme();
  }

  private applyTheme(): void {
    if (this.isThemeDark) {
      document.body.classList.add('dark-theme');
    } else {
      document.body.classList.remove('dark-theme');
    }
  }
}
