import { Pipe, PipeTransform } from '@angular/core';
import { TransferenciaCab } from '../models/transferenciaCab';

@Pipe({
  name: 'paginacionSt'
})
export class PaginacionStPipe implements PipeTransform {

  transform(documentos: TransferenciaCab[], page: number): TransferenciaCab[] {
    let _page: number;
    if(page != 0)
      _page = (10 * (page - 1));
    else
      _page = page;

    console.log("pagina", _page);
    return documentos.slice(_page, _page + 10);
  }

}
