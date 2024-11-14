import {Component, OnInit} from '@angular/core';
import {TemperatureClient, TemperatureEntryDto} from "../../services/apiClient/api.service";

@Component({
  selector: 'app-time-series',
  templateUrl: './time-series.component.html',
  styleUrl: './time-series.component.css'
})
export class TimeSeriesComponent implements OnInit {
  private readonly entriesPerPackage = 5;
  private readonly maximumTemperature = 30;
  private readonly historyTimeSpanMilliseconds = 300000;
  public temperatureEntryDtos: TemperatureEntryDto[] = [];
  public isLoadingScreenActive: boolean = false;

  public constructor(private temperatureClient: TemperatureClient) {
  }

  public ngOnInit(): void {
    this.refreshEntries();
  }

  public saveButtonClicked(): void {
    const entries: TemperatureEntryDto[] = this.generateEntriesPackage();
    this.temperatureClient.createBatch(entries)
      .subscribe({
        next: () => this.refreshEntries(),
        error: error => console.error(error),
      });
  }

  public clearButtonClicked(): void {
    const historyTimeLimit = new Date().getTime() - this.historyTimeSpanMilliseconds;
    this.temperatureClient.deleteRange(new Date(historyTimeLimit), new Date())
      .subscribe({
        next: () => this.refreshEntries(),
        error: error => console.error(error),
      });
  }

  private generateEntriesPackage(): TemperatureEntryDto[] {
    const time = new Date();
    const entries: TemperatureEntryDto[] = [];

    for (let i = 0; i < this.entriesPerPackage; i++) {
      entries.push({
        timestamp: time,
        temperature: Math.random() * this.maximumTemperature,
        source: `sensor ${i}`
      });
    }

    return entries;
  }

  private refreshEntries(): void {
    this.isLoadingScreenActive = true;
    const historyTimeLimit = new Date().getTime() - this.historyTimeSpanMilliseconds;
    this.temperatureClient.getRange(new Date(historyTimeLimit), new Date())
      .subscribe({
        next: result => {
          this.temperatureEntryDtos = result;
          this.isLoadingScreenActive = false;
        },
        error: error => {
          console.error(error);
          this.isLoadingScreenActive = false;
        },
      });
  }
}
