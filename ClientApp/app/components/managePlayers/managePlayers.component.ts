import { Component } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'manage-players',
    template: require('./managePlayers.component.html')
})
export class ManagePlayersComponent {
    public bannedIps: Array<BannedIp>;
    public bannedPlayers: Array<BannedPlayer>;
    public userCache: Array<UserCache>;
    public players: Array<Player>;
    public http: Http;
    constructor(_http: Http) {
        this.http = _http;
        this.http.get('/api/Players/GetBannedIps').subscribe(result => {
            this.bannedIps = result.json();
        });
        this.http.get('/api/Players/GetBannedPlayers').subscribe(result => {
            this.bannedPlayers = result.json();
        });
        this.http.get('/api/Players/GetUserCache').subscribe(result => {
            this.userCache = result.json();
        });
        this.http.get('/api/Players/GetWhiteList').subscribe(result => {
            this.players = result.json();
        });
    }
}

interface BannedIp {
    Uuid: string;
    Created: string;
    Source: string;
    string: boolean;
    Reason: string;
}

interface BannedPlayer {
    Uuid: string;
    Name: string;
    Created: string;
    Source: string;
    Expires: string;
    Reason: string;
}

interface UserCache {
    ExpiresOn: Date;
    Uuid: string;
    Name: string;
}

interface Player {
    Uuid: string;
    Name: string; 
}