using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Exceptions.Model;
using Application.Features.Commands.CreateTemplate;
using Application.Features.Commands.DeleteTemplate;
using Application.Features.Commands.UpdateTemplate;
using Application.Features.Queries.GetTemplateById;
using Application.Features.Queries.GetTemplateList;
using Application.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TemplateController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemplateController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name = "GetTemplate")]
    [ProducesResponseType(typeof(IEnumerable<EntityDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<EntityDto>>> GetAll([FromQuery] GetTemplateParameters parameters)
    {
        var entities = await _mediator.Send(parameters);
        Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(entities.MetaData));
        return Ok(new Response<PagedList<EntityDto>>(entities));
    }

    [HttpGet("{id}", Name = "GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetById(long id)
    {
        var entity = await _mediator.Send(id);

        return Ok(entity);
    }

    [HttpPost(Name = "CreateTemplate")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<long>> CreateTemplate([FromBody] CreateTemplateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new Response<long>(result, $"Successfully created with Id: {result}"));
    }

    [HttpPut(Name = "Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update([FromBody] UpdateTemplateCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteTemplateCommand() { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}