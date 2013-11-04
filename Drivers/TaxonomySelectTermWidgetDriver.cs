using System;
using System.Web.Mvc;
using NogginBox.TaxonomySelectTermFilter.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Taxonomies.Services;

namespace NogginBox.TaxonomySelectTermFilter.Drivers
{
	public class TaxonomySelectTermWidgetDriver: ContentPartDriver<TaxonomySelectTermWidgetPart>
	{
		private readonly IOrchardServices _services;
		private readonly ITaxonomyService _taxonomyService;

		public TaxonomySelectTermWidgetDriver(IOrchardServices services, ITaxonomyService taxonomyService)
		{
			_services = services;
			_taxonomyService = taxonomyService;
		}

		protected override DriverResult Display(TaxonomySelectTermWidgetPart part, string displayType, dynamic shapeHelper)
		{
			// Todo: Get the taxonomy details
			var taxonomy = _taxonomyService.GetTaxonomy(part.TaxonomyId);
			var terms = taxonomy.Terms;

			int lastQueryTermId;
			Int32.TryParse(_services.WorkContext.HttpContext.Request.QueryString["Terms"], out lastQueryTermId);

			return ContentShape("Parts_TaxonomySelectTermWidget", () => shapeHelper.Parts_TaxonomySelectTermWidget(
				Terms: terms, LastTermId: lastQueryTermId));
		}

		//GET
		protected override DriverResult Editor(TaxonomySelectTermWidgetPart part, dynamic shapeHelper)
		{
			var taxonomies = _taxonomyService.GetTaxonomies();
			part.TaxonomyOptions = new SelectList(taxonomies, "Id", "Name");

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