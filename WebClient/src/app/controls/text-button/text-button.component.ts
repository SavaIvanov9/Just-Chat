import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  OnChanges
} from '@angular/core';

@Component({
  selector: 'app-text-button',
  templateUrl: './text-button.component.html',
  styleUrls: ['./text-button.component.scss']
})
export class TextButtonComponent implements OnInit {
  @Input() label: string;
  @Output() action = new EventEmitter<any>();

  constructor() {}

  ngOnInit() {}

  onClick() {
    this.action.emit();
  }
}
