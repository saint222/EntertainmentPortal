import { Injectable } from '@angular/core'
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router'

@Injectable({ providedIn: 'root'})
export class DemoGuard implements CanActivate {
    constructor() {}

    canActivate(rote: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return true;
    }
}