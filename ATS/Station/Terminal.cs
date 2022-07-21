namespace ATS
{
    public class Terminal : ITerminal
    {
        private readonly ILogger logger = new ConsoleLogger();
        public event EventHandler<CallEventArgs>? Called;
        public event EventHandler<ConnectEventArgs>? Connected;
        public event EventHandler<CallEventArgs>? CallTerminate;

        public void OnIncomingCall(object? sender, CallEventArgs e)
        {
            Console.WriteLine($"Incomming call by {e.CallerPhoneNumber}. Do you want to accept that? [y/n]");

            string? answer;
            do
            {
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    AcceptCall(e);
                    Console.WriteLine("Call accepted");
                }
                else if (answer == "n")
                {
                    RejectCall(e);
                    Console.WriteLine("Call rejected");
                }
            }
            while (answer != "y" && answer != "n");

            if (answer == "n")
            {
                e.DataTimeEndCall = DateTime.Now;
                CallTerminate?.Invoke(this, e);
                Console.WriteLine("Call terminate");
                return;
            }
            answer = "";

            while (answer != "y")
            {
                Console.WriteLine("Terminate Call? [y/n]");
                answer = Console.ReadLine();

                if (answer == "y")
                {
                    e.DataTimeEndCall = DateTime.Now;
                    CallTerminate?.Invoke(this, e);
                    Console.WriteLine("Call terminate");
                }
            }

        }

        public CallErrorCode Call(string targetPhoneNumber)
        {
            var eventArgs = new CallEventArgs(targetPhoneNumber)
            {
                DateTimeBeginCall = DateTime.Now
            };

            Called?.Invoke(this, eventArgs);

            return eventArgs.ErrorCode;
        }

        public void CallingError(CallEventArgs e)
        {
            logger.Log("Call Error : " + e.ErrorCode.ToString());
        }

        private void AcceptCall(CallEventArgs e)
        {
            e.ErrorCode = CallErrorCode.OK;
            e.ResponseCode = CallResponseCode.ACCEPT;
        }

        private void RejectCall(CallEventArgs e)
        {
            e.ErrorCode = CallErrorCode.OK;
            e.ResponseCode = CallResponseCode.REJECT;
        }

        public void Connect()
        {
            Connected?.Invoke(this, new ConnectEventArgs(PortStatus.CONNECTED));
        }

        public void Disconnect()
        {
            Connected?.Invoke(this, new ConnectEventArgs(PortStatus.FREE));
        }
    }
}
