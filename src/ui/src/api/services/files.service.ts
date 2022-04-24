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

import { DeleteFileResponse } from '../models/delete-file-response';
import { GetFilesResponse } from '../models/get-files-response';

@Injectable({
  providedIn: 'root',
})
export class FilesService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiFilesGet
   */
  static readonly ApiFilesGetPath = '/api/files';

  /**
   * Gets a list of files.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiFilesGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesGet$Plain$Response(params?: {
    FileId?: string;
  }): Observable<StrictHttpResponse<GetFilesResponse>> {

    const rb = new RequestBuilder(this.rootUrl, FilesService.ApiFilesGetPath, 'get');
    if (params) {
      rb.query('FileId', params.FileId, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<GetFilesResponse>;
      })
    );
  }

  /**
   * Gets a list of files.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiFilesGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesGet$Plain(params?: {
    FileId?: string;
  }): Observable<GetFilesResponse> {

    return this.apiFilesGet$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<GetFilesResponse>) => r.body as GetFilesResponse)
    );
  }

  /**
   * Gets a list of files.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiFilesGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesGet$Json$Response(params?: {
    FileId?: string;
  }): Observable<StrictHttpResponse<GetFilesResponse>> {

    const rb = new RequestBuilder(this.rootUrl, FilesService.ApiFilesGetPath, 'get');
    if (params) {
      rb.query('FileId', params.FileId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<GetFilesResponse>;
      })
    );
  }

  /**
   * Gets a list of files.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiFilesGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesGet$Json(params?: {
    FileId?: string;
  }): Observable<GetFilesResponse> {

    return this.apiFilesGet$Json$Response(params).pipe(
      map((r: StrictHttpResponse<GetFilesResponse>) => r.body as GetFilesResponse)
    );
  }

  /**
   * Path part for operation apiFilesDelete
   */
  static readonly ApiFilesDeletePath = '/api/files';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiFilesDelete$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesDelete$Plain$Response(params?: {
    FileId?: string;
  }): Observable<StrictHttpResponse<DeleteFileResponse>> {

    const rb = new RequestBuilder(this.rootUrl, FilesService.ApiFilesDeletePath, 'delete');
    if (params) {
      rb.query('FileId', params.FileId, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<DeleteFileResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiFilesDelete$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesDelete$Plain(params?: {
    FileId?: string;
  }): Observable<DeleteFileResponse> {

    return this.apiFilesDelete$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<DeleteFileResponse>) => r.body as DeleteFileResponse)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiFilesDelete$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesDelete$Json$Response(params?: {
    FileId?: string;
  }): Observable<StrictHttpResponse<DeleteFileResponse>> {

    const rb = new RequestBuilder(this.rootUrl, FilesService.ApiFilesDeletePath, 'delete');
    if (params) {
      rb.query('FileId', params.FileId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<DeleteFileResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiFilesDelete$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiFilesDelete$Json(params?: {
    FileId?: string;
  }): Observable<DeleteFileResponse> {

    return this.apiFilesDelete$Json$Response(params).pipe(
      map((r: StrictHttpResponse<DeleteFileResponse>) => r.body as DeleteFileResponse)
    );
  }

}
