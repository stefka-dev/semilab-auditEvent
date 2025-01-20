using AuditEvent.Storage.Interfaces;
using AuditEvent.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditEvent.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IRepository<AuditEventMessage> _repository;

    public MessageController(IRepository<AuditEventMessage> repository)
    {
        _repository = repository;
    }

    [HttpGet("[action]")]
    public IActionResult IntegrityCheck()
    {
        var retVal = _repository.IntegrityCheck();
        return Ok(retVal);
    }
    [HttpGet("[action]")]
    public IActionResult GetAll()
    {
        var retVal = _repository.GetAll();
        return Ok(retVal);
    }
}