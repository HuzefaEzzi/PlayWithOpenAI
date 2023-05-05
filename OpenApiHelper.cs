using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.GPT3.ObjectModels.SharedModels;

namespace PlayWithOpenAI
{
    internal static class OpenApiHelper
    {
        public static OpenAIService Service { get; } = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = "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"//Environment.GetEnvironmentVariable("MY_OPEN_AI_API_KEY")
        });

        public static async Task<List<EmbeddingResponse>> GetEmbeddings(List<string> input)
        {

            var embeddingResult = await Service.Embeddings.CreateEmbedding(new EmbeddingCreateRequest()
            {
                InputAsList = input,
                Model = Models.TextSearchAdaDocV1,
            });

            if (embeddingResult.Successful)
            {
                return embeddingResult.Data;
            }
            else
            {
                if (embeddingResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{embeddingResult.Error.Code}: {embeddingResult.Error.Message}");
                return null;
            }
        }
        public static double GetCosineSimilarity(List<double> V1, List<double> V2)
        {
            int N = 0;
            N = ((V2.Count < V1.Count) ? V2.Count : V1.Count);
            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;
            for (int n = 0; n < N; n++)
            {
                dot += V1[n] * V2[n];
                mag1 += Math.Pow(V1[n], 2);
                mag2 += Math.Pow(V2[n], 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }

        public static string BuildPrompt(string context, string orignalQuery)
        {
            return @$"Answer the question as truthfully as possible using the provided context, and if the answer is not contained within the text below, say 'I dont know' 
Context:{context}
Q:{orignalQuery}
A:";
        }

        public static async Task<List<ChoiceResponse>> CallCompletion(string input)
        {
            var response = await Service.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Model = Models.TextSearchAdaDocV1,
                Prompt = input,
               // MaxTokens = 2049
            });
            if (response.Successful) {
                return response.Choices;
            }
            else
            {
                if (response.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{response.Error.Code}: {response.Error.Message}");
                return null;
            }
        }
    }
}
