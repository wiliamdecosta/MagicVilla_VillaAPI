using JustclickCoreModules.Filters;
using JustclickCoreModules.Requests;
using JustclickCoreModules.Validators;
using MagicVilla_DB.Data.Repositories.Implementation;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Utils.Filters;

namespace MagicVilla_DB.Services
{
    public class TownService
    {
        private readonly TownRepository _repository;
        private readonly RequestValidator _validator;

        public TownService(RequestValidator validator, TownRepository repository)
        {
            _repository = repository;
            _validator = validator;
        }

        public List<Town> FetchAll()
        {
            return _repository.FetchAll();
        }

        public Paginated<Town> FetchAll(SearchRequest searchRequest)
        {
            return _repository.FetchAll(searchRequest);
        }

        public Town FetchOne(Guid id)
        {
            Town? town = _repository.FetchOne(id);
            if (town == null)
            {
                throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
            }
            return town;
        }

        public Town Create(TownRequest request)
        {
            if (!_validator.Validate(request))
            {
                throw new InvalidRequestValueException(_validator.Errors);
            }

            Town town = _repository.Create(request);
            return town;
        }

        public Town Update(Guid id, TownRequest request)
        {
            if (!_validator.Validate(request))
            {
                throw new InvalidRequestValueException(_validator.Errors);
            }

            Town? town = _repository.Update(id, request);
            if (town == null)
            {
                throw new InvalidRequestValueException(null, "INVALID_ID");
            }

            return town;
        }

        public List<string> Delete(DeleteRequest request)
        {
            List<string> deletedIds = _repository.Delete(request);
            return deletedIds;
        }
    }
}
