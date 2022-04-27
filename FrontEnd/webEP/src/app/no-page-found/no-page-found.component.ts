import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-no-page-found',
  templateUrl: './no-page-found.component.html',
  styleUrls: ['./no-page-found.component.css']
})
export class NoPageFoundComponent {
  constructor(private router: Router){}

  year = new Date().getFullYear();

  goHome(){
    this.router.navigateByUrl('/dashboard');
  }

}
