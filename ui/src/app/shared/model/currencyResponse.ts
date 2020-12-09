export interface ICurrencyResponseData {
  currency: string
  exchange: number
  date: string
}

export interface ICurrencyResponse {
  data: ICurrencyResponseData
  messages: string[]
}
