namespace ATS
{
    public static class AbonentExtention
    {
        public static ICollection<Call> Filter(this ICollection<Call> calls, FilterParameters param, string value)
        {
            ICollection<Call> callList = calls;

            ICollection<Call> result = new List<Call>();
            switch (param)
            {
                case FilterParameters.ABONENT:
                    {
                        FilterByAbonent(value, callList, result);
                        break;
                    }
                case FilterParameters.CALL_DATE:
                    {
                        FilterByDate(value, callList, result);
                        break;
                    }
                case FilterParameters.COST:
                    {
                        FilterByCost(value, callList, result);
                        break;
                    }
            }
            return result;
        }

        private static void FilterByAbonent(string value, ICollection<Call> callList, ICollection<Call> result)
        {
            foreach (var call in callList)
            {
                if (call.TargetPhoneNumber == value)
                {
                    result.Add(call);
                }
            }
        }

        private static void FilterByDate(string value, ICollection<Call> callList, ICollection<Call> result)
        {
            DateTime date = Convert.ToDateTime(value).Date;
            Console.WriteLine(date);
            foreach (var call in callList)
            {
                if (call.DateTimeBegin.Date == date)
                {
                    result.Add(call);
                }
            }
        }

        private static void FilterByCost(string value, ICollection<Call> callList, ICollection<Call> result)
        {
            double cost = double.Parse(value);
            foreach (var call in callList)
            {
                if (call.Cost > cost - 2 && call.Cost < cost + 2)
                {
                    result.Add(call);
                }
            }
        }
    }
}
