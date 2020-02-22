import { Component, OnInit, Inject } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { vehicle } from '../models/vehicle';
import { customer } from '../models/customer';
import { HttpClient } from '@angular/common/http';
import { flatMap } from 'rxjs/operators';
import { interval } from 'rxjs';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  customers: customer[];
  vehicles: vehicle[];
  allVehicles: vehicle[];
  filter: any = {};

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private vehicleService: VehicleService) {
    this.http = http;
    this.baseUrl = baseUrl;
    vehicleService.getcustomers(this.http, this.baseUrl)
      .subscribe(data => {
        this.customers = data;
        console.log('customers', this.customers);
    } , error => console.log(error));

    vehicleService.getvehicles(this.http, this.baseUrl)
      .subscribe(data => {
        this.vehicles = data;
        this.allVehicles = data;
        console.log('vehicles', this.vehicles);
      }, error => console.log(error));

  }

  ngOnInit(): void {
    interval(50000)
      .pipe(
        flatMap(() => {
          return this.vehicleService.syncVehicles(this.http, this.baseUrl);
        })
      )
      .subscribe(data => {
        console.log('update status..');
        console.log(data);
        this.allVehicles = data;
        this.vehicles = data;
      }, error => console.log(error));
  }

  onFilterChange() {
    var vehicles = this.allVehicles;
    console.log('change');
    console.log(this.filter);

    if (this.filter.customer)
      vehicles = vehicles.filter(v => v.customerName == this.filter.customer);

    if (this.filter.vehicleStatus)
      vehicles = vehicles.filter(v => v.vehicleStatus == this.filter.vehicleStatus);

    this.vehicles = vehicles;
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }


}
