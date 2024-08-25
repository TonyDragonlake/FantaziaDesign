using FantaziaDesign.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace FantaziaDesign.Models.Mvvm
{
	public class NotifiableObjectCollection<TNotifiableObject> : ObservableCollection<TNotifiableObject>, INotifiableObject where TNotifiableObject : INotifiableObject
	{
		private long m_uId;
		private SynchronizationContext m_syncContext;

		public NotifiableObjectCollection()
		{
			m_uId = SnowflakeUId.Next();
			m_syncContext = SynchronizationContext.Current;
		}

		public NotifiableObjectCollection(IEnumerable<TNotifiableObject> collection) : base(collection)
		{
			m_uId = SnowflakeUId.Next();
			m_syncContext = SynchronizationContext.Current;
		}

		public NotifiableObjectCollection(List<TNotifiableObject> list) : base(list)
		{
			m_uId = SnowflakeUId.Next();
			m_syncContext = SynchronizationContext.Current;
		}

		public long UId => m_uId;
		public SynchronizationContext SynchronizationContext { get => m_syncContext; protected set => m_syncContext = value; }

		public void RaisePropertyChangedEvent(string propName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propName));
		}
	}
}
