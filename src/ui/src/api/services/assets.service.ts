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

import { GetAssetsResponse } from '../models/get-assets-response';
import { UpdateAssetLogsRequest } from '../models/update-asset-logs-request';
import { UpdateAssetLogsResponse } from '../models/update-asset-logs-response';

@Injectable({
  providedIn: 'root',
})
export class AssetsService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiAssetsGet
   */
  static readonly ApiAssetsGetPath = '/api/assets';

  /**
   * Get a list of vehicles optionally filtered by fleet ID.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAssetsGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetsGet$Plain$Response(params?: {
    FleetId?: number;
    AssetCategoryId?: number;
    FileId?: string;
  }): Observable<StrictHttpResponse<GetAssetsResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AssetsService.ApiAssetsGetPath, 'get');
    if (params) {
      rb.query('FleetId', params.FleetId, {});
      rb.query('AssetCategoryId', params.AssetCategoryId, {});
      rb.query('FileId', params.FileId, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<GetAssetsResponse>;
      })
    );
  }

  /**
   * Get a list of vehicles optionally filtered by fleet ID.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAssetsGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetsGet$Plain(params?: {
    FleetId?: number;
    AssetCategoryId?: number;
    FileId?: string;
  }): Observable<GetAssetsResponse> {

    return this.apiAssetsGet$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<GetAssetsResponse>) => r.body as GetAssetsResponse)
    );
  }

  /**
   * Get a list of vehicles optionally filtered by fleet ID.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAssetsGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetsGet$Json$Response(params?: {
    FleetId?: number;
    AssetCategoryId?: number;
    FileId?: string;
  }): Observable<StrictHttpResponse<GetAssetsResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AssetsService.ApiAssetsGetPath, 'get');
    if (params) {
      rb.query('FleetId', params.FleetId, {});
      rb.query('AssetCategoryId', params.AssetCategoryId, {});
      rb.query('FileId', params.FileId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<GetAssetsResponse>;
      })
    );
  }

  /**
   * Get a list of vehicles optionally filtered by fleet ID.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAssetsGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAssetsGet$Json(params?: {
    FleetId?: number;
    AssetCategoryId?: number;
    FileId?: string;
  }): Observable<GetAssetsResponse> {

    return this.apiAssetsGet$Json$Response(params).pipe(
      map((r: StrictHttpResponse<GetAssetsResponse>) => r.body as GetAssetsResponse)
    );
  }

  /**
   * Path part for operation apiAssetsLogsPost
   */
  static readonly ApiAssetsLogsPostPath = '/api/assets/logs';

  /**
   * Update vehicle location logs.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAssetsLogsPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAssetsLogsPost$Plain$Response(params?: {
    body?: UpdateAssetLogsRequest
  }): Observable<StrictHttpResponse<UpdateAssetLogsResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AssetsService.ApiAssetsLogsPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UpdateAssetLogsResponse>;
      })
    );
  }

  /**
   * Update vehicle location logs.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAssetsLogsPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAssetsLogsPost$Plain(params?: {
    body?: UpdateAssetLogsRequest
  }): Observable<UpdateAssetLogsResponse> {

    return this.apiAssetsLogsPost$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<UpdateAssetLogsResponse>) => r.body as UpdateAssetLogsResponse)
    );
  }

  /**
   * Update vehicle location logs.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAssetsLogsPost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAssetsLogsPost$Json$Response(params?: {
    body?: UpdateAssetLogsRequest
  }): Observable<StrictHttpResponse<UpdateAssetLogsResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AssetsService.ApiAssetsLogsPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UpdateAssetLogsResponse>;
      })
    );
  }

  /**
   * Update vehicle location logs.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAssetsLogsPost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAssetsLogsPost$Json(params?: {
    body?: UpdateAssetLogsRequest
  }): Observable<UpdateAssetLogsResponse> {

    return this.apiAssetsLogsPost$Json$Response(params).pipe(
      map((r: StrictHttpResponse<UpdateAssetLogsResponse>) => r.body as UpdateAssetLogsResponse)
    );
  }

}
