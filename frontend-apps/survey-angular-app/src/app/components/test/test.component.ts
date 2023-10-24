import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { trigger, transition, style, animate, animation, useAnimation } from "@angular/animations";

const scaleIn = animation([
  style({ opacity: 0, transform: "scale(0.5)" }), // start state
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 1, transform: "scale(1)" })
  )
]);

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css'],
  animations: [
    trigger('appearanceAnimation', [
      transition("void => *", [useAnimation(scaleIn, { params: { time: '500ms' } })])
    ])
  ]
})
export class TestComponent {

  index: number = 1;
  optionText: string = "Option 1";
  amount: number = 42;
  percentage: number = 50;

  options: { index: number, optionText: string, amount: number, percentage: number }[] = [
    { index: 1, optionText: "Option 1", amount: 50, percentage: 50 },
    { index: 2, optionText: "Option 2", amount: 25, percentage: 25 },
    { index: 3, optionText: "Option 3", amount: 25, percentage: 25 }
  ]

  constructor() {
  }

  generateRandomOptionsValues() {

    this.options = [];

    for (let i = 0; i < 3; i++) {
      const index = i + 1;
      const optionText = "Option" + index;
      const amount = Math.floor(Math.random() * (100 - 5) + 5);
      const percentage = amount;

      this.options.push({
        index, optionText, amount, percentage
      })
    }
  }
}
