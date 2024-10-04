import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: '/usuario-listar', title: 'Usuario', icon: 'ni-circle-08 text-green', class: ''},
    // { path: '/cliente-listar', title: 'Cliente', icon: 'ni-circle-08 text-green', class: ''},
    { path: '/doc-listar', title: 'Solicitud Consumo',  icon:'ni-bag-17 text-pink', class: '' }, 
    { path: '/transferencia-listar', title: 'Solicitud Transferencia',  icon:'ni-bag-17 text-pink', class: '' }, 
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private router: Router) { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }

  salir(){
    sessionStorage.removeItem("dataUsuario");
    this.router.navigateByUrl('/login', { replaceUrl: true });

  }
}
