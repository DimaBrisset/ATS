namespace ATS
{
    public class Tariff : ITariff
    {
        public double CostPerMinute { get; }

        public Tariff()
        {
            CostPerMinute = 10f;
        }
    }
}
