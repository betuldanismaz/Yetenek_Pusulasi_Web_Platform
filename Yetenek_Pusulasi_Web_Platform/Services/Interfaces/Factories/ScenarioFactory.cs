using System;
using Yetenek_Pusulasi_Web_Platform.Models.Entities;
using Yetenek_Pusulasi_Web_Platform.Services.Interfaces;

namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces.Factories
{
    public class ScenarioFactory : IScenarioFactory
    {
        // Bu fabrika, farklı içerik üreteçlerini DI ile alabilir veya burada new'leyebilir.
        // DI ile almak daha esnek olur.
        private readonly IServiceProvider _serviceProvider;

        public ScenarioFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Scenario CreateScenario(ScenarioType type, string title, dynamic? contentParameters = null)
        {
            IScenarioContentGenerator? contentGenerator = null;

            switch (type)
            {
                case ScenarioType.ProblemSolving:
                    contentGenerator = (IScenarioContentGenerator)_serviceProvider.GetService(typeof(ProblemSolvingContentGenerator))!;
                    break;
                case ScenarioType.Empathy:
                    contentGenerator = (IScenarioContentGenerator)_serviceProvider.GetService(typeof(EmpathyContentGenerator))!;
                    break;
                case ScenarioType.Generic:
                default:
                    return new Scenario
                    {
                        Title = title,
                        Content = "Lütfen bu genel senaryoya uygun bir cevap verin.",
                        Description = "Genel amaçlı bir senaryo.",
                        Type = type,
                        CreatedAt = DateTime.UtcNow
                    };
            }

            if (contentGenerator == null)
            {
                throw new InvalidOperationException($"Content generator for type {type} could not be resolved.");
            }

            return new Scenario
            {
                Title = title,
                Content = contentGenerator.GenerateContent(contentParameters),
                Description = contentGenerator.GetDefaultDescription(),
                Type = type,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}