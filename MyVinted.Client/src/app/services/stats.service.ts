import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatsService {
  private readonly statsApiUrl = environment.apiUrl + 'stats/';

  constructor(private httpClient: HttpClient) { }

  public fetchPremiumStats() {
    return this.httpClient.get<any>(this.statsApiUrl + 'premium');
  }

  public fetchAdminStats() {
    return this.httpClient.get<any>(this.statsApiUrl + 'admin');
  }
}
