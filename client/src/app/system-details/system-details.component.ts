import { Component, OnInit } from "@angular/core";
import { SystemDetailsService } from "./system-details.service";
import { SystemDetails } from "./models/SystemDetails";
import { finalize } from "rxjs/operators";

@Component({
  selector: "app-system-details",
  templateUrl: "./system-details.component.html",
  styleUrls: ["./system-details.component.scss"]
})
export class SystemDetailsComponent implements OnInit {
  serverInfo: SystemDetails;
  serverError?: string;
  loading = true;

  constructor(private service: SystemDetailsService) {}

  async ngOnInit() {
    this.service
      .getServerInfo()
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(
        info => {
          this.serverError = undefined;
          this.serverInfo = info;
          console.log(this.serverInfo);
        },
        err => {
          this.serverError = err.statusText;
          console.error(err);
        },
        () => (this.loading = false)
      );
  }
}
