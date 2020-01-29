using Glossary.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Glossary
{

    public class GlossaryStoreContext
    {

        private readonly string _filename;

        public GlossaryStoreContext(string filename)
        {
            if (File.Exists(filename))
                _glossaries = JsonConvert.DeserializeObject<List<GlossaryModel>>(File.ReadAllText(filename));
            _filename = filename;
        }

        public void Save()
        {
            File.WriteAllText(_filename, JsonConvert.SerializeObject(this.Glossaries));
            using (StreamWriter file = File.CreateText(_filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, this.Glossaries);
            }
        }

        public List<GlossaryModel> Glossaries => _glossaries ?? (_glossaries = new List<GlossaryModel>());

        private List<GlossaryModel> _glossaries { get; set; }
    }


}