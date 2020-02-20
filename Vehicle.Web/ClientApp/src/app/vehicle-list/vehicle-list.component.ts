import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { vehicle } from '../models/vehicle';
import { customer } from '../models/customer';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  customers: customer[];
  vehicles: vehicle[];
  allVehicles: vehicle[];
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit(): void {
    this.vehicleService.getCustomers()
      .subscribe(customers => this.customers = customers);

    this.vehicleService.syncVehicles()
      .subscribe(vehicles => this.vehicles = this.allVehicles = vehicles);
  }

  onFilterChange() {
    var vehicles = this.allVehicles;

    if (this.filter.customerId)
      vehicles = vehicles.filter(v => v.customerId = this.filter.customerId);

    if (this.filter.status)
      vehicles = vehicles.filter(v => v.status = this.filter.status);

    this.vehicles = vehicles;
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }
}
