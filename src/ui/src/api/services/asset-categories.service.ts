/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { GetAssetCategoriesRequest } from '../models/get-asset-categories-request';
import { GetAssetCategoriesResponse } from '../models/get-asset-categories-response';

@Injectable({
  providedIn: 'root',
})
export class AssetCategoriesService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiAssetcategoriesGet
   */
  static readonly ApiAssetcategoriesGetPath = '/api/assetcategories';

  /**
   * Gets a list of AssetCategories.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAssetcategoriesGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetcategoriesGet$Plain$Response(params?: {
    request?: GetAssetCategoriesRequest;
  }): Observable<StrictHttpResponse<GetAssetCategoriesResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AssetCategoriesService.ApiAssetcategoriesGetPath, 'get');
    if (params) {
      rb.query('request', params.request, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<GetAssetCategoriesResponse>;
      })
    );
  }

  /**
   * Gets a list of AssetCategories.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAssetcategoriesGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetcategoriesGet$Plain(params?: {
    request?: GetAssetCategoriesRequest;
  }): Observable<GetAssetCategoriesResponse> {

    return this.apiAssetcategoriesGet$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<GetAssetCategoriesResponse>) => r.body as GetAssetCategoriesResponse)
    );
  }

  /**
   * Gets a list of AssetCategories.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAssetcategoriesGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetcategoriesGet$Json$Response(params?: {
    request?: GetAssetCategoriesRequest;
  }): Observable<StrictHttpResponse<GetAssetCategoriesResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AssetCategoriesService.ApiAssetcategoriesGetPath, 'get');
    if (params) {
      rb.query('request', params.request, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<GetAssetCategoriesResponse>;
      })
    );
  }

  /**
   * Gets a list of AssetCategories.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAssetcategoriesGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetcategoriesGet$Json(params?: {
    request?: GetAssetCategoriesRequest;
  }): Observable<GetAssetCategoriesResponse> {

    return this.apiAssetcategoriesGet$Json$Response(params).pipe(
      map((r: StrictHttpResponse<GetAssetCategoriesResponse>) => r.body as GetAssetCategoriesResponse)
    );
  }

}
