import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { UserProfile } from 'src/app/models/domain/user-profile';
import { AccountService } from 'src/app/services/account.service';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-avatar-uploader',
  templateUrl: './avatar-uploader.component.html',
  styleUrls: ['./avatar-uploader.component.scss']
})
export class AvatarUploaderComponent implements OnInit {
  @Input() currentUser: UserProfile;

  @ViewChild('fileInput') fileInput: ElementRef;

  private readonly unitConversionMultiplier = constants.unitConversionMultiplier;

  constructor(private accountService: AccountService, private notifier: Notifier, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
  }

  public setAvatar(file: File) {
    if (file) {
      if (file.size / this.unitConversionMultiplier / this.unitConversionMultiplier <= constants.maxFileSize) {
        const reader = new FileReader();
        reader.readAsDataURL(file);

        reader.onload = () => {
          const url = reader.result.toString();
          this.currentUser.avatarUrl = url;

          this.accountService.setAvatar(file).subscribe(() => {
            this.notifier.push('Avatar has been set');

            this.authService.currentUser.avatarUrl = url;
            this.listener.changeCurrentUser(this.authService.currentUser);
          }, error => this.notifier.push(error, 'error'));
        };

        this.fileInput.nativeElement.value = '';
      } else {
        this.notifier.push(`Maximum file size is ${constants.maxFileSize} MB`, 'warning');
      }
    }
  }

  public deleteAvatar() {
    if (this.currentUser.avatarUrl) {
      this.accountService.deleteAvatar().subscribe(() => {
        this.notifier.push('Avatar has been deleted');

        this.currentUser.avatarUrl = null;
        this.authService.currentUser.avatarUrl = null;
        this.listener.changeCurrentUser(this.authService.currentUser);
      }, error => this.notifier.push(error, 'error'));
    }
  }
}
