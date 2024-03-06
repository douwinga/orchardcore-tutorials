using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.Implementation;
using Tutorials.Fields.Settings;

namespace Tutorials.Fields.Handlers
{
	public class ContentFieldVisabilityShapeDisplayEvents : IShapeDisplayEvents
	{
		public Task DisplayingAsync(ShapeDisplayContext context)
		{
			// only handle edit views
			if (context.Shape.Metadata.DisplayType != "Edit")
			{
				return Task.CompletedTask;
			}

			var partFieldDefinition = context.Shape.GetType()?.GetProperty("PartFieldDefinition")?.GetValue(context.Shape) as ContentPartFieldDefinition;

			if (partFieldDefinition == null)
			{
				return Task.CompletedTask;
			}

			var settings = partFieldDefinition.GetSettings<ContentFieldVisabilitySettings>();

			if (settings == null)
			{
				return Task.CompletedTask;
			}

			if (settings.IsHidden)
			{
				// if hidden, add a wrapper that just wraps the field in a hidden div
				context.Shape.Metadata.Wrappers.Add("HiddenField_Wrapper");
			}

			return Task.CompletedTask;
		}

		public Task DisplayedAsync(ShapeDisplayContext context)
		{
			return Task.CompletedTask;
		}

		public Task DisplayingFinalizedAsync(ShapeDisplayContext context)
		{
			return Task.CompletedTask;
		}
	}
}
