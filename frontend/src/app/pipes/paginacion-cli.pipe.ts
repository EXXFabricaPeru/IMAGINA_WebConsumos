import { Pipe, PipeTransform } from '@angular/core';
import { Cliente } from '../models/cliente';

@Pipe({
  name: 'paginacionCli'
})
export class PaginacionCliPipe implements PipeTransform {

  transform(clientes: Cliente[], page: number, cant: number): Cliente[] {
    let _page: number;
    if(page != 0)
      _page = (cant * (page - 1));
    else
      _page = page;

    console.log(page);
    return clientes.slice(_page, _page + cant);
  }

}
