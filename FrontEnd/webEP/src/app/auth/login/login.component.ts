import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public formSubmitted = false;

  public loginForm = this.fb.group({
    email:[localStorage.getItem('email') || '', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
    password:['', [ Validators.required]],
    remember:[(localStorage.getItem('email') || '' !== '')]
  });

  constructor(private fb: FormBuilder, private router: Router, private userService: UserService, private alertService: AlertService) { }

  login(){
    this.formSubmitted = true;
    if(this.loginForm.valid){
      this.userService.login(this.loginForm.value).subscribe(resp =>{
        if(this.loginForm.get('remember')?.value){
          localStorage.setItem('email', this.loginForm.get('email')?.value)
        }else{
          localStorage.removeItem('email');
        }
        this.router.navigateByUrl('/dashboard');
      }, (err) => {
        this.alertService.getShowAlert('Error al iniciar sesión', err.message, 'error');
      })
    }
  }

  invalidField(field: string): boolean{
    return !this.loginForm.get(field)?.value && this.formSubmitted;
  }

}
