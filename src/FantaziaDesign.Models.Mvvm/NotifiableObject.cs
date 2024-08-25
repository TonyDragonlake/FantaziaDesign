using CommunityToolkit.Mvvm.ComponentModel;
using FantaziaDesign.Core;
using System.Threading;
using System.Windows.Input;

namespace FantaziaDesign.Models.Mvvm
{
	public interface IAddItemCommandHost
	{
		ICommand AddItemCommand { get; }
	}

	public interface IRemoveItemCommandHost
	{
		ICommand RemoveItemCommand { get; }
	}

	public interface IClearItemsCommandHost
	{
		ICommand ClearItemsCommand { get; }
	}

	public interface IAddSelfCommandHost
	{
		ICommand AddSelfCommand { get; }
	}

	public interface IRemoveSelfCommandHost
	{
		ICommand RemoveSelfCommand { get; }
	}

	public class NotifiableObject : ObservableObject, INotifiableObject
	{
		private long m_uId;
		private SynchronizationContext m_syncContext;

		public long UId => m_uId;
		public SynchronizationContext SynchronizationContext { get => m_syncContext; protected set => m_syncContext = value; }

		public NotifiableObject()
		{
			m_uId = SnowflakeUId.Next();
			m_syncContext = SynchronizationContext.Current;
		}

		public void RaisePropertyChangedEvent(string propName)
		{
			OnPropertyChanged(propName);
		}
	}
}
