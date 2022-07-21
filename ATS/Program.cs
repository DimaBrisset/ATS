namespace ATS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITariff tariff = new Tariff();
            IOperator teleOperator = new Operator();
            IStation station = new Station();

            ILogger logger = new ConsoleLogger();

            teleOperator.AddStation(station);

            IAbonent abonent1 = new Abonent(tariff, "+375444444444", new Terminal());
            IAbonent abonent2 = new Abonent(tariff, "+375299999999", new Terminal());
            IAbonent abonent3 = new Abonent(tariff, "+375333333333", new Terminal());

            teleOperator.AddAbonent(abonent1, station);
            teleOperator.AddAbonent(abonent2, station);
            teleOperator.AddAbonent(abonent3, station);

            abonent1.TopUpBalance(40);
            abonent2.TopUpBalance(10);

            abonent1.Terminal.Connect();
            abonent2.Terminal.Connect();

            foreach (var port in station.GetPortStatusList())
            {
                logger.Log(port);
            }

            abonent1.Terminal.Call("+375299999999");
            abonent2.Terminal.Call("+375333333333");
   
            abonent1.Terminal.Call("+375299999999");
            abonent3.Terminal.Call("+375299999999");
        
            foreach (var port in station.GetPortStatusList())
            {
                logger.Log(port);
            }

            ICollection<Call> calls = abonent1.GetCallReport().Filter(FilterParameters.ABONENT, "+375299999999");

            foreach (var call in calls)
            {
                call.Write(logger);
            }

            teleOperator.DeleteAbonent(abonent3);


            Console.ReadKey();
        }
    }
}