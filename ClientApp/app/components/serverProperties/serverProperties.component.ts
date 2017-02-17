import { Component } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'server-properties',
    template: require('./serverProperties.component.html')
})
export class ServerPropertiesComponent {
    public serverProperty: Array<ServerProperty>;
    public bullshit: String;
    public http: Http;
    constructor(_http: Http) {
        this.http = _http;
        this.http.get('/api/ServerProperties/GetServerPropertiesFile').subscribe(result => {
            this.serverProperty = result.json();
        });
    }

    public editRow(property: ServerProperty) {
        property.isEditingRow = true;
    }

    public submitEdit(property: ServerProperty) {
        debugger;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let body = JSON.stringify(property);
        this.http.post('/api/ServerProperties/UpdateServerProperties', body, {
            headers: headers
        })
            .subscribe(
            err => console.log("do nothing"),
            () => console.log('Update Complete')
            );
    }
}

interface ServerProperty {
    property: string;
    value: string;
    isEditingRow: boolean;
}
