import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'server-properties',
    template: require('./serverProperties.component.html')
})
export class ServerPropertiesComponent {
    public testString: TestString;

    constructor(http: Http) {
        http.get('/api/ServerProperties/GetServerPropertiesFile').subscribe(result => {
            this.testString = result.json();
        });
    }
}

interface TestString {
    Property: string;
    Value: string;
}
