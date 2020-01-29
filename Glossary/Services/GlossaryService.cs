using Glossary.Models;
using Glossary.Models.PaginationModels;
using Glossary.Repository.Glossary;
using Glossary.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glossary.Services
{
    public class GlossaryService : IGlossaryService<GlossaryModel>
    {
        private readonly IGlossaryRepository _repository;

        public GlossaryService(IGlossaryRepository repository)
        {
            this._repository = repository;

        }

        public async Task<ResponseModel<GlossaryModel>> Get(string term)
        {
            var response = await _repository.Get(term);

            return new ResponseModel<GlossaryModel>()
            {
                Count = 1,
                PageNumber = 1,
                PageSize = 1,
                Records = new List<GlossaryModel>() { response }

            };
        }
        public async Task<ResponseModel<GlossaryModel>> Search(RequestModel<string> request)
        {
            var response = await _repository.Search(request.FilterModel);
            return response.Paginate(request.PageSize, request.PageNumber);
        }

        public async Task<ResponseModel<GlossaryModel>> GetByCharacter(RequestModel<char> request)
        {
            var response = await _repository.GetByCharacter(request.FilterModel);
            return response.Paginate(request.PageSize, request.PageNumber);
        }

        public async Task<GlossaryModel> Create(GlossaryModel model)
        {
            if (model.IsValid())
                _repository.Add(model);
            else
                throw new GlossaryException("The glossary is not valid");
            return model;
        }

        public async Task<GlossaryModel> Update(GlossaryModel model)
        {
            _repository.Update(model);
            return model;
        }

        public void Delete(string term)
        {
            _repository.Remove(term);
        }


    }
}