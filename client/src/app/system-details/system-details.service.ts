import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SystemDetails } from "./models/SystemDetails";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class SystemDetailsService {
  constructor(private http: HttpClient) {}

  public getServerInfo(): Observable<SystemDetails> {
    const resp = this.http.get<SystemDetails>("/api/systemdetails");
    return resp;
  }
}
