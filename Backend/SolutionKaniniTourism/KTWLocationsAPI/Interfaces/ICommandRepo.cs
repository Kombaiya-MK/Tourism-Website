namespace KTWLocationsAPI.Interfaces
{
    public interface ICommandRepo<T, K>
    {
        Task<T> Add(T item);
        Task<T> Update(T item);
    }
}
