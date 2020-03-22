import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './app-button.component.html',
  styleUrls: ['./app-button.component.scss']
})
export class AppButtonComponent implements OnInit {
  @Input() label: string;
  @Output() action = new EventEmitter<any>();

  constructor() { }

  ngOnInit() { }

  onClick() {
    this.action.emit();
  }
}
