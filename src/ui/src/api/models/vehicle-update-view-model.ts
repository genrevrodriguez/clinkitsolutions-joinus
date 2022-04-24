/* tslint:disable */
/* eslint-disable */
import { Location } from './location';
import { VehicleType } from './vehicle-type';
export interface VehicleUpdateViewModel {
  location?: Location;
  name?: null | string;
  type?: VehicleType;
  vehicleId?: null | number;
}
