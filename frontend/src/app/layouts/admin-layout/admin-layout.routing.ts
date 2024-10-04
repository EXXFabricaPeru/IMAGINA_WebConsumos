import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';
import { DocumentoListarComponent } from 'src/app/pages/documento-listar/documento-listar.component';
import { DocumentoMostrarComponent } from 'src/app/pages/documento-mostrar/documento-mostrar.component';
import { DocumentoCrearComponent } from 'src/app/pages/documento-crear/documento-crear.component';
import { UsuarioListarComponent } from 'src/app/pages/usuario-listar/usuario-listar.component';
import { UsuarioCrearComponent } from 'src/app/pages/usuario-crear/usuario-crear.component';
import { TransferenciaListarComponent } from 'src/app/pages/transferencia-listar/transferencia-listar.component';
import { TransferenciaCrearComponent } from 'src/app/pages/transferencia-crear/transferencia-crear.component';
import { TransferenciaMostrarComponent } from 'src/app/pages/transferencia-mostrar/transferencia-mostrar.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',                    component: DashboardComponent },
    { path: 'user-profile',                 component: UserProfileComponent },
    // { path: 'tables',                       component: TablesComponent },
    // { path: 'icons',                        component: IconsComponent },
    // { path: 'maps',                         component: MapsComponent },
    { path: 'doc-listar',                   component: DocumentoListarComponent },
    { path: 'doc-mostrar/:nro',             component: DocumentoMostrarComponent },
    { path: 'doc-crear/:nro',               component: DocumentoCrearComponent },
    { path: 'usuario-listar',               component: UsuarioListarComponent},
    { path: 'usuario-crear/:codigo',        component: UsuarioCrearComponent},
    { path: 'transferencia-listar',         component: TransferenciaListarComponent},
    { path: 'transferencia-crear/:codigo',  component: TransferenciaCrearComponent},
    { path: 'transferencia-mostrar/:nro',   component: TransferenciaMostrarComponent }
];
