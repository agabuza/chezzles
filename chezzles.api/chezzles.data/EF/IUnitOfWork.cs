namespace chezzles.data.EF
{
    public interface IUnitOfWork
    {
        void Save();
        void SaveAsync();
    }
}