import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-message-form',
  templateUrl: './message-form.component.html',
  styleUrls: ['./message-form.component.scss']
})
export class MessageFormComponent implements OnInit, Validatable {
  @Input() recipientId: string;

  @Output() messageSent = new EventEmitter<any>();

  messageForm: FormGroup;

  constants = constants;

  constructor(private messenger: Messenger, private formBuilder: FormBuilder, private notifier: Notifier) { }

  ngOnInit(): void {
    this.createMessageForm();
  }

  public sendMessage() {
    if (this.messageForm.valid) {
      const request = Object.assign({}, { ...this.messageForm.value, recipientId: this.recipientId });

      this.messenger.sendMessage(request).subscribe(res => {
        const response: any = res.body;
        this.messageSent.emit(response.message);
      }, error => this.notifier.push(error, 'error'), () => this.createMessageForm());
    }
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.messageForm);

  private createMessageForm() {
    this.messageForm = this.formBuilder.group({
      text: ['', [Validators.required, Validators.maxLength(constants.maxMessageLength)]]
    });
  }
}
