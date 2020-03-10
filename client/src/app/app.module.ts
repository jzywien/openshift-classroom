import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { SystemDetailsComponent } from "./system-details/system-details.component";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  declarations: [AppComponent, SystemDetailsComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
