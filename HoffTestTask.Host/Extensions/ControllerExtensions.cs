using Microsoft.AspNetCore.Mvc;

namespace HoffTestTask.Host.Extensions;

public static class ControllerExtensions
{
    public static IActionResult Response(this ControllerBase controllerBase,
        HoffTestTask.Infrastructure.Results.IResult result)
        => controllerBase.StatusCode(result.StatusCode, result);
}