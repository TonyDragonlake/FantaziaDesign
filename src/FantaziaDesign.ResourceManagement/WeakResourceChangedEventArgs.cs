using System;
using FantaziaDesign.Events;

namespace FantaziaDesign.ResourceManagement
{
	public class WeakResourceChangedEventArgs<TKey, TResource> : ResourceChangedEventArgs<TKey, TResource>, IEventArguments<ResourceChangedEventHandler<TKey, TResource>>
	{
		protected readonly WeakReference m_sender;
		protected bool m_handled;

		public WeakResourceChangedEventArgs(object sender, TKey key) : base(key) 
		{
			m_sender = new WeakReference(sender);
		}

		public WeakResourceChangedEventArgs(object sender, TKey key, TResource resource) : this(sender, key) => m_innerArgs.SetResource(resource);
		
		public WeakResourceChangedEventArgs(object sender, IResourceContainer<TKey, TResource> resourceContainer) : base(resourceContainer) 
		{
			m_sender = new WeakReference(sender);
		}

		public object Sender => m_sender.IsAlive ? m_sender.Target : null;
		public bool Handled { get => m_handled; set => m_handled = value; }

		public void InvokeEventHandler(ResourceChangedEventHandler<TKey, TResource> handler)
		{
			handler?.Invoke(Sender, m_innerArgs);
		}
	}

}
