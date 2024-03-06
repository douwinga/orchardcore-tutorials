using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Implementation;
using OrchardCore.Modules;
using Tutorials.Fields.Drivers;
using Tutorials.Fields.Handlers;
using Tutorials.Fields.Settings;

namespace Tutorials.Fields
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, TextFieldRegExEditorSettingsDriver>();

            services.AddContentField<TextField>()
                .UseDisplayDriver<TextFieldRegExDisplayDriver>();

            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, ContentFieldVisabilitySettingsDriver>();

            services.AddScoped<IShapeDisplayEvents, ContentFieldVisabilityShapeDisplayEvents>();
        }
    }
}
