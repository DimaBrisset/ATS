namespace ATS
{
    public class Operator : IOperator
    {
        private readonly ICollection<IAbonent> _abonentList;
        private readonly ICollection<Call> _callList;
        private readonly ICollection<IStation> _stationList;

        public Operator()
        {
            _abonentList = new List<IAbonent>();
            _callList = new List<Call>();
            _stationList = new List<IStation>();
        }

        public void AddAbonent(IAbonent? abonent, IStation station)
        {
            if (abonent != null)
            {
                _abonentList.Add(abonent);
                abonent.CallReportRequest += OnCallReportRequest;
                IPort port = new Port(abonent.PhoneNumber, abonent.Terminal);
                station.Ports.Add(port);
                port.OutcomingCall += station.CallingRequest;
                port.CallTerminate += station.OnCallTerminate;
            }
        }

        private IStation GetStationByPhoneNumberContains(string value)
        {
            foreach (var st in _stationList)
            {
                if (st.Ports.FirstOrDefault(p => p.PhoneNumber == value) != null)
                {
                    return st;
                }
            }
            return new Station();
        }

        public void DeleteAbonent(IAbonent abonent)
        {
            IStation? station = GetStationByPhoneNumberContains(abonent.PhoneNumber);
            var port = station.Ports.FirstOrDefault(p => p.PhoneNumber == abonent.PhoneNumber);
            if (port != null)
            {
                port.OutcomingCall -= station.CallingRequest;
                port.CallTerminate -= station.OnCallTerminate;
                _abonentList.Remove(abonent);
            }
        }

        public void AddStation(IStation station)
        {
            station.BalanceRequest += OnBalanceRequest;
            station.CallRecordSend += OnCallRecordSend;
            _stationList.Add(station);

        }

        public void RemoveStation(IStation station)
        {
            station.BalanceRequest -= OnBalanceRequest;
            station.CallRecordSend -= OnCallRecordSend;
            _stationList.Remove(station);
        }

        private void OnBalanceRequest(object? sender, BalanceEventArgs e)
        {

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            e.Balance = _abonentList.FirstOrDefault(a => a.PhoneNumber == e.PhoneNumber).Balance;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.


        }

        private void OnCallRecordSend(object? sender, CallEventArgs e)
        {
            IAbonent? abonent = _abonentList.FirstOrDefault(a => a.PhoneNumber == e.CallerPhoneNumber);
            if (abonent != null)
            {
                Call call = new(e, abonent.Tariff);
                abonent.Balance -= call.Cost;
                _callList.Add(call);
            }
        }

        private void OnCallReportRequest(object? sender, AbonentEventArgs e)
        {
            var abonent = sender as IAbonent;
            foreach (var call in _callList)
            {
                if (abonent != null)
                {
                    if (call.CallerPhoneNumber == abonent.PhoneNumber || call.TargetPhoneNumber == abonent.PhoneNumber)
                    {
                        e.Calls.Add(call);
                    }
                }
            }
        }

        public ICollection<IAbonent> GetAbonentList()
        {
            return _abonentList;
        }
    }
}
