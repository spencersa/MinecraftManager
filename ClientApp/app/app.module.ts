import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { UniversalModule } from "angular2-universal";
import { AppComponent } from "./components/app/app.component";
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { HomeComponent } from "./components/home/home.component";
import { ServerPropertiesComponent } from "./components/serverProperties/serverProperties.component";
import { FormsModule } from "@angular/forms";
import { ReversePipe } from "./components/pipes/reverse.pipe";
import { ManagePlayersComponent } from "./components/managePlayers/managePlayers.component";
import { ModalComponent } from "./components/shared/modal.component";

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        ServerPropertiesComponent,
        ManagePlayersComponent,
        ReversePipe,
        ModalComponent
    ],
    imports: [
        UniversalModule, // must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "server-properties", component: ServerPropertiesComponent },
            { path: "manage-players", component: ManagePlayersComponent },
            { path: "**", redirectTo: "home" }
        ])
    ]
})
export class AppModule {
}
