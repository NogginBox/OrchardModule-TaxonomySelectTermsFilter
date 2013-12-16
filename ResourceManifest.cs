using Orchard.UI.Resources;

namespace NogginBox.TaxonomySelectTermFilter {
	public class ResourceManifest : IResourceManifestProvider {
		public void BuildManifests(ResourceManifestBuilder builder) {
			var manifest = builder.Add();
			manifest.DefineScript("SimpleSelectTree").SetUrl("SimpleSelectTree.js").SetDependencies("jQuery"); ;
		}
	}
}