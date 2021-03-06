import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IUserDetailed } from '../../account/user';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler/error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAccount(): Observable<IUserDetailed> {
    return this.http.get<IUserDetailed>(this.baseUrl + 'api/account')
      .pipe(tap(data => console.log('getAccount: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }
}
