import { Injectable, Inject } from '@angular/core';
import { IGroup } from '../../group-management/group';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService } from '../error-handler/error-handler.service';
import { IJoinRequest } from '../../group-management/join-request';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public getGetOwnedGroups(userId?: number): Observable<IGroup[]> {
    let params = new HttpParams();
    if (userId != null) {
      params.set('userId', userId.toString());
    }

    return this.http.get<IGroup[]>(this.baseUrl + 'api/group/GetOwnedGroups', { params: params })
      .pipe(tap(data => console.log('getGetOwnedGroups: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }

  public getGetJoinRequestsForOwnedGroups(userId?: number): Observable<IJoinRequest[]> {
    let params = new HttpParams();
    if (userId != null) {
      params.set('userId', userId.toString());
    }

    return this.http.get<IJoinRequest[]>(this.baseUrl + 'api/group/GetJoinRequestsForOwnedGroups', { params: params })
      .pipe(tap(data => console.log('getGetJoinRequestsForOwnedGroups: ' + JSON.stringify(data))),
        catchError(ErrorHandlerService.handleError));
  }

  /**
   * putRequestToJoinGroup
   */
  public putRequestToJoinGroup(groupId: number) {
    console.log(groupId);
    let params = new HttpParams();
    params.set('groupId', groupId.toString());
    //console.log(params);
    return this.http.put(this.baseUrl + 'api/group/RequestToJoinGroup', null, { params: params });
  }
}
