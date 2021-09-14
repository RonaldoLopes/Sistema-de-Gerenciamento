import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-dialog-message',
  templateUrl: './dialog-message.component.html',
  styleUrls: ['./dialog-message.component.scss']
})
export class DialogMessageComponent implements OnInit {

  message: string = '';
  confirmButtonText = '';
  cancelButtonText = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<DialogMessageComponent>
  ) {
    if (data) {
      this.message = data.message || this.message;
      if (data.buttonText) {
        this.confirmButtonText = data.buttonText.delete || this.confirmButtonText;
        this.cancelButtonText = data.buttonText.cancel || this.cancelButtonText;
      }
    }
  }

  onConfirmClick(): void {
    this.dialogRef.close('delete');
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
  }

}
