import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'btn-sort',
  templateUrl: './btn-sort.component.html',
  styleUrls: ['./btn-sort.component.css']
})
export class BtnSortComponent {
  public fromBigToSmall = false;
  @Input() public active = false;

  @Output() onChanged = new EventEmitter<boolean>();
  toggle(increased: any) {
    this.fromBigToSmall = !this.fromBigToSmall;
    this.onChanged.emit(this.fromBigToSmall);
  }
}
