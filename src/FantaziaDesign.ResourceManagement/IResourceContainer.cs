namespace FantaziaDesign.ResourceManagement
{
	public interface IResourceContainer<TKey, TResource>
	{
		TKey Key { get; }
		TResource Resource { get; }
		void SetResource(TResource resource);
	}
}
