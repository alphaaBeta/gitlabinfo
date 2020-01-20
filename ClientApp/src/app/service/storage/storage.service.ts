import { Injectable, Inject, OnInit } from '@angular/core';
import { LOCAL_STORAGE, SESSION_STORAGE, WebStorageService } from 'angular-webstorage-service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  private data: any[];
  private ownerModeEnabledKey = 'ownerModeEnabled';
  private accountLoggedInKey = 'accountLoggedIn';

  constructor(@Inject(LOCAL_STORAGE) private localStorage: WebStorageService,
  @Inject(SESSION_STORAGE) private sessionStorage: WebStorageService) {
    this.data = [];
  }


  private saveInLocal(key: any, val: any): void {
    this.localStorage.set(key, val);
    this.data[key] = this.localStorage.get(key);
  }

  private getFromLocal(key: any): void {
    this.data[key] = this.localStorage.get(key);
  }

  private saveInSession(key: any, val: any): void {
    this.sessionStorage.set(key, val);
    this.data[key] = this.sessionStorage.get(key);
  }

  private getFromSession(key: any): void {
    this.data[key] = this.sessionStorage.get(key);
  }

  
  public setOwnerMode(enable: boolean) {
    this.saveInLocal(this.ownerModeEnabledKey, enable);
  }
  public isOwnerModeEnabled(): boolean {
    if (this.data[this.ownerModeEnabledKey]) {
      return this.data[this.ownerModeEnabledKey];
    } else {
      this.getFromLocal(this.ownerModeEnabledKey);
      if (!this.data[this.ownerModeEnabledKey]) {
        return false;
      }
      return this.data[this.ownerModeEnabledKey];
    }
  }

  public setAccountLoggedIn(enable: boolean) {
    this.saveInSession(this.accountLoggedInKey, enable);
  }
  public isAccountLoggedIn(): boolean {
    if (this.data[this.accountLoggedInKey]) {
      return this.data[this.accountLoggedInKey];
    } else {
      this.getFromSession(this.accountLoggedInKey);
      if (!this.data[this.accountLoggedInKey]) {
        return false;
      }
      return this.data[this.accountLoggedInKey];
    }
  }

}
