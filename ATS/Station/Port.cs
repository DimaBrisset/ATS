namespace ATS
{
    class Port : IPort, IDisposable
    {
        public PortStatus Status { get; set; }
        public string PhoneNumber { get; set; }

        private ITerminal _terminal;

        public event EventHandler<CallEventArgs>? IncomingCall;
        public event EventHandler<CallEventArgs>? OutcomingCall;
        public event EventHandler<CallEventArgs>? CallTerminate;

        public Port(string phoneNumber, ITerminal terminal)
        {
            Status = PortStatus.FREE;
            PhoneNumber = phoneNumber;
            _terminal = terminal;
            _terminal.Connected += OnTerminalStatusRequest;
        }

        public void Dispose()
        {
            _terminal.Connected -= OnTerminalStatusRequest;
        }

        private ConnectionError Connect()
        {
            if (Status != PortStatus.CONNECTED)
            {
                IncomingCall += _terminal.OnIncomingCall;

                _terminal.Called += OnOutcomingCall;
                _terminal.CallTerminate += OnCallTerminate;
                Status = PortStatus.CONNECTED;

                return ConnectionError.OK;
            }
            else
            {
                return ConnectionError.ALREADY_CONNECTED;
            }
        }

        private void Disconnect()
        {
            if (Status != PortStatus.FREE)
            {
                IncomingCall -= _terminal.OnIncomingCall;

                _terminal.Called -= OnOutcomingCall;
                _terminal.CallTerminate -= OnCallTerminate;

                Status = PortStatus.FREE;
            }
        }

        public void OnIncomingCall(CallEventArgs e)
        {
            Status = PortStatus.BUSY;
            IncomingCall?.Invoke(this, e);
        }

        private void OnOutcomingCall(object? sender, CallEventArgs e)
        {
            Status = PortStatus.BUSY;
            e.CallerPhoneNumber = PhoneNumber;
            OutcomingCall?.Invoke(sender, e);
        }

        public void CallingErrorMessage(CallEventArgs e)
        {
            _terminal.CallingError(e);
        }

        private void OnCallTerminate(object? sender, CallEventArgs e)
        {
            CallTerminate?.Invoke(sender, e);
        }

        private void OnTerminalStatusRequest(object? sender, ConnectEventArgs e)
        {
            if (e.Status == PortStatus.CONNECTED)
            {
                Connect();
            }
            else
            {
                Disconnect();
            }
        }


    }
}
