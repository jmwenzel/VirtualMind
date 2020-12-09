import { Component, OnInit, Input } from '@angular/core';
import { ICurrencyResponseData } from '../shared/model/currencyResponse';

@Component({
  selector: 'app-currency-result',
  templateUrl: './currency-result.component.html',
  styleUrls: ['./currency-result.component.scss']
})
export class CurrencyResultComponent implements OnInit {
  @Input("currency") data?: ICurrencyResponseData;

  constructor() {
  }

  ngOnInit(): void {
  }

}
