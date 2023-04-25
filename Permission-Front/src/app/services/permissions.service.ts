import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { IResultViewModel } from '../interfaces/result.interface';
import { lastValueFrom } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class PermissionsService {
    private baseUrl;

    constructor(private http: HttpClient) {
        this.baseUrl = environment.baseApiUrl;
    }
    GetGroupPermissions(groupId: number | null): Promise<IResultViewModel> {
        return lastValueFrom(this.http.get<IResultViewModel>(`${this.baseUrl}/Permissions/GetGroupPermissions?groupId=${groupId}`));
    }
    UpdateGroupPermissions(list: any[]): Promise<IResultViewModel> {
        return lastValueFrom(this.http.post<IResultViewModel>(`${this.baseUrl}/Permissions/UpdateGroupPermissions`, list));
    }
}
