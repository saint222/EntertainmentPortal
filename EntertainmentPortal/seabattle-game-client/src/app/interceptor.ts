import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest} from '@angular/common/http';
import {Observable} from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(){

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(sessionStorage.getItem('id_token') !== null &&  sessionStorage.getItem('id_token') !== undefined)
    {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${sessionStorage.getItem('id_token')}`
      }
    });
   }

    return next.handle(req);
  }
}