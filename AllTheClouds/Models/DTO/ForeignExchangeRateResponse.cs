namespace AllTheClouds.Models.DTO
{
    public class ForeignExchangeRateResponse
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
    }
}
