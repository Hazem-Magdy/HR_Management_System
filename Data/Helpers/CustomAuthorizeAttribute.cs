using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

public class AdminOnlyAttribute : TypeFilterAttribute
{
    public AdminOnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Admin" } };
    }
}

public class HROnlyAttribute : TypeFilterAttribute
{
    public HROnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "HR" } };
    }
}

public class AccountantOnlyAttribute : TypeFilterAttribute
{
    public AccountantOnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Accountant" } };
    }
}

public class EmployeeOnlyAttribute : TypeFilterAttribute
{
    public EmployeeOnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Employee" } };
    }
}

public class AccountantHROnlyAttribute : TypeFilterAttribute
{
    public AccountantHROnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Accountant", "HR" } };
    }
}

public class AdminAccountantOnlyAttribute : TypeFilterAttribute
{
    public AdminAccountantOnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Admin", "Accountant" } };
    }
}

public class AdminHROnlyAttribute : TypeFilterAttribute
{
    public AdminHROnlyAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Admin", "HR" } };
    }
}

public class AdminAccountantHRAttribute : TypeFilterAttribute
{
    public AdminAccountantHRAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Admin", "Accountant", "HR" } };
    }
}

public class AdminAccountantEmployeeAttribute : TypeFilterAttribute
{
    public AdminAccountantEmployeeAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Admin", "Accountant", "Employee" } };
    }
}

public class AdminAccountantHREmployeeAttribute : TypeFilterAttribute
{
    public AdminAccountantHREmployeeAttribute() : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { new string[] { "Admin", "Accountant", "HR", "Employee" } };
    }
}

public class CustomAuthorizeFilter : IAuthorizationFilter
{
    private readonly string[] _roles;

    public CustomAuthorizeFilter(string[] roles)
    {
        _roles = roles;
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new ChallengeResult();
            return;
        }

        if (!HasPermission(context.HttpContext.User))
        {
            context.Result = new ObjectResult(new { error = "You do not have permission to access this resource." })
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };
            return;
        }
    }

    private bool HasPermission(ClaimsPrincipal user)
    {
        // Check if the authenticated user has permission to access the resource
        // based on the user's role(s).

        foreach (var role in _roles)
        {
            if (user.IsInRole(role))
            {
                return true;
            }
        }

        return false;
    }

    
}
#region old
/*
public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new ChallengeResult();
            return;
        }

        // Check if the user has permission based on the specified roles or the ViewEmployeePolicy policy.
        var serviceProvider = context.HttpContext.RequestServices;
        if (!HasPermission(context.HttpContext.User, serviceProvider).GetAwaiter().GetResult())
        {
            context.Result = new ObjectResult(new { error = "You do not have permission to access this resource." })
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };
            return;
        }
    }

    private async Task<bool> HasPermission(ClaimsPrincipal user, IServiceProvider serviceProvider)
    {
        // Check if the authenticated user has permission to access the resource
        // based on the user's role(s) or the ViewEmployeePolicy policy.

        foreach (var role in _roles)
        {
            if (user.IsInRole(role))
            {
                return true;
            }
        }

        // Check if the user meets the requirements of the ViewEmployeePolicy policy.
        //var policyEvaluator = serviceProvider.GetRequiredService<IAuthorizationService>();
        //var authorizationResult = await policyEvaluator.AuthorizeAsync(user, null, "ViewEmployeePolicy");
        //if (authorizationResult.Succeeded)
        //{
        //    return true;
        //}

        return false;
    }
*/
#endregion