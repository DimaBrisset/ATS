namespace ATS
{
    public class BalanceEventArgs : EventArgs
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public double Balance { get; set; }
    }
}
