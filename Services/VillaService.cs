using AutoMapper;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Data;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Data.Repositories.Implementation;
using JustclickCoreModules.Requests;
using JustclickCoreModules.Validators;
using JustclickCoreModules.Filters;


namespace MagicVilla_DB.Services
{
    public class VillaService
    {
        private readonly VillaRepository _repository;
        private readonly TownRepository _townRepository;
        private readonly RequestValidator _validator;

        public VillaService(ApplicationDbContext db,
            IMapper mapper,
            RequestValidator validator,
            VillaRepository repository,
            TownRepository townRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
            _validator = validator;
        }

        public List<Villa> FetchAll()
        {
            return _repository.FetchAll();
        }

        public Paginated<Villa> FetchAll(SearchRequest searchRequest)
        {
            return _repository.FetchAll(searchRequest);
        }

        public Villa FetchOne(Guid id)
        {
            Villa? town = _repository.FetchOne(id);
            if (town == null)
            {
                throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
            }
            return town;
        }

        public Villa Create(VillaRequest request)
        {

            if (!_validator.Validate(request))
            {
                throw new InvalidRequestValueException(_validator.Errors);
            }

            Town? town = _townRepository.FetchOne(request.TownId);
            if (town == null)
            {
                throw new InvalidRequestValueException(new Dictionary<string, List<string>> { { "townId", ["ID Kota Tidak Valid"] } });
            }

            Villa villa = _repository.Create(request);
            return villa;
        }

        public Villa Update(Guid id, VillaRequest request)
        {
            if (!_validator.Validate(request))
            {
                throw new InvalidRequestValueException(_validator.Errors);
            }

            Town? town = _townRepository.FetchOne(request.TownId);
            if (town == null)
            {
                throw new InvalidRequestValueException(new Dictionary<string, List<string>> { { "townId", ["ID Kota Tidak Valid"] } });
            }

            Villa? villa = _repository.Update(id, request);
            if (villa == null)
            {
                throw new InvalidRequestValueException(null, "INVALID_ID");
            }

            return villa;
        }

        public List<string> Delete(DeleteRequest request)
        {
            List<string> deletedIds = _repository.Delete(request);
            return deletedIds;
        }
    }
}
