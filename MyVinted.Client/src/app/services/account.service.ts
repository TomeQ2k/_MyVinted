import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly accountApiUrl = environment.apiUrl + 'account/';

  constructor(private httpClient: HttpClient) { }

  public getProfile() {
    return this.httpClient.get<any>(this.accountApiUrl);
  }

  public changeUsername(request: any) {
    return this.httpClient.patch(this.accountApiUrl + 'changeUsername', request);
  }

  public changePassword(request: any) {
    return this.httpClient.put(this.accountApiUrl + 'changePassword', request);
  }

  public changeEmail(newEmail: string, token: string) {
    return this.httpClient.get<any>(this.accountApiUrl + 'changeEmail', { params: { newEmail, token } });
  }

  public sendChangeEmailCallback(request: any) {
    return this.httpClient.post(this.accountApiUrl + 'changeEmail/send', request);
  }

  public changePhoneNumber(request: any) {
    return this.httpClient.patch(this.accountApiUrl + 'changePhoneNumber', request);
  }

  public setAvatar(avatar: File) {
    const formData = new FormData();

    formData.append('avatar', avatar);

    return this.httpClient.post(this.accountApiUrl + 'avatar/set', formData);
  }

  public deleteAvatar() {
    return this.httpClient.delete(this.accountApiUrl + 'avatar/delete');
  }
}
