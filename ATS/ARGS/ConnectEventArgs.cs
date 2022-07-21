namespace ATS
{
    public class ConnectEventArgs : EventArgs
    {
        public PortStatus Status { get; }

        public ConnectEventArgs(PortStatus status)
        {
            Status = status;
        }
    }
}
