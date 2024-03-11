using External.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API.Filters;

public sealed class TransactionalContextFilter(FastFoodContext dbContext) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Method != HttpMethod.Get.Method)
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContext.BeginTransactionAsync() ??
                                              throw new Exception("Error initializing a transaction");

                var actionExecutedContext = await next();

                if (IsValidResponse(actionExecutedContext))
                    await dbContext.CommitTransactionAsync(transaction);
                else
                    dbContext.RollbackTransaction();

                await dbContext.DisposeAsync();
            });
        }

        await next();
    }

    private static bool IsValidResponse(ActionExecutedContext actionExecutedContext)
    {
        return actionExecutedContext.Exception == null
               && IsSuccessStatusCode(actionExecutedContext.Result!);
    }

    private static bool IsSuccessStatusCode(IActionResult result)
    {
        if (result is IStatusCodeActionResult statusCodeResult)
            return statusCodeResult.StatusCode is >= 200 and <= 299;

        return false;
    }
}