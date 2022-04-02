import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  public formSubmitted = false;

  public registerForm = this.fb.group({
    name: ['Yoe Andres', [Validators.required]],
    surnames: ['Cardenas', [Validators.required]],
    email:['yac8807@gmail.com', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
    userName:['yac123456', [ Validators.required, Validators.minLength(3)]],
    password:['Yac123456789#', [ Validators.required, Validators.minLength(5)]],
    password2:['Yac123456789#', [ Validators.required, Validators.minLength(5)]],
    terms:[false, Validators.requiredTrue],
    typeUser:['Applicant', [Validators.required]]
  },
  {
    validator: this.equalsPasswords('password','password2')
  }
  );

  constructor(private fb: FormBuilder, private userService: UserService, private alertService: AlertService, private router: Router,) { }

  createUser(){
    this.formSubmitted = true;
    console.log(this.registerForm.value);
    console.log(this.registerForm);

    if(this.registerForm.invalid){
      console.log('Error Formulario incorrecto');
    }else{
      this.userService.createUser(this.registerForm.value).subscribe(resp => {
        this.router.navigateByUrl('/');
      }, (err) => {
        this.alertService.getShowAlert('Error al registrase', err.message, 'error');
      });
    }
  }

  invalidField(field: string): boolean{
    return !this.registerForm.get(field)?.value && this.formSubmitted;
  }

  passwordsNotEquals(){
      const pass1 = this.registerForm.get('password')?.value;
      const pass2 = this.registerForm.get('password2')?.value;
      return (pass1 !== pass2 && this.formSubmitted);
  }

  equalsPasswords(pass1: string, pass2: string){
    return (formGroup: FormGroup) => {
      const pass1Control = formGroup.controls[pass1];
      const pass2Control = formGroup.controls[pass2];
      if (pass1Control.value === pass2Control.value){
        pass2Control.setErrors(null);
      } else{
        pass2Control.setErrors({noEquals: true});
      }
    };
  }

}
