namespace PrettyLink.Domain.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Fenris.Validation.ArgumentValidation;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class WordDataProvider : IWordDataProvider
    {
        private readonly IList<string> adjectives;

        private readonly IList<string> nouns;

        private readonly Random random = new Random();

        public WordDataProvider([NotNull] string filename)
        {
            filename.ShouldNotBeNullOrEmpty(nameof(filename));

            if (File.Exists(filename) == false)
            {
                throw new FileNotFoundException("Unable to locate dictionary file.", filename);
            }

            using (var reader = File.OpenText(filename))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var dictionaryFile = (JObject)JToken.ReadFrom(jsonReader);

                    nouns = dictionaryFile["Nouns"].ToObject<List<string>>();
                    adjectives = dictionaryFile["Adjectives"].ToObject<List<string>>();
                }
            }
        }

        public string GetAdjective()
        {
            return adjectives[random.Next(0, adjectives.Count - 1)];
        }

        public string GetNoun()
        {
            return nouns[random.Next(0, adjectives.Count - 1)]; 
        }
    }
}
