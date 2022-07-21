namespace ATS
{
    class Abonent : IAbonent
    {
        public ITerminal Terminal { get; private set; }
        public double Balance { get; set; }
        public string PhoneNumber { get; }
        public ITariff Tariff { get; set; }

        public event EventHandler<AbonentEventArgs>? CallReportRequest;

        public Abonent(ITariff tariff, string phoneNumber, ITerminal terminal)
        {
            Balance = 0;
            Tariff = tariff;
            PhoneNumber = phoneNumber;
            Terminal = terminal;
        }

        public void TopUpBalance(double value)
        {
            Balance += value;
        }

        public ICollection<Call> GetCallReport()
        {
            var eventArgs = new AbonentEventArgs();
            CallReportRequest?.Invoke(this, eventArgs);

            return eventArgs.Calls;
        }
    }
}
