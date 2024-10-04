import { Pipe, PipeTransform } from '@angular/core';
import { PedidoCab } from '../models/pedidocab';

@Pipe({
  name: 'paginacion'
})
export class PaginacionPipe implements PipeTransform {

  transform(documentos: PedidoCab[], page: number): PedidoCab[] {
    let _page: number;
    if(page != 0)
      _page = (10 * (page - 1));
    else
      _page = page;

    // console.log(_page);
    return documentos.slice(_page, _page + 10);
  }

}
