import { Injectable, Inject } from '@angular/core';
import { IGroup } from '../../group-management/models/group';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { ResponseContentType, Jsonp } from '@angular/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IJoinRequest } from '../../group-management/models/joinRequest';
import { ISurvey } from '../../group-management/models/survey';
import { ISurveyAnswer } from '../../group-management/models/surveyAnswer';
import { IGroupOptions, IGroupOptionsPost } from '../../group-management/models/groupOptions';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  private handleData(methodName: string, data: any) {
    // console.log(methodName + ': ' + JSON.stringify(data));
  }

  public getGroups(userId?: number, role?: number): Observable<IGroup[]> {
    let paramObj = {};
    if (userId) {
      paramObj = {
        userId: userId
      };
    }
    if (role) {
      paramObj = {
        role: role
      };
    }
    const params = new HttpParams({ fromObject: paramObj });

    return this.http.get<IGroup[]>(this.baseUrl + 'api/group/GetGroups', { params: params })
      .pipe(tap(data => this.handleData('getGroups', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getJoinRequestsForOwnedGroups(userId?: number): Observable<IJoinRequest[]> {
    const params = new HttpParams();
    if (userId != null) {
      params.set('userId', userId.toString());
    }

    return this.http.get<IJoinRequest[]>(this.baseUrl + 'api/group/GetJoinRequestsForOwnedGroups', { params: params })
      .pipe(tap(data => this.handleData('getJoinRequestsForOwnedGroups', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putRequestToJoinGroup(groupId: number) {
    const params = new HttpParams().set('groupId', groupId.toString());

    return this.http.put(this.baseUrl + 'api/group/RequestToJoinGroup', null, { params: params })
      .pipe(tap(data => this.handleData('putRequestToJoinGroup', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putAddUserToGroup(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString())
      .set('userId', userId.toString());

    return this.http.put(this.baseUrl + 'api/group/AddUserToGroup', null, { params: params })
      .pipe(tap(data => this.handleData('putAddUserToGroup', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public deleteUserJoinRequest(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString())
      .set('userId', userId.toString());

    return this.http.delete(this.baseUrl + 'api/group/RemoveUserJoinRequest', { params: params })
      .pipe(tap(data => this.handleData('deleteUserJoinRequest', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putAddCurrentUserAsGroupOwner(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());
    if (userId) {
      params.append('userId', userId.toString());
    }

    return this.http.put(this.baseUrl + 'api/group/AddCurrentUserAsGroupOwner', null, { params: params })
      .pipe(tap(data => this.handleData('putAddCurrentUserAsGroupOwner', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getProjectsFromGroup(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/GetProjectsFromGroup', { params: params })
      .pipe(tap(data => this.handleData('getProjectsFromGroup', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getReportedHoursInGroup(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/GetReportedHoursInGroup', { params: params })
      .pipe(tap(data => this.handleData('getReportedHoursInGroup', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getAvailableSurveys(groupId: number): Observable<ISurvey[]> {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get<ISurvey[]>(this.baseUrl + 'api/group/GetAvailableSurveys', { params: params })
      .pipe(tap(data => this.handleData('getAvailableSurveys', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public postSurveyAnswer(surveyAnswer: ISurveyAnswer) {
    const body = surveyAnswer;

    return this.http.post(this.baseUrl + 'api/group/PostAnswerSurvey', body)
      .pipe(tap(data => this.handleData('postSurveyAnswer', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public postGroupOptions(groupOptions: IGroupOptionsPost) {
    const body = groupOptions;
    
    return this.http.post(this.baseUrl + 'api/group/PostGroupOptions', body)
      .pipe(tap(data => this.handleData('postGroupOptions', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getSurveysFromOwnedGroup(): Observable<ISurvey[]> {

    return this.http.get<ISurvey[]>(this.baseUrl + 'api/group/GetSurveysForOwnedGroups')
      .pipe(tap(data => this.handleData('getSurveysFromOwnedGroup', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public updateDbInfo(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.put(this.baseUrl + 'api/group/UpdateDbInfo', null, { params: params })
      .pipe(tap(data => this.handleData('updateDbInfo', data)),
        catchError(ErrorHandlerService.handleError));
  }

  public exportGroupInfo(groupId: number): Observable<Blob> {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/ExportToExcel', { params: params, responseType: 'blob'})
      .pipe(tap(data => this.handleData('exportGroupInfo', data)),
        catchError(ErrorHandlerService.handleError));
  }
}
