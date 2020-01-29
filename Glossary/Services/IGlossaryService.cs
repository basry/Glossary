
using Glossary.Models.PaginationModels;
using System;
using System.Threading.Tasks;

namespace Glossary.Services
{
    public interface IGlossaryService<T>
    {
        Task<ResponseModel<T>> Get(string term);
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task<ResponseModel<T>> Search(RequestModel<string> request);
        Task<ResponseModel<T>> GetByCharacter(RequestModel<Char> request);
        void Delete(string term);
    }
}
