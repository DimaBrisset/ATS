namespace ATS
{
    public static class CallExtention
    {
        public static void Write(this Call call, ILogger logger)
        {
            string response = "";
            string error = "";

            switch (call.ErrorCode)
            {
                case CallErrorCode.BUSY:
                    {
                        error = "BUSY";
                        break;
                    }
                case CallErrorCode.OK:
                    {
                        error = "OK";
                        break;
                    }
                case CallErrorCode.NOT_ENOUGH_MONEY:
                    {
                        error = "NOT_ENOUGH_MONEY";
                        break;
                    }
                case CallErrorCode.WRONG_NUMBER:
                    {
                        error = "WRONG_NUMBER";
                        break;
                    }
                case CallErrorCode.UNKNOWN:
                    {
                        error = "UNKNOWN";
                        break;
                    }
                case CallErrorCode.NOT_EXIST:
                    {
                        error = "NOT_EXIST";
                        break;
                    }
            }

            switch (call.ResponseCode)
            {
                case CallResponseCode.ACCEPT:
                    {
                        response = "ACCEPT";
                        break;
                    }
                case CallResponseCode.REJECT:
                    {
                        response = "REJECT";
                        break;
                    }
                case CallResponseCode.UNKNOWN:
                    {
                        response = "UNKNOWN";
                        break;
                    }
            }

            logger.Log($"Caller:  {call.CallerPhoneNumber} Target: {call.TargetPhoneNumber}");
            logger.Log($"Date: {call.DateTimeBegin} Duration: {call.Duration:0:00}");
            logger.Log($"Response: {response} Error: {error}");
            logger.Log($"Cost: {call.Cost}");
        }
    }
}
