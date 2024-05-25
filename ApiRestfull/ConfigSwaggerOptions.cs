using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiRestfull
{
    public class ConfigSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;



        public ConfigSwaggerOptions(IApiVersionDescriptionProvider provider)
            
        {
            _provider = provider;
            
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
                    var info = new OpenApiInfo
                    {

                        Title = "Api de una Universidad",
                        Version = description.ApiVersion.ToString(),
                        Description = "Esta es la primera version de la API, que maneja, los estduiantes, usuarios administradores y cursos ",
                        Contact = new OpenApiContact()
                        {
                            Email = "oscarvallecillo95@gmail.com",
                            Name = "Oscar Josue Vallecillo Aguilar"
                        }

                    };

                    if (description.IsDeprecated)
                    {
                        info.Description += "Esta version de la API está deprecada";

                    }

                    return info;

                    


         }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            foreach (var description in this._provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description) );
                
            }
        }

        public void Configure(SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
