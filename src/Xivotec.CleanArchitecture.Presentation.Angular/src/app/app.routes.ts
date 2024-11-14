import {Routes} from '@angular/router';

// import components to add to routing
import {DeviceComponent} from './components/device/device.component';
import {HomeComponent} from './components/home/home.component';
import {NotificationComponent} from './components/notification/notification.component';
import {ProcessComponent} from './components/process/process.component';
import {SettingsComponent} from './components/settings/settings.component';
import {TodolistComponent} from './components/todolist/todolist.component';
import {TodoitemComponent} from './components/todolist/todoitem/todoitem.component';
import {TododetailComponent} from './components/todolist/tododetail/tododetail.component';
import {
  NotificationDetailsComponent
} from "./components/notification/notification-details/notification-details.component";
import {RecipeComponent} from "./components/recipe/recipe.component";
import {RecipeDetailComponent} from "./components/recipe/recipe-detail/recipe-detail.component";
import {RecipeImportComponent} from "./components/recipe/recipe-import/recipe-import.component";
import {DeviceControlComponent} from "./components/device/device-control/device-control.component";
import {DeviceConfigComponent} from "./components/device/device-config/device-config.component";
import {TimeSeriesComponent} from "./components/time-series/time-series.component";

export const routes: Routes = [
  // add path to components
  {path: '', component: HomeComponent},
  {path: 'home', redirectTo: ''},
  {path: 'recipe', component: RecipeComponent},
  {path: 'recipedetail', component: RecipeDetailComponent},
  {path: 'recipeimport', component: RecipeImportComponent},
  {path: 'device', component: DeviceComponent},
  {path: 'deviceconfig', component: DeviceConfigComponent},
  {path: 'devicecontrol', component: DeviceControlComponent},
  {path: 'todolist', component: TodolistComponent},
  {path: 'todoitem', component: TodoitemComponent},
  {path: 'tododetail', component: TododetailComponent},
  {path: 'process', component: ProcessComponent},
  {path: 'notification', component: NotificationComponent},
  {path: 'notificationdetail', component: NotificationDetailsComponent},
  {path: 'timeseries', component: TimeSeriesComponent},
  {path: 'settings', component: SettingsComponent}
];
