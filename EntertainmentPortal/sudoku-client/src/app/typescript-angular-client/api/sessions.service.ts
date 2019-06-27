import { PlayerShort } from './../model/playerShort';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent } from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from '../encoder';

import { Observable } from 'rxjs';
import { Subject } from 'rxjs';

import { GetHintCommand } from '../model/getHintCommand';
import { Session } from '../model/session';
import { SetCellValueCommand } from '../model/setCellValueCommand';
import { UpdateSessionCommand } from '../model/updateSessionCommand';

import { BASE_PATH, COLLECTION_FORMATS } from '../variables';
import { Configuration } from '../configuration';

@Injectable()
export class SessionsService {

    protected basePath = 'http://localhost:58857';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    public updateSession: Subject<string> = new Subject<string>();
    public newSession: Subject<string> = new Subject<string>();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * Creates a new gamesession due to the set parametrs and saves it in the Db.
     *
     * @param model
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public sessionsCreateSession(model: Session, observe?: 'body', reportProgress?: boolean): Observable<Session>;
    public sessionsCreateSession(model: Session, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Session>>;
    public sessionsCreateSession(model: Session, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Session>>;
    public sessionsCreateSession(model: Session, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (model === null || model === undefined) {
            throw new Error('Required parameter model was null or undefined when calling sessionsCreateSession.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.post<Session>(`${this.basePath}/api/sessions`,
            model,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * Changes the known information about a gamesession and saves it in the Db.
     *
     * @param model
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public sessionsEditSession(model: UpdateSessionCommand, observe?: 'body', reportProgress?: boolean): Observable<Session>;
    public sessionsEditSession(model: UpdateSessionCommand, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Session>>;
    public sessionsEditSession(model: UpdateSessionCommand, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Session>>;
    public sessionsEditSession(model: UpdateSessionCommand, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (model === null || model === undefined) {
            throw new Error('Required parameter model was null or undefined when calling sessionsEditSession.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.put<Session>(`${this.basePath}/api/sessions`,
            model,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *         Changes the value of a cell after each of three possibilies to get automatically set values durring the game as prompts and saves it in the Db.
     *
     * @param model
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public sessionsGetHint(model: GetHintCommand, observe?: 'body', reportProgress?: boolean): Observable<Session>;
    public sessionsGetHint(model: GetHintCommand, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Session>>;
    public sessionsGetHint(model: GetHintCommand, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Session>>;
    public sessionsGetHint(model: GetHintCommand, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (model === null || model === undefined) {
            throw new Error('Required parameter model was null or undefined when calling sessionsGetHint.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.put<Session>(`${this.basePath}/api/getHint`,
            model,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * Fetches a gamesession from the Db by the unique Id.
     *
     * @param id
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public sessionsGetSessionById(id: number, observe?: 'body', reportProgress?: boolean): Observable<Session>;
    public sessionsGetSessionById(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Session>>;
    public sessionsGetSessionById(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Session>>;
    public sessionsGetSessionById(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling sessionsGetSessionById.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Session>(`${this.basePath}/api/sessions/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * Changes the value of a cell durring a gamesession and saves it in the Db.
     *
     * @param model
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public sessionsSetCellValue(model: SetCellValueCommand, observe?: 'body', reportProgress?: boolean): Observable<Session>;
    public sessionsSetCellValue(model: SetCellValueCommand, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Session>>;
    public sessionsSetCellValue(model: SetCellValueCommand, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Session>>;
    public sessionsSetCellValue(model: SetCellValueCommand, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (model === null || model === undefined) {
            throw new Error('Required parameter model was null or undefined when calling sessionsSetCellValue.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.put<Session>(`${this.basePath}/api/setCellValue`,
            model,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    get UpdateSession() {
      return this.updateSession.asObservable();
    }

    get NewSession() {
      return this.newSession.asObservable();
    }

}
