using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;

namespace Xivotec.CleanArchitecture.Presentation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : Controller
{
    private readonly IPersistenceConfigurationService _configurationService;

    public SettingsController(IPersistenceConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet]
    public ActionResult<List<string>> Get()
    {
        var configDto = _configurationService.GetPersistenceConfigurationDto();
        List<string> returnList = [
            configDto.Host ?? "",
            configDto.Port ?? "",
            configDto.PersistenceName ?? ""
        ];
        return Ok(returnList);
    }
}