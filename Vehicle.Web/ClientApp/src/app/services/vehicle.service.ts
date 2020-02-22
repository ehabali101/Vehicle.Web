import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { customer } from '../models/customer';
import { vehicle } from '../models/vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor() { }

  getcustomers(http: HttpClient, baseUrl: string): Observable<customer[]> {
    return http.get<customer[]>(baseUrl + 'api/vehicle/Customers');
  }

  getvehicles(http: HttpClient, baseUrl: string): Observable<vehicle[]> {
    return http.get<vehicle[]>(baseUrl + 'api/vehicle/Vehicles');
  }

  syncVehicles(http: HttpClient, baseUrl: string): Observable<vehicle[]> {
    return http.get<vehicle[]>(baseUrl + 'api/vehicle/SyncVehicles');
  }

}
