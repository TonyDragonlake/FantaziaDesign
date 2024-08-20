using System.ComponentModel;

namespace FantaziaDesign.ResourceManagement
{
	public interface IResourceNotifier<TKey, TResource> : IResourceContainer<TKey, TResource>, INotifyPropertyChanged { }
}
