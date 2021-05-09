import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OpinionService {
  private readonly opinionApiUrl = environment.apiUrl + 'opinion/';

  constructor(private httpClient: HttpClient) { }

  public addOpinion(request: any) {
    return this.httpClient.post(this.opinionApiUrl + 'add', request, { observe: 'response' });
  }

  public deleteOpinion(opinionId: string, userId: string) {
    return this.httpClient.delete(this.opinionApiUrl + 'delete', { params: { opinionId, userId }, observe: 'response' });
  }
}
