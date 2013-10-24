using NogginBox.TaxonomySelectTermFilter.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Taxonomies.Services;

namespace NogginBox.TaxonomySelectTermFilter.Drivers
{
	public class TaxonomySelectTermWidgetDriver: ContentPartDriver<TaxonomySelectTermWidgetPart>
	{
		private readonly ITaxonomyService _taxonomyService;

		public TaxonomySelectTermWidgetDriver(ITaxonomyService taxonomyService)
		{
			_taxonomyService = taxonomyService;
		}

		protected override DriverResult Display(TaxonomySelectTermWidgetPart part, string displayType, dynamic shapeHelper)
		{
			// Todo: Get the taxonomy details
			var taxonomy = _taxonomyService.GetTaxonomy(part.TaxonomyId);
			var terms = taxonomy.Terms;

            return ContentShape("Parts_TaxonomySelectTermWidget", () => shapeHelper.Parts_TaxonomySelectTermWidget(
                Terms: terms));
        }

        //GET
        protected override DriverResult Editor(TaxonomySelectTermWidgetPart part, dynamic shapeHelper)
		{
            return ContentShape("Parts_TaxonomySelectTermWidget_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/TaxonomySelectTermWidget",
                    Model: part,
                    Prefix: Prefix));
        }

        //POST
        protected override DriverResult Editor(TaxonomySelectTermWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
		{
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
	}
}