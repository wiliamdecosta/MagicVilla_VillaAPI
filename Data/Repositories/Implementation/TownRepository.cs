using AutoMapper;
using Azure.Core;
using JustclickCoreModules.Filters;
using JustclickCoreModules.Requests;
using MagicVilla_DB.Data.Repositories.Abstract;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Utils.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MagicVilla_DB.Data.Repositories.Implementation
{
    public class TownRepository : ITownRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly FilterUtil<Town> _filterUtil;

        public TownRepository(ApplicationDbContext db, IMapper mapper, FilterUtil<Town> filterUtil)
        {
            _filterUtil = filterUtil;
            _db = db;
            _mapper = mapper;
        }


        public List<Town> FetchAll()
        {
            return _db.Towns.ToList();
        }

        public Paginated<Town> FetchAll(SearchRequest searchRequest)
        {
            List<string> searchKeywordColumns = new List<string>();
            searchKeywordColumns.Add("Name");

            Paginated<Town> query = _filterUtil.GetContent(searchRequest, searchKeywordColumns);
            return query;
        }

        public Town? FetchOne(Guid id)
        {
            Town? town = _db.Towns.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return town;
        }

        public Town Create(TownRequest request)
        {
            Town town = _mapper.Map<TownRequest, Town>(request);
            town.Id = Guid.NewGuid();

            _db.Towns.Add(town);
            _db.SaveChanges();

            return town;
        }

        public List<string> Delete(DeleteRequest ids)
        {
            List<string> deletedIds = [];
            foreach (string townId in ids.Ids)
            {
                var town = _db.Towns.Include(v => v.Villas).FirstOrDefault(x => x.Id.ToString() == townId);
                if (town == null) continue;
                _db.Villas.RemoveRange(town.Villas);
                _db.Towns.Remove(town);
                _db.SaveChanges();
                deletedIds.Add(townId);
            }

            return deletedIds;
        }

        public Town? Update(Guid id, TownRequest request)
        {

            Town? town = _db.Towns.FirstOrDefault(x => x.Id == id);
            if (town == null) return town;

            _mapper.Map<TownRequest, Town>(request, town);

            _db.Towns.Update(town);
            _db.SaveChanges();

            return town;
        }

    }
}
