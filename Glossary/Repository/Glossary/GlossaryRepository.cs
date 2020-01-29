using Glossary.Models;
using Glossary.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glossary.Repository.Glossary
{
    public class GlossaryRepository : IGlossaryRepository
    {

        public GlossaryStoreContext Context;

        public GlossaryRepository(GlossaryStoreContext context)
        {
            Context = context;
        }

        public void Add(GlossaryModel entity)
        {
            if (GlossaryExist(entity)) //  exist
                throw new GlossaryException("Glossary already exists");
            Context.Glossaries.Add(entity);

            Context.Save();
        }

        public void Update(GlossaryModel entity)
        {
            if (!GlossaryExist(entity)) // not exist
                throw new GlossaryException("Glossary does not exist");
            Context.Glossaries.Find(model => model.Term == entity.Term).Text = entity.Text;
            Context.Save();
        }

        public void Remove(string term)
        {
            var glossary = Context.Glossaries.Find(model => model.Term == term);
            if (glossary == null) // not exist
                throw new GlossaryException("Glossary does not exist");
            Context.Glossaries.Remove(glossary);
            Context.Save();
        }

        public async Task<GlossaryModel> Get(string term)
        {

            var result = await Task.Run(() => Context.Glossaries.Find(model => model.Term == term));
            if (result == null) // not exist
                throw new GlossaryException("Glossary does not exist");
            return result;
        }

        public async Task<List<GlossaryModel>> Search(string keyword)
        {
            var result = await Task.Run(() => Context.Glossaries.Contains(keyword));
            return result;
        }


        public async Task<List<GlossaryModel>> GetByCharacter(char c)
        {
            var result = await Task.Run(() => Context.Glossaries.StartsWith(c));
            return result;
        }

        private bool GlossaryExist(GlossaryModel g)
        {
            if (Context.Glossaries.Find(model => model.Term == g.Term) == null) //not exists 
                return false;

            return true;
        }


    }
}
