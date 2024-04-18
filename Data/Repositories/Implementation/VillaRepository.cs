using AutoMapper;
using JustclickCoreModules.Requests;
using MagicVilla_DB.Data.Repositories.Abstract;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_DB.Data.Repositories.Implementation
{
    public class VillaRepository : IVillaRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly FilterUtil<Villa> _filterUtil;

        public VillaRepository(ApplicationDbContext db, IMapper mapper, FilterUtil<Villa> filterUtil)
        {
            _db = db;
            _mapper = mapper;
            _filterUtil = filterUtil;
        }

        public List<Villa> FetchAll()
        {
            return _db.Villas.Include(obj => obj.Town).ToList();
        }

        public Paginated<Villa> FetchAll(SearchRequest searchRequest)
        {
            List<string> searchKeywordsColumns = new List<string>();
            searchKeywordsColumns.Add("Name");
            searchKeywordsColumns.Add("Details");

            Paginated<Villa> query = _filterUtil.GetContent(searchRequest, searchKeywordsColumns);
            query.Data = query.Data.Include(obj => obj.Town);
            return query;
        }

        public Villa? FetchOne(Guid id)
        {
            return _db.Villas.Include(obj => obj.Town).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public Villa Create(VillaRequest request)
        {
            Villa newVilla = _mapper.Map<Villa>(request);
            newVilla.Id = Guid.NewGuid();
            newVilla.CreatedDate = DateTime.Now;
            newVilla.UpdatedDate = DateTime.Now;

            _db.Villas.Add(newVilla);
            _db.SaveChanges();

            return _db.Villas.Include(obj => obj.Town).First(x => x.Id == newVilla.Id);
        }


        public Villa? Update(Guid id, VillaRequest request)
        {
            Villa? villa = _db.Villas.Include(obj => obj.Town).AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (villa == null) return villa;

            _mapper.Map<VillaRequest, Villa>(request, villa);
            villa.UpdatedDate = DateTime.Now;

            _db.Villas.Update(villa);
            _db.SaveChanges();
            return villa;
        }

        public List<string> Delete(DeleteRequest ids)
        {
            List<string> deletedIds = [];
            foreach (string villaId in ids.Ids)
            {
                var villa = _db.Villas.FirstOrDefault(x => x.Id.ToString() == villaId);
                _db.Villas.Remove(villa);
                _db.SaveChanges();
                deletedIds.Add(villaId);
            }
            return deletedIds;
        }

    }
}
