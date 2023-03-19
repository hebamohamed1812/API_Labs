using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace lab1.Filters;

public class ValidateCarTypeAttribute : ActionFilterAttribute
{
    private readonly ILogger<ValidateCarTypeAttribute> _logger;
    public ValidateCarTypeAttribute(ILogger<ValidateCarTypeAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogCritical("This is a custom action filter");
        var allowedTypeRegex = new Regex("[Electric|Gas|Diesel|Hybrid]",
            RegexOptions.IgnoreCase,
            TimeSpan.FromSeconds(2));

        Car? car = context.ActionArguments["car"] as Car;

        if (car is null || !allowedTypeRegex.IsMatch(car.Type))
        {
            context.Result = new BadRequestObjectResult(new GeneralResponse("The Type is not covered"));
        }
    }
}
