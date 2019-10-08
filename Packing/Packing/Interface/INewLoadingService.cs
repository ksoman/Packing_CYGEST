using PackingCygest.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PackingCygest.Interface
{
    /// <summary>
    /// Interface New Loading service
    /// </summary>
    public interface INewLoadingService
    {
        /// <summary>
        /// Gets the rubric item asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ObservableCollection<RubricsModel>> GetRubricItemAsync();
        /// <summary>
        /// Gets the rooms asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ObservableCollection<RoomsModel>> GetRoomsAsync();
        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ObservableCollection<ItemModel>> GetItemAsync();


    }
}
