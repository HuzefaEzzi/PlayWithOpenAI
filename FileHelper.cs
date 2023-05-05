using OpenAI.GPT3.ObjectModels.ResponseModels;
using OpenAI.GPT3.Tokenizer.GPT3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithOpenAI
{
    internal static class FileHelper
    {
        const string source = "D:\\playground\\PlayWithOpenAI\\nnt-medium-text-demo.txt";
        const string destination = "D:\\playground\\PlayWithOpenAI\\serialized.json";

        public static bool HasDestination()
        {
            return File.Exists(destination);
        }

        public static string GetStoredEmbeddings()
        {
            string content = File.ReadAllText(destination);
            return content;
        }

        public static List<string> GetSections()
        {
            string fileContent = File.ReadAllText(source);

            List<string> sections = new List<string>();
            foreach (var item in fileContent.Split(Environment.NewLine))
            {
                if (string.IsNullOrWhiteSpace(item.Trim()) == false)
                {

                    int c = TokenizerGpt3.TokenCount(item);
                    if (c < 1000)
                    {
                        sections.Add(item);
                    }
                    else
                    {
                        Console.WriteLine("Skipped" + item);
                    }
                }
            }
            return sections;
        }

        public static List<string> GetSectionsFineTuned()
        {
            string fileContent = File.ReadAllText(source);

            List<string> sections = new List<string>();
            foreach (var paragrapgh in fileContent.Split(Environment.NewLine))
            {
                if (string.IsNullOrWhiteSpace(paragrapgh.Trim()) == false)
                {
                    foreach (var sentence in paragrapgh.Split('.'))
                    {
                        sections.Add(sentence);
                    }
                }
            }
            return sections;
        }

        public static void WriteEmbeddings(EmbeddingFile file, List<EmbeddingResponse> embeddingRespones)
        {
            foreach (var section in file.Sections)
            {
                section.Vector = embeddingRespones?[section.Index]?.Embedding;
            }
            File.WriteAllText(destination, Newtonsoft.Json.JsonConvert.SerializeObject(file));
        }
    }
}