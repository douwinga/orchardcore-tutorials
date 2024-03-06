using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using Tutorials.Fields.ViewModels;

namespace Tutorials.Fields.Settings
{
    public class ContentFieldVisabilitySettingsDriver : ContentPartFieldDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition)
        {
            return Initialize<ContentFieldVisabilitySettingsViewModel>("ContentFieldVisabilitySettings_Edit", model =>
            {
                var settings = partFieldDefinition.GetSettings<ContentFieldVisabilitySettings>();

                model.IsHidden = settings.IsHidden;
            }).Location("Editor");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
        {
            var model = new ContentFieldVisabilitySettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix);

            context.Builder.MergeSettings<ContentFieldVisabilitySettings>(settings =>
            {
                settings.IsHidden = model.IsHidden;
            });

            return Edit(partFieldDefinition);
        }
    }
}
