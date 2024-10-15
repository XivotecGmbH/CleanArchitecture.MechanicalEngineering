import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

// components
import { AppComponent } from './components/app.component';
import { DeviceComponent } from './components/device/device.component';
import { NotificationComponent } from './components/notification/notification.component';
import { ProcessComponent } from './components/process/process.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { TodolistComponent } from './components/todolist/todolist.component';
import { TodoitemComponent } from './components/todolist/todoitem/todoitem.component';
import { TododetailComponent } from './components/todolist/tododetail/tododetail.component';
import { SettingsComponent } from './components/settings/settings.component';
import { HomeComponent } from './components/home/home.component';
import { NotificationDetailsComponent } from './components/notification/notification-details/notification-details.component';
import {NgOptimizedImage} from "@angular/common";
import { RecipeDetailComponent } from './components/recipe/recipe-detail/recipe-detail.component';
import { RecipeImportComponent } from './components/recipe/recipe-import/recipe-import.component';
import { DeviceConfigComponent } from './components/device/device-config/device-config.component';
import { DeviceControlComponent } from './components/device/device-control/device-control.component';
import { TimeSeriesComponent } from './components/time-series/time-series.component';

@NgModule({
  declarations: [
    AppComponent,
    DeviceComponent,
    NotificationComponent,
    ProcessComponent,
    RecipeComponent,
    TodolistComponent,
    SettingsComponent,
    HomeComponent,
    TodoitemComponent,
    TododetailComponent,
    NotificationDetailsComponent,
    RecipeDetailComponent,
    RecipeImportComponent,
    DeviceConfigComponent,
    DeviceControlComponent,
    TimeSeriesComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    FormsModule,
    MatSidenavModule,
    MatSelectModule,
    MatSlideToggleModule,
    NgOptimizedImage
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
