using Glossary.Models;
using Glossary.Models.PaginationModels;
using Glossary.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Glossary.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class GlossaryController : ControllerBase
    {

        private readonly IGlossaryService<GlossaryModel> _service;

        public GlossaryController(IGlossaryService<GlossaryModel> service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("Get Glossary")]
        public async Task<ActionResult<GlossaryModel>> Get(string term)
        {
            var model = await _service.Get(term);
            if (model == null)
                return NotFound();
            else
                return Ok(model);
        }
        [HttpPost]
        [Description("Search Glossary")]
        [Route("/Search")]
        public async Task<ResponseModel<GlossaryModel>> Search([FromBody] RequestModel<string> request)
        {
            var response = await _service.Search(request);
            return response;
        }
        [HttpPost]
        [Description("Get List by character")]
        [Route("/GetByCharacter")]
        public async Task<ResponseModel<GlossaryModel>> GetByCharacter([FromBody] RequestModel<Char> request)
        {
            var response = await _service.GetByCharacter(request);
            return response;
        }


        [HttpPost]
        [ValidateModel]
        [Description("Create Glossary")]
        public async Task<ActionResult> Create([FromBody] GlossaryModel model)
        {
            var savedModel = await _service.Create(model);
            return Ok(savedModel);
        }

        [HttpPut]
        [ValidateModel]
        [SwaggerResponse(typeof(GlossaryModel))]
        [Description("Update Glossary")]
        public async Task<ActionResult<GlossaryModel>> Update([FromBody] GlossaryModel model)
        {
            var savedModel = await _service.Update(model);
            return Ok(savedModel);
        }

        [HttpGet]
        [ValidateModel]
        [Route("{terms}")]
        [Description("Remove Glossary")]
        public ActionResult Remove(string terms)
        {
            _service.Delete(terms);

            return Ok();
        }

    }
}