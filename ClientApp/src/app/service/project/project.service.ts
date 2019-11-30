import { Injectable, Inject } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IProjectRequest } from '../../project-management/models/projectRequest';
import { Observable } from 'rxjs';
import { IProject } from '../../project-management/models/project';

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

  public getProjects(groupId: number): Observable<IProject[]> {
    console.log(groupId.toString())
    const params = new HttpParams().set('groupId', groupId.toString());
    console.log(params);

    return this.http.get<IProject[]>(this.baseUrl + 'api/project/GetProjects', { params: params })
      .pipe(tap(data => console.log('getProjects: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }
}
