import {Component} from '@angular/core';
import {ProcessClient} from "../../services/apiClient/api.service";

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrl: './process.component.css'
})
export class ProcessComponent {

  public constructor(private processClient: ProcessClient) {
  }

  public processButtonClicked(): void {
    this.processClient.postProcessStart()
      .subscribe({
        error: error => console.error(error)
      });
  }
}
