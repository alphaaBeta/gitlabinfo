import { Injectable, Inject } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IProjectRequestPut, IProjectRequestGet } from '../../project-management/models/projectRequest';
import { Observable } from 'rxjs';
import { IProject } from '../../project-management/models/project';
import { IReportedTimePost, IReportedTimeGet } from '../../project-management/models/reportedTime';
import { IEngagementPointsGet, IEngagementPointsPut } from '../../project-management/models/engagementPoints';
import { IWorkDescriptionPost, IWorkDescriptionGet } from '../../project-management/models/workDescription';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  private handleData(methodName: string, data: any) {
    // console.log(methodName + ': ' + JSON.stringify(data));
  }

  public postRequestProjectCreation(projectRequest: IProjectRequestPut) {
    const body = projectRequest;
    
    return this.http.post(this.baseUrl + 'api/project/RequestProjectCreation', body)
      .pipe(tap(data => this.handleData('postRequestProjectCreation', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public approveProjectCreationRequest(requestId: number) {
    const params = new HttpParams().set('requestId', requestId.toString());

    return this.http.put(this.baseUrl + 'api/project/ApproveProjectCreationRequest', null, { params: params })
      .pipe(tap(data => this.handleData('approveProjectCreationRequest', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public rejectProjectCreationRequest(requestId: number) {
    const params = new HttpParams().set('requestId', requestId.toString());

    return this.http.put(this.baseUrl + 'api/project/RejectProjectCreationRequest', null, { params: params })
      .pipe(tap(data => this.handleData('rejectProjectCreationRequest', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getProjectCreationRequests(): Observable<IProjectRequestGet[]> {

    return this.http.get<IProjectRequestGet[]>(this.baseUrl + 'api/project/GetProjectCreationRequests')
      .pipe(tap(data => this.handleData('getProjectCreationRequest', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getProjects(groupId: number): Observable<IProject[]> {
    const params = new HttpParams().set('groupId', groupId.toString());

    return this.http.get<IProject[]>(this.baseUrl + 'api/project/GetProjects', { params: params })
      .pipe(tap(data => this.handleData('getProjects', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public postReportTime(reportedTime: IReportedTimePost) {
    const body = reportedTime;

    return this.http.post(this.baseUrl + 'api/project/ReportHours', body)
      .pipe(tap(data => this.handleData('postReportTime', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getReportedTimes(projectId: number): Observable<IReportedTimeGet[]> {
    const params = new HttpParams().set('projectId', projectId.toString());

    return this.http.get<IReportedTimeGet[]>(this.baseUrl + 'api/project/GetReportedHoursInProject', { params: params })
      .pipe(tap(data => this.handleData('getReportedTimes', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public submitEngagementPoints(engagemenetPoints: IEngagementPointsPut) {
    const body = engagemenetPoints;
    
    return this.http.put(this.baseUrl + 'api/project/GiveEngagementPoints', body)
      .pipe(tap(data => this.handleData('putEngagementPoints', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getEngagementPoints(projectId: number): Observable<IEngagementPointsGet[]> {
    const params = new HttpParams().set('projectId', projectId.toString());

    return this.http.get<IEngagementPointsGet[]>(this.baseUrl + 'api/project/GetEngagementPointsInProject', { params: params })
      .pipe(tap(data => this.handleData('getEngagementPoints', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public postWorkDescription(workDescription: IWorkDescriptionPost) {
    const body = workDescription;

    return this.http.post(this.baseUrl + 'api/project/PostWorkDescription', body)
      .pipe(tap(data => this.handleData('postWorkDescription', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getWorkDescriptions(projectId: number): Observable<IWorkDescriptionGet[]> {
    const params = new HttpParams().set('projectId', projectId.toString());

    return this.http.get<IWorkDescriptionGet[]>(this.baseUrl + 'api/project/GetWorkDescriptions', { params: params })
      .pipe(tap(data => this.handleData('getWorkDescriptions', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putWorkDescriptionComment(workDescriptionId: number, comment: string) {
    const params = new HttpParams().set('workDescriptionId', workDescriptionId.toString()).set('comment', comment.toString());

    return this.http.put(this.baseUrl + 'api/project/PutWorkDescriptionComment', null, { params: params })
      .pipe(tap(data => this.handleData('putWorkDescriptionComment', data)),
        catchError(ErrorHandlerService.handleError));
  }
}
