import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'modal-win',
  templateUrl: './modal-win.component.html',
  styleUrls: ['./modal-win.component.css']
})
export class ModalWin {
  @Output() onChanged = new EventEmitter<boolean>();
  btnClick(increased: any) {
    this.onChanged.emit(increased);
  }
}
