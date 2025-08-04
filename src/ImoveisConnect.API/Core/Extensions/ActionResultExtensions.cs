using ImoveisConnect.API.Controllers.Core;
using ImoveisConnect.API.Core.Response;
using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Application.Core.Result.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ImoveisConnect.API.Core.Extensions
{
    public static class ActionResultExtensions
    {
        public static ActionResult FromResult<T>(this BaseApiController controller, Result<T> result)
        {
            return result.Type switch
            {
                ResultType.Success => controller.Ok(new Response<T>(result.Data, result.Success, ResultType.Success)),
                ResultType.NotFound => controller.NotFound(new Response<T>(result.Errors, ResultType.NotFound)),
                ResultType.Invalid => controller.BadRequest(new Response<T>(result.Errors, ResultType.Invalid)),
                _ => throw new Exception("Um resultado não tratado ocorreu em uma chamada de serviço."),
            };
        }

    }
}
