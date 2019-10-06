import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }
  static handleError(err: HttpErrorResponse) {
    let errorMsg = '';
    if (err.error instanceof ErrorEvent) {
      errorMsg = `An error occurred: ${err.error.message}`
    } else {
      errorMsg = `Server returned code ${err.status}, error message is ${err.message}`;
    }

    console.error(errorMsg);
    return throwError(errorMsg);
  }
}
