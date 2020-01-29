using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SDKGen
{
    class Program
    {
        static string baseUrl = "http://localhost:60442/";

        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            tasks.Add(GenerateGlossarySdk());

            Task.WaitAll(tasks.ToArray());
            Console.ReadLine();
        }

        private static async Task GenerateGlossarySdk()
        {
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";
            var url = baseUrl + "swagger/v1/swagger.json";
            var document = await SwaggerDocument.FromUrlAsync(url);

            var settings = new SwaggerToCSharpClientGeneratorSettings
            {
                ClassName = "GlossaryClient",
                GenerateClientInterfaces = true,
                OperationNameGenerator = new SingleClientFromOperationIdOperationNameGenerator(),
                ExceptionClass = "GlossaryClientException",
                InjectHttpClient = true,
                CSharpGeneratorSettings =
                    {
                        Namespace = "Glossary.Client",
                        ClassStyle = NJsonSchema.CodeGeneration.CSharp.CSharpClassStyle.Poco,
                        ArrayType= "System.Collections.Generic.List"
                    }
            };

            var generator = new SwaggerToCSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            code = code.Replace("Required = Newtonsoft.Json.Required.Always", "Required = Newtonsoft.Json.Required.Default");
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\Client\\GlossaryClient.cs"));
            File.WriteAllText(path, code);
            Console.WriteLine("Glossary sdk generated");
        }
    }
}
