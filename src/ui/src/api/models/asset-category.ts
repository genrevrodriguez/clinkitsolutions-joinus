/* tslint:disable */
/* eslint-disable */
export interface AssetCategory {
  iconPath?: null | string;
  id?: number;
  name?: null | string;
  parentAssetCategory?: AssetCategory;
  parentAssetCategoryId?: null | number;
  subAssetCategories?: null | Array<AssetCategory>;
}
