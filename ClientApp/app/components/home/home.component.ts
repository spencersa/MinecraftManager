import { Component } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'home',
    template: require('./home.component.html')
})
export class HomeComponent {
    public http: Http;
    constructor(_http: Http) {
        this.http = _http;
    }

    public startServer() {
        debugger;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.get('/api/Server/Start').subscribe(result => {
        });
    }
}
