namespace ATS
{
    public interface IStation
    {
        ICollection<IPort> Ports { get; }

        event EventHandler<CallEventArgs>? CallRecordSend;
        event EventHandler<BalanceEventArgs>? BalanceRequest;

        IPort GetPortByPhoneNumber(string value);
        double GetBalanceByPhoneNumber(string value);
        void OnCallTerminate(object? sender, CallEventArgs e);
        ICollection<string> GetPortStatusList();
        void CallingRequest(object? sender, CallEventArgs e);

    }
}
