import { Component, Input } from '@angular/core';
import { trigger, transition, style, animate, animation, useAnimation } from "@angular/animations";

const scaleIn = animation([
  style({ opacity: 0, transform: "scale(0.5)" }), // start state
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 1, transform: "scale(1)" })
  )
]);

@Component({
  selector: 'app-choice-option-result-chart',
  templateUrl: './choice-option-result-chart.component.html',
  styleUrls: ['./choice-option-result-chart.component.css'],
  animations: [
    trigger('appearanceAnimation', [
      transition("void => *", [useAnimation(scaleIn, { params: { time: '500ms' } })])
    ])
  ]
})
export class ChoiceOptionResultChartComponent {
  @Input() index!: number;
  @Input() optionText!: string;
  @Input() percentage!: number;
  @Input() answersAmount!: number;
}
