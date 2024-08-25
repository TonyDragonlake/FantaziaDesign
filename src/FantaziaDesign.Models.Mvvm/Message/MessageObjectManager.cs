using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace FantaziaDesign.Models.Mvvm.Message
{
	public interface IMessageObjectManager : 
		INotifiableObject, 
		IList<IMessageObject>, 
		IReadOnlyList<IMessageObject>, 
		IList, 
		INotifyCollectionChanged, 
		INotifyPropertyChanged, 
		IRemoveItemCommandHost
	{

	}

	public class MessageObjectManager : NotifiableObjectCollectionViewModel<IMessageObject>, IMessageObjectManager
	{
		private readonly RelayCommand<object> m_removeItemCommand;

		public MessageObjectManager() : base()
		{
			m_removeItemCommand = new RelayCommand<object>(ExecuteRemoveItemCommand);
		}

		public ICommand RemoveItemCommand => m_removeItemCommand;

		private void ExecuteRemoveItemCommand(object parameter)
		{
			if (parameter is IMessageObject messageObject)
			{
				messageObject.CriticalSetMessageTaskCompletion();
				Remove(messageObject);
			}
		}
	}

}
