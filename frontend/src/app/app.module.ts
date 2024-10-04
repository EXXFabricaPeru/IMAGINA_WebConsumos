import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { DocumentoCrearComponent } from './pages/documento-crear/documento-crear.component';
import { DocumentoListarComponent } from './pages/documento-listar/documento-listar.component';
import { DocumentoMostrarComponent } from './pages/documento-mostrar/documento-mostrar.component';
import { PaginacionPipe } from './pipes/paginacion.pipe';
import { UsuarioListarComponent } from './pages/usuario-listar/usuario-listar.component';
import { UsuarioCrearComponent } from './pages/usuario-crear/usuario-crear.component';
import { PaginacionCliPipe } from './pipes/paginacion-cli.pipe';
import { PaginacionEeccPipe } from './pipes/paginacion-eecc.pipe';
import { PaginacionArtPipe } from './pipes/paginacion-art.pipe';
import { TransferenciaListarComponent } from './pages/transferencia-listar/transferencia-listar.component';
import { TransferenciaCrearComponent } from './pages/transferencia-crear/transferencia-crear.component';
import { TransferenciaMostrarComponent } from './pages/transferencia-mostrar/transferencia-mostrar.component';
import { PaginacionStPipe } from './pipes/paginacion-st.pipe';
import { LostPasswordComponent } from './pages/lost-password/lost-password.component';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent,
    DocumentoCrearComponent,
    DocumentoListarComponent,
    DocumentoMostrarComponent,
    PaginacionPipe,
    UsuarioListarComponent,
    UsuarioCrearComponent,
    PaginacionCliPipe,
    PaginacionEeccPipe,
    PaginacionArtPipe,
    TransferenciaListarComponent,
    TransferenciaCrearComponent,
    TransferenciaMostrarComponent,
    PaginacionStPipe,
    LostPasswordComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
