using System.Threading.Tasks;

namespace GloboTicket.Promotion.Data
{
    public interface INotifier<T>
    {
        Task Notify(T entityAdded);
    }
}
