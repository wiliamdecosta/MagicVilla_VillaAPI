using JustclickCoreModules.Requests;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Utils.Filters;

namespace MagicVilla_DB.Data.Repositories.Abstract
{
    public interface IVillaRepository
    {
        List<Villa> FetchAll();

        Paginated<Villa> FetchAll(SearchRequest request);
        Villa? FetchOne(Guid id);

        Villa Create(VillaRequest request);

        Villa? Update(Guid id, VillaRequest request);

        List<string> Delete(DeleteRequest ids);
    }
}
