using HR_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Data.Helpers
{
    public class ViewEmployeeRequirement : IAuthorizationRequirement
    {

    }

    public class ViewEmployeeHandler : AuthorizationHandler<ViewEmployeeRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewEmployeeRequirement requirement, int employeeId)
        {
            // Check if the authenticated user is an employee and their ID matches the employee ID being accessed,
            // and if they have any of the required roles.
            if (context.User.IsInRole("Employee") && context.User.HasClaim(c => c.Type == "employeeId" && c.Value == employeeId.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}