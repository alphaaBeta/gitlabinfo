import { Injectable, Inject } from '@angular/core';
import { IGroup } from '../../group-management/models/group';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
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
    const params = new HttpParams({fromObject: paramObj});

    return this.http.get<IGroup[]>(this.baseUrl + 'api/group/GetGroups', { params: params })
      .pipe(tap(data => console.log('getGetGroups: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }

  public getJoinRequestsForOwnedGroups(userId?: number): Observable<IJoinRequest[]> {
    const params = new HttpParams();
    if (userId != null) {
      params.set('userId', userId.toString());
    }

    return this.http.get<IJoinRequest[]>(this.baseUrl + 'api/group/GetJoinRequestsForOwnedGroups', { params: params })
      .pipe(tap(data => console.log('getGetJoinRequestsForOwnedGroups: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }

  public putRequestToJoinGroup(groupId: number) {
    const params = new HttpParams().set('groupId', groupId.toString());

    return this.http.put(this.baseUrl + 'api/group/RequestToJoinGroup', null, { params: params })
      .pipe(tap(data => console.log('putRequestToJoinGroup: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putAddUserToGroup(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString())
      .set('userId', userId.toString());

    return this.http.put(this.baseUrl + 'api/group/AddUserToGroup', null, { params: params })
      .pipe(tap(data => console.log('putAddUserToGroup: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public deleteUserJoinRequest(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString())
      .set('userId', userId.toString());

    return this.http.delete(this.baseUrl + 'api/group/RemoveUserJoinRequest', { params: params })
      .pipe(tap(data => console.log('deleteRemoveUserJoinRequest: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putAddCurrentUserAsGroupOwner(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());
      if (userId) {
        params.append('userId', userId.toString());
      }
      console.log(params);

    return this.http.put(this.baseUrl + 'api/group/AddCurrentUserAsGroupOwner', null, { params: params })
      .pipe(tap(data => console.log('putAddCurrentUserAsGroupOwner: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getProjectsFromGroup(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/GetProjectsFromGroupAsync', { params: params })
      .pipe(tap(data => console.log('getProjectsFromGroupAsync: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getReportedHoursInGroup(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/GetReportedHoursInGroupAsync', { params: params })
      .pipe(tap(data => console.log('getReportedHoursInGroupAsync: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getAvailableSurveys(groupId: number): Observable<ISurvey[]> {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get<ISurvey[]>(this.baseUrl + 'api/group/GetAvailableSurveysAsync', { params: params })
      .pipe(tap(data => console.log('getAvailableSurveys: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public postSurveyAnswer(surveyAnswer: ISurveyAnswer) {
    const body = surveyAnswer;

    return this.http.post(this.baseUrl + 'api/group/PostAnswerSurveyAsync', body)
      .pipe(tap(data => console.log('postSurveyAnswer: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public postGroupOptions(groupOptions: IGroupOptionsPost) {
    const body = groupOptions;

    return this.http.post(this.baseUrl + 'api/group/PostGroupOptionsAsync', body)
      .pipe(tap(data => console.log('postGroupOptions: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getSurveysFromOwnedGroup(): Observable<ISurvey[]> {

    return this.http.get<ISurvey[]>(this.baseUrl + 'api/group/GetSurveysForOwnedGroups')
      .pipe(tap(data => console.log('getSurveysFromOwnedGroup: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public updateDbInfo(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.put(this.baseUrl + 'api/group/UpdateDbInfo', null, { params: params })
      .pipe(tap(data => console.log('addUsersFromGroupToDb: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }
}
