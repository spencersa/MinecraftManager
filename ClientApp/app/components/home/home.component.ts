import { Component, OnInit } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

@Component({
    selector: 'home',
    template: require('./home.component.html')
})
export class HomeComponent implements OnInit {
    public http: Http;
    public serverMessages: serverMessage;
    constructor(_http: Http) {
        this.http = _http;
    }

    ngOnInit() {
        this.initializePolling();
    }

    public startServer() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.get('/api/Server/Start').subscribe(result => {});
    }

    public stopServer() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.get('/api/Server/Stop').subscribe(result => {});
    }

    initializePolling() {
        Observable.interval(1000)
            .switchMap(() => this.http.get('/api/Server/GetServerOutput/')).map((data) => data.json())
            .subscribe((data) => {
                if (data) {
                    this.serverMessages = data;
                }
                console.log(data);
            });
    }
}

interface serverMessage {
    messages: Array<string>;
    serverStatus: string;
}