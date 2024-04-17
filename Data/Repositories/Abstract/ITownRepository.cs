using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Utils.Filters;

namespace MagicVilla_DB.Data.Repositories.Abstract
{
    public interface ITownRepository
    {
        List<Town> FetchAll();

        Paginated<Town> FetchAll(SearchRequest request);
        Town? FetchOne(Guid id);

        Town Create(TownRequest request);

        Town? Update(Guid id, TownRequest request);

        List<string> Delete(DeleteRequest ids);
    }
}
