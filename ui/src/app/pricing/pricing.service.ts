import { Injectable } from '@angular/core';
import { ICurrency } from '../shared/model/currency';
import {
  HttpClient,
  HttpHeaders,
  HttpParams,
  HttpEventType
} from '@angular/common/http';
import { delay } from 'rxjs/operators';
import { throwError, from, of } from 'rxjs';
import { ICurrencyResponse, ICurrencyResponseData } from '../shared/model/currencyResponse';

@Injectable({
  providedIn: 'root'
})
export class PricingService {

  constructor(private http: HttpClient) { }

  getCurrencyList(): ICurrency[] {
    return [{
      id: 'USD',
      name: 'USD - Dolar americano',
    }, {
      id: 'BRL',
      name: 'BRL - Real',
    }];
  }

  searchCurrency(currency: string) {
    let url = "https://localhost:44349/virtualmind-api/v1/currency/" + currency;
     return this.http
       .get<ICurrencyResponse>(
        url,
         {
           responseType: 'json'
         }
       )
       ;
  }
}
