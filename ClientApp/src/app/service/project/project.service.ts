import { Injectable, Inject } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IProjectRequest } from '../../project-management/models/projectRequest';
import { Observable } from 'rxjs';
import { IProject } from '../../project-management/models/project';
import { IReportedTime } from '../../project-management/models/reportedTime';
import { IEngagementPointsGet, IEngagementPointsPut } from 'src/app/project-management/models/engagementPoints';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public postRequestProjectCreation(projectRequest: IProjectRequest) {
    const body = projectRequest;

    return this.http.post(this.baseUrl + 'api/project/RequestProjectCreationAsync', body)
      .pipe(tap(data => console.log('postRequestProjectCreation: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getProjects(groupId: number): Observable<IProject[]> {
    const params = new HttpParams().set('groupId', groupId.toString());

    return this.http.get<IProject[]>(this.baseUrl + 'api/project/GetProjects', { params: params })
      .pipe(tap(data => console.log('getProjects: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }

  public postReportTime(reportedTime: IReportedTime) {
    const body = reportedTime;

    return this.http.post(this.baseUrl + 'api/project/ReportHoursAsync', body)
      .pipe(tap(data => console.log('postReportTime: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putEngagementPoints(engagemenetPoints: IEngagementPointsPut) {
    const body = engagemenetPoints;

    return this.http.put(this.baseUrl + 'api/project/GiveEngagementPointsAsync', body)
      .pipe(tap(data => console.log('putEngagementPoints: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getEngagementPoints(projectId: number): Observable<IEngagementPointsGet[]> {
    const params = new HttpParams().set('projectId', projectId.toString());

    return this.http.get<IEngagementPointsGet[]>(this.baseUrl + 'api/project/GetEngagementPointsInProjectAsync', { params: params })
      .pipe(tap(data => console.log('getEngagementPoints: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }
}
