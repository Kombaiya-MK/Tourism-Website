namespace KTWFeedbackAPI.Interfaces
{
    public interface IQueryRepo<T, K>
    {
        Task<T> Get(K key);
        Task<ICollection<T>> GetAll();

    }
}
