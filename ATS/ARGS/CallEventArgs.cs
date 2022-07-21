namespace ATS
{
    public class CallEventArgs : EventArgs
    {
        public string CallerPhoneNumber { get; set; }

        public string TargetPhoneNumber { get; }

        public CallResponseCode ResponseCode { get; set; }

        public CallErrorCode ErrorCode { get; set; }

        public DateTime DateTimeBeginCall { get; set; }

        public DateTime DataTimeEndCall { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public CallEventArgs(string targetPhoneNumber)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            TargetPhoneNumber = targetPhoneNumber;
            ErrorCode = CallErrorCode.UNKNOWN;
            ResponseCode = CallResponseCode.UNKNOWN;
        }
    }
}
