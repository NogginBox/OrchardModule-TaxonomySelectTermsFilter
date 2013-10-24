using Orchard.ContentManagement;
using Orchard.Events;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Projections.Descriptors.Filter;
using Orchard.Taxonomies.Models;
using Orchard.Taxonomies.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NogginBox.TaxonomySelectTermFilter.Filters
{
    public interface IFilterProvider : IEventHandler
	{
        void Describe(DescribeFilterContext describe);
    }

    public class SelectTermsFilter : IFilterProvider
	{
	    private readonly IHttpContextAccessor _httpContextAccessor;
	    private readonly ITaxonomyService _taxonomyService;

        public SelectTermsFilter(IHttpContextAccessor httpContextAccessor, ITaxonomyService taxonomyService)
		{
	        _httpContextAccessor = httpContextAccessor;
	        _taxonomyService = taxonomyService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeFilterContext describe)
		{
            describe.For("Taxonomy", T("Taxonomy"), T("Taxonomy"))
                .Element("SelectedTerms", T("User selected Terms"), T("User can select terms"),
                    ApplyFilter,
                    context => T("User selected taxonomy terms")
                );
        }

        public void ApplyFilter(FilterContext context)
		{
			var queryStringParams = _httpContextAccessor.Current().Request.QueryString;
			var termIds = queryStringParams["Terms"];

            if (!String.IsNullOrEmpty(termIds)) {
                var ids = termIds.Split(new[] { ',' }).Select(Int32.Parse).ToArray();

                if (ids.Length == 0) {
                    return;
                }

                var terms = ids.Select(_taxonomyService.GetTerm).ToList();
                var allChildren = new List<TermPart>();
                foreach(var term in terms) {
                    allChildren.AddRange(_taxonomyService.GetChildren(term));
                    allChildren.Add(term);
                }

                allChildren = allChildren.Distinct().ToList();

                var allIds = allChildren.Select(x => x.Id).ToList();

                // is one of
                Action<IAliasFactory> selector = alias => alias.ContentPartRecord<TermsPartRecord>().Property("Terms", "terms").Property("TermRecord", "termRecord");
                Action<IHqlExpressionFactory> filter = x => x.InG("Id", allIds);
                context.Query.Where(selector, filter);
            }
        }
    }
}