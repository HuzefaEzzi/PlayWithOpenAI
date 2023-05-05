// See https://aka.ms/new-console-template for more information
using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.Tokenizer.GPT3;
using PlayWithOpenAI;
using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;

Console.WriteLine("Welcome!!");
EmbeddingFile embeddingFile;
if (FileHelper.HasDestination() == false)
{
    var sections = FileHelper.GetSectionsFineTuned();

    var embeddings = await OpenApiHelper.GetEmbeddings(sections);
    var file = new EmbeddingFile()
    {
        Sections = sections.Select((s, i) => new PlayWithOpenAI.Section() { Content = s, Index = i }).ToList(),
    };

    FileHelper.WriteEmbeddings(file, embeddings);
    embeddingFile = file;
}
else
{
    embeddingFile = JsonConvert.DeserializeObject<EmbeddingFile>(FileHelper.GetStoredEmbeddings());
}
while (true)
{
    await ProgramHelper.Exec(embeddingFile);
}

Console.ReadLine();
