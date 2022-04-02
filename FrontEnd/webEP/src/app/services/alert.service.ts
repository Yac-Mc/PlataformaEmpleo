import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon, SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor() {}

	getShowAlert(
		title?: string,
		message?: string,
		iconAlert: SweetAlertIcon = 'success',
		showCancelButton: boolean = false,
		confirmButtonText: string = 'Confirmar'
	) {
		this.closeAlert();
		return new Promise<SweetAlertResult>((resolve) => {
			Swal.fire({
				title,
				html: message,
				icon: iconAlert as SweetAlertIcon,
				showCancelButton,
				confirmButtonText,
			}).then((resp) => {
				return resolve(resp);
			});
		});
	}

	getShowLoading(message: string, iconAlert: SweetAlertIcon = 'info') {
		this.closeAlert();
		Swal.fire({
			allowOutsideClick: false,
			text: message,
			icon: iconAlert as SweetAlertIcon,
		});
		Swal.showLoading();
	}

	closeAlert() {
		Swal.close();
	}

	getShowAlert2(
		title?: string,
		message?: string,
		iconAlert: SweetAlertIcon = 'success',
		showCancelButton: boolean = false,
		confirmButtonText: string = 'Confirmar',
		showDenyButton: boolean = false,
		denyButtonText : string = 'Denegar'		
	) {
		this.closeAlert();
		return new Promise<SweetAlertResult>((resolve) => {
			Swal.fire({
				title,
				html: message,
				icon: iconAlert as SweetAlertIcon,
				showCancelButton,
				confirmButtonText,
				showDenyButton,
				denyButtonText
			}).then((resp) => {
				return resolve(resp);
			});
		});
	}
}
