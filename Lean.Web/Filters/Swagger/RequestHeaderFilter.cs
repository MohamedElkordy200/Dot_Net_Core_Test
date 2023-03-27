using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lean.Web.Filters.Swagger
{
    //    / <summary>
    /// Operation filter to add the requirement of the custom header
    /// </summary>
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "Accept Language For localization",
                Required = true,
                Example = new OpenApiString("en")
            });

        }
    }

}
