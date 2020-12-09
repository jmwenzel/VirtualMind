import { Component, OnInit } from '@angular/core';
import { PricingService } from './pricing.service';
import { ICurrency } from '../shared/model/currency';
import { ICurrencyResponseData } from '../shared/model/currencyResponse';

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.scss'],
  providers: [PricingService]
})
export class PricingComponent implements OnInit {

  currencyList: ICurrency[] = [];
  selectedCurrency: string = '';
  isSearching: boolean = false;
  currencyData: ICurrencyResponseData | null = null;

  constructor(private pricingService: PricingService) { }

  ngOnInit(): void {
    this.currencyList = this.pricingService.getCurrencyList();
  }

  onCurrencySearch() {
    console.log(this.selectedCurrency);
    this.isSearching = true;
    this.currencyData = null;

    this.pricingService
      .searchCurrency(this.selectedCurrency)
      .subscribe(
        responseData => {
          this.currencyData = responseData.data;
        },
        (error) => {
          console.log(error);
        },
        () => {
          console.log('completed');
          this.isSearching = false;
        }
      );
  }
}
