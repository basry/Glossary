using Glossary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glossary.Repository.Glossary
{
    public interface IGlossaryRepository : IRepository<GlossaryModel>
    {
        Task<List<GlossaryModel>> Search(string id);
        Task<List<GlossaryModel>> GetByCharacter(char c);
    }

}