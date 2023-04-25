import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { IResultViewModel } from '../interfaces/result.interface';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {
  private baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.baseApiUrl;
  }
  GetGroups(): Promise<IResultViewModel> {
    return lastValueFrom(this.http.get<IResultViewModel>(`${this.baseUrl}/Groups/GetGroups`));
  }
  AddGroup(group: any): Promise<IResultViewModel> {
    return lastValueFrom(this.http.post<IResultViewModel>(`${this.baseUrl}/Groups/AddGroup`, group));
  }
  UpdateGroup(group: any): Promise<IResultViewModel> {
    return lastValueFrom(this.http.post<IResultViewModel>(`${this.baseUrl}/Groups/UpdateGroup`, group));
  }
  DeleteGroup(groupId: number): Promise<IResultViewModel> {
    return lastValueFrom(this.http.get<IResultViewModel>(`${this.baseUrl}/Groups/DeleteGroup?groupId=${groupId}`));
  }
}
