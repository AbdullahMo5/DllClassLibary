using MetaQuotes.MT5CommonAPI;

namespace Goorge.Models
{
    public class SymbolModel
    {
        public SymbolModel()
        {

        }
        public SymbolModel(CIMTConSymbol _symbol)
        {
            Symbol = _symbol.Symbol();
            Path = _symbol.Path();
            ISIN = _symbol.ISIN();
            Description = _symbol.Description();
            International = _symbol.International();
            Category = _symbol.Category();
            Exchange = _symbol.Exchange();
            CFI = _symbol.CFI();
            Sector = _symbol.Sector();
            Industry = _symbol.Industry();
            Country = _symbol.Country();
            Basis = _symbol.Basis();
            Source = _symbol.Source();
            Page = _symbol.Page();
            CurrencyBase = _symbol.CurrencyBase();
            CurrencyBaseDigits = _symbol.CurrencyBaseDigits();
            ContractSize = _symbol.ContractSize();
            TickSize = _symbol.TickSize();
            TickValue = _symbol.TickValue();
            MarginInitial = _symbol.MarginInitial();
            MarginMaintenance = _symbol.MarginMaintenance();
            CurrencyProfit = _symbol.CurrencyProfit();
            CurrencyProfitDigits = _symbol.CurrencyProfitDigits();
            CurrencyMargin = _symbol.CurrencyMargin();
            CurrencyMarginDigits = _symbol.CurrencyMarginDigits();
            Color = _symbol.Color();
            ColorBackground = _symbol.ColorBackground();
            Digits = _symbol.Digits();
            Point = _symbol.Point();
            SwapMode = _symbol.SwapMode();
            PriceStrike = _symbol.PriceStrike();            
        }
        public string Symbol { get; set; }
        public string Path { get; set; }
        public string ISIN { get; set; }
        public string Description { get; set; }
        public string International { get; set; }
        public string Category { get; set; }
        public string Exchange { get; set; }
        public string CFI { get; set; }
        public CIMTConSymbol.EnSectors Sector { get; set; }
        public CIMTConSymbol.EnIndustries Industry { get; set; }
        public string Country { get; set; }
        public string Basis { get; set; }
        public string Source { get; set; }
        public string Page { get; set; }
        public string CurrencyBase { get; set; }
        public uint CurrencyBaseDigits { get; set; }
        public string CurrencyProfit { get; set; }
        public uint CurrencyProfitDigits { get; set; }
        public string CurrencyMargin { get; set; }
        public uint CurrencyMarginDigits { get; set; }
        public uint Color { get; set; }
        public uint ColorBackground { get; set; }
        public uint Digits { get; set; }
        public double Point { get; set; }
        public double TickSize { get; set; }
        public double ContractSize { get; set; }
        public double TickValue { get; set; }
        public double MarginInitial { get; set; }
        public double MarginMaintenance { get; set; }
        public uint SwapMode { get; set; }
        public double PriceStrike { get; set; }
    }
}
