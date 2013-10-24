using NogginBox.TaxonomySelectTermFilter.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace NogginBox.TaxonomySelectTermFilter
{
	public class Migrations : DataMigrationImpl
	{
		public int Create()
		{
			// Table
			SchemaBuilder.CreateTable("TaxonomySelectTermWidgetRecord",
				table => table
					.ContentPartRecord()
					.Column<int>("TaxonomyId")
			);

			// Settings part
			ContentDefinitionManager.AlterPartDefinition(
				typeof(TaxonomySelectTermWidgetPart).Name,
				cfg =>
					cfg.Attachable()
			);

			// Create Widget
			ContentDefinitionManager.AlterTypeDefinition("TaxonomySelectTermWidget",
				cfg => cfg
					.WithPart("TaxonomySelectTermWidgetPart")
					.WithPart("WidgetPart")
					.WithPart("CommonPart")
					.WithSetting("Stereotype", "Widget")
			);

			return 1;
		}
	}
}