import { Component } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'manage-players',
    template: require('./managePlayers.component.html')
})
export class ManagePlayersComponent {
    public http: Http;
    constructor(_http: Http) {
        this.http = _http;
    }
}