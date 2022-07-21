namespace ATS
{
    interface IOperator
    {
        void AddAbonent(IAbonent abonent, IStation station);
        void DeleteAbonent(IAbonent abonent);
        void AddStation(IStation station);
        ICollection<IAbonent> GetAbonentList();
    }
}
