namespace ATS
{
    public class Call
    {
        public Call(CallEventArgs e, ITariff tariff)
        {
            CallerPhoneNumber = e.CallerPhoneNumber;
            TargetPhoneNumber = e.TargetPhoneNumber;
            ResponseCode = e.ResponseCode;
            ErrorCode = e.ErrorCode;
            DateTimeBegin = e.DateTimeBeginCall;
            Duration = (e.DataTimeEndCall - e.DateTimeBeginCall).TotalSeconds;
            Cost = Duration * (tariff.CostPerMinute / 10);
        }

        public string CallerPhoneNumber { get; }

        public string TargetPhoneNumber { get; }

        public CallResponseCode ResponseCode { get; }

        public CallErrorCode ErrorCode { get; }

        public DateTime DateTimeBegin { get; }

        public double Duration { get; }

        public double Cost { get; }
    }
}
