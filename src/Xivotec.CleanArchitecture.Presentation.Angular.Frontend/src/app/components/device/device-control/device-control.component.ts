import {Component, OnInit} from '@angular/core';
import {FeatherDeviceClient, FeatherDeviceDto} from "../../../services/apiClient/api.service";
import {Router} from "@angular/router";
import {DeviceControlService} from "../../../services/device/device-control.service";

@Component({
  selector: 'app-device-control',
  templateUrl: './device-control.component.html',
  styleUrl: './device-control.component.css'
})
export class DeviceControlComponent implements OnInit {
  private readonly sensorNotSelectedText: string = "Not selected";
  private currentDevice: FeatherDeviceDto;
  public isDeviceConnected: boolean = false;
  public isTempDataSelected: boolean = true;
  public isDistanceDataSelected: boolean = true;
  public isLumenDataSelected: boolean = true;

  public temperatureValue: string = "";
  public distanceValue: string = "";
  public lumenValue: string = "";

  public constructor(private router: Router, private featherDeviceClient: FeatherDeviceClient, private deviceControlService: DeviceControlService) {
    this.currentDevice = this.router.getCurrentNavigation()?.extras.state?.['currentdevice'];
  }

  public ngOnInit(): void {
    this.deviceControlService.startConnection();
    this.initializeSubjectBindings();
  }

  public connectButtonClicked(): void {
    this.featherDeviceClient.connect(this.currentDevice)
      .subscribe({
        error: error => console.error(error),
      });
  }

  public disconnectButtonClicked(): void {
    this.featherDeviceClient.disconnect(this.currentDevice)
      .subscribe({
        error: error => console.error(error),
      });
  }

  public startButtonClicked(): void {
    this.featherDeviceClient.start(this.currentDevice)
      .subscribe({
        error: error => console.error(error),
      });
    this.startTemperatureDataStream();
    this.startDistanceDataStream();
    this.startLumenDataStream();
  }

  public stopButtonClicked(): void {
    this.featherDeviceClient.stop(this.currentDevice)
      .subscribe({
        error: error => console.error(error),
      });
  }

  public pauseButtonClicked(): void {
    this.featherDeviceClient.pause(this.currentDevice)
      .subscribe({
        error: error => console.error(error),
      });
  }

  public continueButtonClicked(): void {
    this.featherDeviceClient.continue(this.currentDevice)
      .subscribe({
        error: error => console.error(error),
      });
  }

  private initializeSubjectBindings(): void {
    this.deviceControlService.connectionStateSubject.subscribe({
      next: (message: boolean) => {
        this.isDeviceConnected = message;
      }
    });
    this.deviceControlService.distanceDataSubject.subscribe({
      next: (message: string) => {
        this.distanceValue = message;
      }
    });
    this.deviceControlService.temperatureDataSubject.subscribe({
      next: (message: string) => {
        this.temperatureValue = message;
      }
    });
    this.deviceControlService.lumenDataSubject.subscribe({
      next: (message: string) => {
        this.lumenValue = message;
      }
    });
  }

  private startTemperatureDataStream(): void {
    if (this.isTempDataSelected) {
      this.featherDeviceClient.startTemperatureDataReceiving(this.currentDevice)
        .subscribe({
          error: error => console.error(error),
        });
    } else {
      this.temperatureValue = this.sensorNotSelectedText;
    }
  }

  private startDistanceDataStream(): void {
    if (this.isDistanceDataSelected) {
      this.featherDeviceClient.startDistanceDataReceiving(this.currentDevice)
        .subscribe({
          error: error => console.error(error),
        });
    } else {
      this.distanceValue = this.sensorNotSelectedText;
    }
  }

  private startLumenDataStream(): void {
    if (this.isLumenDataSelected) {
      this.featherDeviceClient.startLumenDataReceiving(this.currentDevice)
        .subscribe({
          error: error => console.error(error),
        });
    } else {
      this.lumenValue = this.sensorNotSelectedText;
    }
  }
}
