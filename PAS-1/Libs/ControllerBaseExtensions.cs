using Microsoft.AspNetCore.Mvc;
using PAS_1.Libs;

public static class ControllerBaseExtensions
{
    public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, ServiceResult<T> result)
    {
        if (result.Success)
            return controller.Ok(result.Data);

        return controller.StatusCode(result.Error!.StatusCode, result.Error.Message);
    }
}