using System;

namespace FantaziaDesign.ResourceManagement
{
	public class ResourceChangedEventArgs<TKey, TResource> : EventArgs, IResourceContainer<TKey, TResource>
	{
		private class __ResourceContainer : IResourceContainer<TKey, TResource>
		{
			private readonly TKey m_key;
			private TResource m_resource;
			public __ResourceContainer(TKey key) => m_key = key;
			public TKey Key => m_key;
			public TResource Resource => m_resource;
			public void SetResource(TResource resource) => m_resource = resource;
		}

		protected IResourceContainer<TKey, TResource> m_innerArgs;

		public TKey Key => m_innerArgs.Key;

		public TResource Resource => m_innerArgs.Resource;

		public ResourceChangedEventArgs(TKey key) => m_innerArgs = new __ResourceContainer(key);

		public ResourceChangedEventArgs(TKey key, TResource resource) : this(key) => m_innerArgs.SetResource(resource);

		public ResourceChangedEventArgs(IResourceContainer<TKey, TResource> resourceContainer)
		{
			ArgumentNullException.ThrowIfNull(resourceContainer, nameof(resourceContainer));
			m_innerArgs = resourceContainer;
		}

		public virtual void SetResource(TResource resource) => m_innerArgs.SetResource(resource);
	}
}
