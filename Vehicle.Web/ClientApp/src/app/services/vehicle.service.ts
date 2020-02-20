import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: Http) {
  }

  getVehicles() {
    return this.http.get('/api/vehicle/vehicles')
      .map(res => res.json());
  }

  getCustomers() {
    return this.http.get('/api/vehicle/customers')
      .map(res => res.json());
  }

  syncVehicles() {
    return this.http.get('/api/vehicle/syncvehicles')
      .map(res => res.json());
  }
}
