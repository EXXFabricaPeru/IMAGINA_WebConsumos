import { Pipe, PipeTransform } from '@angular/core';
import { EstadoCuenta } from '../models/estadoCuenta';

@Pipe({
  name: 'paginacionEecc'
})
export class PaginacionEeccPipe implements PipeTransform {

  transform(documentos: EstadoCuenta[], page: number): EstadoCuenta[] {
    let _page: number;
    if(page != 0)
      _page = (10 * (page - 1));
    else
      _page = page;

    // console.log(_page);
    return documentos.slice(_page, _page + 10);
  }

}
