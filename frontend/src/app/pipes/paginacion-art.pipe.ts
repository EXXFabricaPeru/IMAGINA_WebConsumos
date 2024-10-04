import { Pipe, PipeTransform } from '@angular/core';
import { Producto } from '../models/producto';

@Pipe({
  name: 'paginacionArt'
})
export class PaginacionArtPipe implements PipeTransform {

  transform(clientes: Producto[], page: number, cant: number): Producto[] {
    let _page: number;
    if(page != 0)
      _page = (cant * (page - 1));
    else
      _page = page;

    // console.log(_page);
    return clientes.slice(_page, _page + cant);
  }

}
