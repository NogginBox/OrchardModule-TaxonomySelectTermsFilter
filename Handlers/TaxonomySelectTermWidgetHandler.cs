using NogginBox.TaxonomySelectTermFilter.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace NogginBox.TaxonomySelectTermFilter.Handlers
{
	public class TaxonomySelectTermWidgetHandler: ContentHandler
	{
        public TaxonomySelectTermWidgetHandler(IRepository<TaxonomySelectTermWidgetRecord> repository)
		{
            Filters.Add(StorageFilter.For(repository));
        }
	}
}