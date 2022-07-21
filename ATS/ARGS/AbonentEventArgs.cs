namespace ATS
{
    public class AbonentEventArgs : EventArgs
    {
        public ICollection<Call> Calls { get; set; }

        public AbonentEventArgs()
        {
            Calls = new List<Call>();
        }
    }
}
