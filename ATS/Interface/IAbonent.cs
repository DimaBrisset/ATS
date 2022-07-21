namespace ATS
{
    public interface IAbonent
    {
        ITerminal Terminal { get; }
        double Balance { get; set; }
        string PhoneNumber { get; }
        ITariff Tariff { get; set; }

        event EventHandler<AbonentEventArgs> CallReportRequest;

        void TopUpBalance(double value);
        ICollection<Call> GetCallReport();
    }
}
