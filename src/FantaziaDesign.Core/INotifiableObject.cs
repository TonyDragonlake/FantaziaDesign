using FantaziaDesign.Core;
using System.Threading;

namespace FantaziaDesign.Models
{
	public interface INotifiableObject : IPropertyNotifier, IRuntimeUnique
	{
		SynchronizationContext SynchronizationContext { get; }
	}


}
