using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using System.Text;

namespace LearningEnglishSkill
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // 初始化Kernel
            var builder = new KernelBuilder();
            builder.WithAzureChatCompletionService(
                "text-davinci-003",
                "{your azure openai endpoint}",
                "{your azure openai key}");
            var kernel = builder.Build();

            // 導入技能
            var skills = kernel.ImportSemanticSkillFromDirectory("Skills", "Learning");
            var myContext = new ContextVariables();
            var histories = new StringBuilder();

            Console.WriteLine("Say anything to start practicing English.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                var input = Console.ReadLine();
                // 填充變量
                myContext.Set("history", histories.ToString());
                myContext.Set("input", input);
                //# 運行技能
                var myResult = kernel.RunAsync(myContext, skills["LearningEnglishSkill"]).GetAwaiter().GetResult();
                histories.AppendLine(input);
                histories.AppendLine(myResult.Result.ToString());
                Console.WriteLine(myResult);
            }
        }
    }
}