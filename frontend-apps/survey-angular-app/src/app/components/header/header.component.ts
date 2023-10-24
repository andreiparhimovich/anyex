import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  logoIconSvgContent: any;

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {
    this.http.get('assets/logo.svg', { responseType: 'text' })
      .subscribe(data => {
        this.logoIconSvgContent = this.sanitizer.bypassSecurityTrustHtml(data);
      });
  }
}
