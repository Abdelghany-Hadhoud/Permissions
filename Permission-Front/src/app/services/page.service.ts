import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { IResultViewModel } from '../interfaces/result.interface';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PagesService {
  private baseUrl;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.baseApiUrl;
  }
  GetPages(): Promise<IResultViewModel> {
    return lastValueFrom(this.http.get<IResultViewModel>(`${this.baseUrl}/Pages/GetPages`));
  }
}
