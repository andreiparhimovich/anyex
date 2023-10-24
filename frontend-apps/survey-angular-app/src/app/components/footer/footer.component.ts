import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  logoIconSvgContent: any;
  currentYear: string;

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {
    this.http.get('assets/lfooter.svg', { responseType: 'text' })
      .subscribe(data => {
        this.logoIconSvgContent = this.sanitizer.bypassSecurityTrustHtml(data);
      });

    this.currentYear = new Date().getFullYear().toString();
  }
}
