using System.ComponentModel;

namespace FantaziaDesign.Models
{
	public interface IPropertyNotifier : INotifyPropertyChanged
	{
		void RaisePropertyChangedEvent(string propName);
	}
}
