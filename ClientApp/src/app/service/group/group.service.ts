import { Injectable, Inject } from '@angular/core';
import { IGroup } from '../../group-management/models/group';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IJoinRequest } from '../../group-management/models/join-request';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public getGetOwnedGroups(userId?: number): Observable<IGroup[]> {
    const params = new HttpParams();
    if (userId != null) {
      params.set('userId', userId.toString());
    }

    return this.http.get<IGroup[]>(this.baseUrl + 'api/group/GetOwnedGroups', { params: params })
      .pipe(tap(data => console.log('getGetOwnedGroups: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }

  public getGetJoinRequestsForOwnedGroups(userId?: number): Observable<IJoinRequest[]> {
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

  public deleteRemoveUserJoinRequest(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString())
      .set('userId', userId.toString());

    return this.http.delete(this.baseUrl + 'api/group/RemoveUserJoinRequest', { params: params })
      .pipe(tap(data => console.log('deleteRemoveUserJoinRequest: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public putAddCurrentUserAsGroupOwner(groupId: number, userId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString())
      .set('userId', userId.toString());

    return this.http.put(this.baseUrl + 'api/group/AddCurrentUserAsGroupOwner', null, { params: params })
      .pipe(tap(data => console.log('putAddCurrentUserAsGroupOwner: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getProjectsFromGroupAsync(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/GetProjectsFromGroupAsync', { params: params })
      .pipe(tap(data => console.log('getProjectsFromGroupAsync: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }

  public getReportedHoursInGroupAsync(groupId: number) {
    const params = new HttpParams()
      .set('groupId', groupId.toString());

    return this.http.get(this.baseUrl + 'api/group/GetReportedHoursInGroupAsync', { params: params })
      .pipe(tap(data => console.log('getReportedHoursInGroupAsync: ' + data)),
        catchError(ErrorHandlerService.handleError));
  }
}
