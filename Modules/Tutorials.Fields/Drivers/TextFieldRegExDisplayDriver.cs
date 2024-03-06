using Microsoft.Extensions.Localization;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using Tutorials.Fields.Settings;

namespace Tutorials.Fields.Drivers
{
    public class TextFieldRegExDisplayDriver : ContentFieldDisplayDriver<TextField>
    {
        private readonly IStringLocalizer S;

        public TextFieldRegExDisplayDriver(
            IStringLocalizer<TextFieldRegExDisplayDriver> localizer
        )
        {
            S = localizer;
        }

        // We only override the UpdateAsync method as we are just trying to add some validation when the field is saved
        public override async Task<IDisplayResult> UpdateAsync(TextField field, IUpdateModel updater, UpdateFieldEditorContext context)
        {
            // Only use this updater if the editor type is RegEx
            if (context.PartFieldDefinition.Editor() != "RegEx")
            {
                return Edit(field, context);
            }

            // Update the "Text" value of the "field" from the Request data
            if (await updater.TryUpdateModelAsync(field, Prefix, f => f.Text))
            {
                if (string.IsNullOrWhiteSpace(field.Text))
                {
                    return Edit(field, context);
                }

                // Get our RegEx settings for this field
                var expressionSettings = context.PartFieldDefinition.GetSettings<TextFieldRegExEditorSettings>();

                if (!string.IsNullOrWhiteSpace(expressionSettings?.Expression))
                {
                    var regex = new Regex(expressionSettings.Expression);

                    // If the text value does not match the regex pattern. Having a model state error will cancel the database session so nothing is written
                    // and the error message will show to the user
                    if (!regex.IsMatch(field.Text))
                    {
                        updater.ModelState.AddModelError(Prefix, nameof(field.Text), S["The text does not match the required pattern for {0}.", context.PartFieldDefinition.DisplayName()]);
                    }
                }
            }

            return Edit(field, context);
        }
    }
}
