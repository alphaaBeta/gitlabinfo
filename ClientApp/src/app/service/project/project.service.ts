import { Injectable, Inject } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IProjectRequest } from '../../project-management/projectRequest';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public postRequestProjectCreation(projectRequest: IProjectRequest) {
    const body = projectRequest;

    return this.http.post(this.baseUrl + 'api/project/RequestProjectCreationAsync', projectRequest)
      .pipe(tap(data => console.log('postRequestProjectCreation: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }
}
