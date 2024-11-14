import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DeviceControlService {
  private readonly deviceConnectionStateHubAddress: string = "https://localhost:4200/api/hubs/device/connectionstate";
  private readonly deviceDistanceDataHubAddress: string = "https://localhost:4200/api/hubs/device/distance";
  private readonly deviceTemperatureDataHubAddress: string = "https://localhost:4200/api/hubs/device/temperature";
  private readonly deviceLumenDataHubAddress: string = "https://localhost:4200/api/hubs/device/lumen";

  private deviceConnectionStateHubConnection: HubConnection = {} as HubConnection;
  private deviceDistanceDataHubConnection: HubConnection = {} as HubConnection;
  private deviceTemperatureDataHubConnection: HubConnection = {} as HubConnection;
  private deviceLumenDataHubConnection: HubConnection = {} as HubConnection;

  public connectionStateSubject = new Subject<boolean>();
  public temperatureDataSubject = new Subject<string>();
  public distanceDataSubject = new Subject<string>();
  public lumenDataSubject = new Subject<string>();

  public startConnection(): void {
    this.createDataHubs();
    this.mapHubsToSubjects();
  }

  private mapHubsToSubjects(): void {
    this.deviceConnectionStateHubConnection
      .on("ConnectionStateMessage", (message: boolean) => {
        this.connectionStateSubject.next(message);
      });
    this.deviceDistanceDataHubConnection
      .on("DistanceDataMessage", (message: string) => {
        this.distanceDataSubject.next(message);
      });
    this.deviceTemperatureDataHubConnection
      .on("TemperatureDataMessage", (message: string) => {
        this.temperatureDataSubject.next(message);
      });
    this.deviceLumenDataHubConnection
      .on("LumenDataMessage", (message: string) => {
        this.lumenDataSubject.next(message);
      });
  }

  private createDataHubs(): void {
    this.deviceConnectionStateHubConnection = new HubConnectionBuilder()
      .withUrl(this.deviceConnectionStateHubAddress)
      .build();
    this.deviceConnectionStateHubConnection.start();

    this.deviceDistanceDataHubConnection = new HubConnectionBuilder()
      .withUrl(this.deviceDistanceDataHubAddress)
      .build();
    this.deviceDistanceDataHubConnection.start();

    this.deviceTemperatureDataHubConnection = new HubConnectionBuilder()
      .withUrl(this.deviceTemperatureDataHubAddress)
      .build();
    this.deviceTemperatureDataHubConnection.start();

    this.deviceLumenDataHubConnection = new HubConnectionBuilder()
      .withUrl(this.deviceLumenDataHubAddress)
      .build();
    this.deviceLumenDataHubConnection.start();
  }
}
