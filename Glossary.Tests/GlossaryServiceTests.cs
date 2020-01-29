using FluentAssertions;
using Glossary.Models;
using Glossary.Models.PaginationModels;
using Glossary.Repository.Glossary;
using Glossary.Services;
using System.Linq;
using Xunit;

namespace Glossary.Tests
{
    public class GlossaryServiceTests
    {
        private IGlossaryService<GlossaryModel> _service;

        public GlossaryServiceTests()
        {
            var config = Setup.GivenConfiguration();
            var context = new GlossaryStoreContext(config["FilePath"]);
            var repo = new GlossaryRepository(context);
            _service = new GlossaryService(repo);
        }
        [Fact]
        public void GetGlossaryByTerm()
        {
            var response = _service.Get("debt");
            response.Result.Records.Count.Should().Be(1);
            response.Result.Records.First().Term.Should().Be("debt");
            response.Result.Records.First().Text.Should().NotBeEmpty();
        }
        [Fact]
        public void SearchGlossaryByKeyword_to_return_results_from_terms_or_description_texts()
        {
            var response = _service.Search(new RequestModel<string>() { FilterModel = "debt", PageNumber = 1, PageSize = 50 });
            response.Result.Records.Count.Should().Be(2);
            response.Result.Records.Select(x => x.Term.Contains("debt") || x.Text.Contains("debt")).Count().Should()
                .Be(2);

        }

        [Fact]
        public void Create_Glossary_Then_Search_for_it()
        {
            var response = _service.Create(new GlossaryModel()
            {
                Term = "expulsion",
                Text = @"Permanent removal from an organization or place. For example, a school
                principal might order the expulsion of a student from school. "
            });
            response.Result.Term.Should().Be("expulsion");

            var response2 = _service.Get("expulsion");
            response2.Result.Records.Count.Should().Be(1);
            response2.Result.Records.First().Term.Should().Be("expulsion");
            response2.Result.Records.First().Text.Should().NotBeEmpty();
        }

        [Fact]
        public void Create_Glossary_that_exists()
        {
            var response = _service.Create(new GlossaryModel()
            {
                Term = "debt",
                Text = @"Money owed."
            });

            response.Exception.InnerException?.Message.Should()
                .Be("Glossary already exists");
        }
        [Fact]
        public async void DeleteGlossaryThenSearchForIt()
        {

            var response = _service.Get("defamation");
            response.Result.Records.Count.Should().Be(1);
            response.Result.Records.First().Term.Should().Be("defamation");
            response.Result.Records.First().Text.Should().NotBeEmpty();
            _service.Delete("defamation");
            try
            {
                var response2 = await _service.Get("defamation");
                1.Should().Be(0);

            }
            catch (GlossaryException e)
            {
                e.Message.Should().Be("Glossary does not exist");
            }
        }

    }
}
