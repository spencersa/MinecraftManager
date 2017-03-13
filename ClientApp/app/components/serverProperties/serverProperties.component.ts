import { Component } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'server-properties',
    template: require('./serverProperties.component.html')
})
export class ServerPropertiesComponent {
    public serverProperty: Array<ServerProperty>;
    public http: Http;
    constructor(_http: Http) {
        this.http = _http;
        this.http.get('/api/ServerProperties/GetServerPropertiesFile').subscribe(result => {
            this.serverProperty = result.json();
        });
    }

    public editRow(property: ServerProperty) {
        property.isEditingRow = true;
        property.initialValue = property.value;
    }

    public cancelEdit(property: ServerProperty) {
        property.isEditingRow = false;
        property.value = property.initialValue;
    }

    public submitEdit(property: ServerProperty) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let body = JSON.stringify(property);
        this.http.post('/api/ServerProperties/UpdateServerProperties', body, {
            headers: headers
        })
            .subscribe(
                data => console.log(data),
                err => console.log("TODO: add error logging"),
                () => property.isEditingRow = false
            );
    }
}

interface ServerProperty {
    property: string;
    value: string;
    initialValue: string;
    isEditingRow: boolean;
}
