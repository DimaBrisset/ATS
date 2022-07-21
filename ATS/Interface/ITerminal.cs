namespace ATS
{
    public interface ITerminal
    {
        event EventHandler<CallEventArgs>? Called;
        event EventHandler<ConnectEventArgs>? Connected;
        event EventHandler<CallEventArgs>? CallTerminate;

        CallErrorCode Call(string PhoneNumber);
        void OnIncomingCall(object? sender, CallEventArgs e);
        void Connect();
        void Disconnect();
        void CallingError(CallEventArgs e);
    }
}
