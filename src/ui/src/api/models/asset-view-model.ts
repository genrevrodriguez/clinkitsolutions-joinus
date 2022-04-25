/* tslint:disable */
/* eslint-disable */
import { AssetCategory } from './asset-category';
import { Location } from './location';
export interface AssetViewModel {
  assetCategory?: AssetCategory;
  id?: number;
  lastKnownLocation?: Location;
  name?: null | string;
}
