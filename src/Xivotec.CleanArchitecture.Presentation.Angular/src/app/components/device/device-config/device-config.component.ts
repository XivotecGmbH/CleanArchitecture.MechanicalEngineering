import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {
  ConnectionStateDto,
  FeatherDeviceActionsDto,
  FeatherDeviceClient,
  FeatherDeviceDto,
  LedColorDto
} from "../../../services/apiClient/api.service";
import {Guid} from "guid-typescript";

@Component({
  selector: 'app-device-config',
  templateUrl: './device-config.component.html',
  styleUrl: './device-config.component.css'
})
export class DeviceConfigComponent {
  private readonly comPortPrefix: string = "COM";
  private readonly isNewDevice: boolean = true;
  public featherDeviceDto: FeatherDeviceDto = {
    id: Guid.create().toString(),
    name: '',
    comPort: '',
    connectionState: ConnectionStateDto.Disconnected,
    connectionString: '',
    availableLedColors: Object.values(LedColorDto),
    action: FeatherDeviceActionsDto.None,
    recipe: {
      id: Guid.createEmpty().toString(),
      name: '',
      featherDeviceRecipe: undefined
    }
  }
  public comPortEntry: string = "0";

  public constructor(private router: Router, private featherDeviceClient: FeatherDeviceClient) {
    if (this.router.getCurrentNavigation()?.extras.state?.['currentdevice']) {
      this.featherDeviceDto = this.router.getCurrentNavigation()?.extras.state?.['currentdevice'];
      this.comPortEntry = this.featherDeviceDto.comPort.replace(this.comPortPrefix, "");
      this.isNewDevice = false;
    }
  }

  public applyButtonClicked(): void {
    this.featherDeviceDto.comPort = this.comPortPrefix + this.comPortEntry;

    if (this.isNewDevice) {
      this.featherDeviceClient.create(this.featherDeviceDto)
        .subscribe({
          error: error => console.error(error),
        });
    } else {
      this.featherDeviceClient.deInitialize(this.featherDeviceDto)
        .subscribe({
          error: error => console.error(error),
        });
      this.featherDeviceClient.update(this.featherDeviceDto)
        .subscribe({
          error: error => console.error(error),
        });
    }

    this.featherDeviceClient.initialize(this.featherDeviceDto)
      .subscribe({
        next: () => {
          this.router.navigate(['/device']);
        },
        error: error => console.error(error),
      });
  }
}
