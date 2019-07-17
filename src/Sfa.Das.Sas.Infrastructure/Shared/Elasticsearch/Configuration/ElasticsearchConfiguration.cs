namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration
{
    using System.Collections.Generic;
    using Nest;

    using Settings;
    using Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models;

    public class ElasticsearchConfiguration : IElasticsearchConfiguration
    {
        public const string AnalyserEnglishCustom = "english_custom";
        public const string AnalyserEnglishCustomText = "english_custom_text";
        public const string AutocompleteAnalyser = "autocomplete";
        public const string AutocompleteSearchAnalyser = "autocomplete_search";
        public const string AutocompleteTokeniser = "autocomplete";
        public const string AutocompleteNgramPropertyField = "auto";
        private readonly IElasticsearchSettings _elasticsearchSettings;

        public ElasticsearchConfiguration(IElasticsearchSettings elasticsearchSettings)
        {
            _elasticsearchSettings = elasticsearchSettings;
        }

        public AnalysisDescriptor ApprenticeshipAnalysisDescriptor()
        {
            return new AnalysisDescriptor()
                        .CharFilters(t => t.PatternReplace("char_pattern_replace_er", m => m.Pattern("or\\b").Replacement("er")))
                        .TokenFilters(t => t
                            .Stemmer("english_possessive_stemmer", m => m.Language("possessive_english"))
                            .Stop("english_stop", m => m.StopWords(_elasticsearchSettings.StopWords))
                            .Stop("english_stop_freetext", m => m.StopWords(_elasticsearchSettings.StopWordsExtended))
                            .Stemmer("english_stemmer", m => m.Language("english"))
                            .PatternReplace("pattern_replace_er", m => m.Pattern("or\b").Replacement("er"))
                            .Synonym("english_custom_synonyms", s => s.Synonyms(_elasticsearchSettings.Synonyms)))
                        .Analyzers(a => a
                            .Custom(AnalyserEnglishCustom, l => l
                                .Tokenizer("standard")
                                .Filters("english_possessive_stemmer", "lowercase", "english_stop", "english_custom_synonyms", "pattern_replace_er", "english_stemmer")
                                .CharFilters("char_pattern_replace_er"))
                            .Custom(AnalyserEnglishCustomText, l => l
                                .Tokenizer("standard")
                                .Filters("english_possessive_stemmer", "lowercase", "english_stop_freetext", "english_custom_synonyms"))
                            .Custom(AutocompleteAnalyser, cc => cc
                                .Tokenizer(AutocompleteTokeniser)
                                .Filters(new List<string> { "lowercase" }))
                            .Custom(AutocompleteSearchAnalyser, cc => cc
                                .Tokenizer("lowercase")))
                        .Tokenizers(tz => tz
                            .EdgeNGram(AutocompleteTokeniser, td => td
                                .MinGram(2)
                                .MaxGram(20)
                                .TokenChars(TokenChar.Letter)));
        }

        public int ApprenticeshipIndexShards() => !string.IsNullOrEmpty(_elasticsearchSettings.ApprenticeshipIndexShards) ? int.Parse(_elasticsearchSettings.ApprenticeshipIndexShards) : 1;

        public int ApprenticeshipIndexReplicas() => !string.IsNullOrEmpty(_elasticsearchSettings.ApprenticeshipIndexShards) ? int.Parse(_elasticsearchSettings.ApprenticeshipIndexReplicas) : 0;

        public int ProviderIndexShards() => !string.IsNullOrEmpty(_elasticsearchSettings.ProviderIndexShards) ? int.Parse(_elasticsearchSettings.ProviderIndexShards) : 1;

        public int ProviderIndexReplicas() => !string.IsNullOrEmpty(_elasticsearchSettings.ProviderIndexReplicas) ? int.Parse(_elasticsearchSettings.ProviderIndexReplicas) : 0;

        public int LarsIndexShards() => !string.IsNullOrEmpty(_elasticsearchSettings.LarsIndexShards) ? int.Parse(_elasticsearchSettings.LarsIndexShards) : 1;

        public int LarsIndexReplicas() => !string.IsNullOrEmpty(_elasticsearchSettings.LarsIndexReplicas) ? int.Parse(_elasticsearchSettings.LarsIndexReplicas) : 0;

        public MappingsDescriptor ApprenticeshipMappingDescriptor()
        {
            return new MappingsDescriptor()
                    .Map<StandardDocument>(m => m
                    .AutoMap()
                    .Properties(p => p
                        .Text(t => t
                            .Name("title")
                            .Analyzer(AnalyserEnglishCustom)
                            .Fields(f => f
                                .Text(t2 => t2
                                    .Name(AutocompleteNgramPropertyField)
                                    .Analyzer(AutocompleteAnalyser)
                                    .SearchAnalyzer(AutocompleteSearchAnalyser))))
                        .Text(t => t
                            .Name("jobRoles")
                            .Analyzer(AnalyserEnglishCustom)
                            .Fields(f => f
                                .Text(t2 => t2
                                    .Name(AutocompleteNgramPropertyField)
                                    .Analyzer(AutocompleteAnalyser)
                                    .SearchAnalyzer(AutocompleteSearchAnalyser))))
                        .Text(t => t
                            .Name("keywords")
                            .Analyzer(AnalyserEnglishCustom)
                            .Fields(f => f
                                .Text(t2 => t2
                                    .Name(AutocompleteNgramPropertyField)
                                    .Analyzer(AutocompleteAnalyser)
                                    .SearchAnalyzer(AutocompleteSearchAnalyser))))))
                    .Map<FrameworkDocument>(m => m
                        .AutoMap()
                        .Properties(p => p
                            .Object<JobRoleItem>(o => o
                            .Name(n => n.JobRoleItems)
                            .Properties(jrps => jrps
                                .Text(t => t
                                    .Name("title")
                                    .Analyzer(AnalyserEnglishCustom)
                                    .Fields(f => f
                                        .Text(t2 => t2
                                            .Name(AutocompleteNgramPropertyField)
                                            .Analyzer(AutocompleteAnalyser)
                                            .SearchAnalyzer(AutocompleteSearchAnalyser))))
                                .Text(t2 => t2
                                    .Name(n => n.Description)
                                    .Analyzer(AnalyserEnglishCustomText))))
                            .Text(t => t
                                .Name("keywords")
                                .Analyzer(AnalyserEnglishCustom)
                                .Fields(f => f
                                    .Text(t2 => t2
                                        .Name(AutocompleteNgramPropertyField)
                                        .Analyzer(AutocompleteAnalyser)
                                        .SearchAnalyzer(AutocompleteSearchAnalyser))))));
        }
    }
}
