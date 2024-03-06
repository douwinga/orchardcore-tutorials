using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using Tutorials.Fields.ViewModels;

namespace Tutorials.Fields.Settings
{
    public class TextFieldRegExEditorSettingsDriver : ContentPartFieldDefinitionDisplayDriver<TextField>
    {
        public TextFieldRegExEditorSettingsDriver() { }

        // Builds the settings editor view shape
        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition)
        {
            return Initialize<TextFieldRegExEditorSettingsViewModel>("TextFieldRegExEditorSettings_Edit", model =>
            {
                var settings = partFieldDefinition.GetSettings<TextFieldRegExEditorSettings>();
                model.Expression = settings.Expression;
            })
            .Location("Editor");
        }

        // Handles saving the editor settings
        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
        {
            // Only save if the RegEx editor is selected
            if (partFieldDefinition.Editor() == "RegEx")
            {
                var model = new TextFieldRegExEditorSettingsViewModel();
                var settings = new TextFieldRegExEditorSettings();

                // Update the model values with what is sent in the Request data
                await context.Updater.TryUpdateModelAsync(model, Prefix);

                settings.Expression = model.Expression;

                // update the settings
                context.Builder.WithSettings(settings);
            }

            return Edit(partFieldDefinition);
        }
    }
}
