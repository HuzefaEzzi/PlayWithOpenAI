using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithOpenAI
{
    internal class ProgramHelper
    {
        public static async Task Exec(EmbeddingFile embeddingFile)
        {
            Console.WriteLine("Please enter text to search");

            string searchtext = Console.ReadLine()?.Trim();

            if (embeddingFile != null && string.IsNullOrWhiteSpace(searchtext) == false)
            {
                var searchEmbedding = await OpenApiHelper.GetEmbeddings(new List<string>() { searchtext });
                List<SimilarSections> sections = new List<SimilarSections>();
                foreach (var storedSection in embeddingFile.Sections)
                {
                    var similiar = new SimilarSections();
                    similiar.Section = storedSection;
                    similiar.Similarity = OpenApiHelper.GetCosineSimilarity(storedSection.Vector, searchEmbedding[0].Embedding);
                    sections.Add(similiar);
                }
                var filtered = sections.OrderBy(s => s.Similarity).Take(10);
                var filteredText = string.Join("\n\n###\n\n", filtered.Select(s => s.Section.Content));
                  Console.WriteLine("Filtered: " + filteredText + Environment.NewLine);
                string prompt = OpenApiHelper.BuildPrompt(filteredText, searchtext);
                 Console.WriteLine("Prompt: " + prompt + Environment.NewLine);
                var complitions = await OpenApiHelper.CallCompletion(prompt);
                foreach (var complition in complitions)
                {
                    Console.WriteLine("Complition 1: " + complition.Text + Environment.NewLine);
                    Console.WriteLine("Complition 1 Completeion Reason: " + complition.FinishReason + Environment.NewLine);
                }

                Console.WriteLine("Search Complete!");


            }

        }
    }
}
