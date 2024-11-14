import {Component, OnInit} from '@angular/core';
import {DeviceType} from "./dtos/DeviceType";
import {FeatherDeviceClient, FeatherDeviceDto} from "../../services/apiClient/api.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-device',
  templateUrl: './device.component.html',
  styleUrl: './device.component.css'
})
export class DeviceComponent implements OnInit {
  public readonly DeviceType = DeviceType;
  public deviceCollection: FeatherDeviceDto[] = [];
  public selectedDeviceType: number = 0;
  public documentDarkThemePresent: boolean = document.body.classList.contains('dark-theme');

  public constructor(private router: Router, private featherDeviceClient: FeatherDeviceClient) {
  }

  public ngOnInit(): void {
    this.getDevices();
  }

  public addButtonClicked(): void {
    this.router.navigate(['deviceconfig']);
  }

  public editButtonClicked(featherDeviceDto: FeatherDeviceDto): void {
    this.router.navigate(['deviceconfig'], {state: {'currentdevice': featherDeviceDto}});
  }

  public deleteButtonClicked(featherDeviceDto: FeatherDeviceDto): void {
    this.featherDeviceClient.deleteById(featherDeviceDto.id).subscribe({
      next: () => {
        this.getDevices();
      },
      error: error => console.error(error)
    });
  }

  public navigateButtonClicked(featherDeviceDto: FeatherDeviceDto): void {
    this.router.navigate(['devicecontrol'], {state: {'currentdevice': featherDeviceDto}});
  }

  private getDevices(): void {
    this.featherDeviceClient.getAll().subscribe({
      next: result => {
        this.deviceCollection = result;
      },
      error: error => console.error(error)
    });
  }
}
