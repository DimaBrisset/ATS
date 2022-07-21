namespace ATS
{
    public interface IPort
    {
        string PhoneNumber { get; set; }
        PortStatus Status { get; set; }

        event EventHandler<CallEventArgs>? IncomingCall;
        event EventHandler<CallEventArgs>? OutcomingCall;
        event EventHandler<CallEventArgs>? CallTerminate;

        void OnIncomingCall(CallEventArgs e);
        void CallingErrorMessage(CallEventArgs e);
    }
}
