﻿using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.ComponentModel.DataAnnotations;

namespace NogginBox.TaxonomySelectTermFilter.Models
{
	public class TaxonomySelectTermWidgetRecord : ContentPartRecord
	{
		public virtual int TaxonomyId { get; set; }
	}

	public class TaxonomySelectTermWidgetPart : ContentPart<TaxonomySelectTermWidgetRecord>
	{
		[Required]
		public int TaxonomyId
		{
			get { return Record.TaxonomyId; }
			set { Record.TaxonomyId = value; }
		}

		/// <summary>
		/// Required by editor to populate TaxonomyId, not persisted
		/// </summary>
		public SelectList TaxonomyOptions { get; set; }
	}
}