using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FantaziaDesign.Models.Mvvm.Message
{
	public partial class MessageViewModel : NotifiableObjectViewModel, IMessageObject
	{
		#region Code Generated Properties
		[ObservableProperty]
		private object m_messageResult;
		[ObservableProperty]
		private bool m_closeOnTimeout;
		[ObservableProperty]
		private bool m_closeOnClickAway;
		[ObservableProperty]
		private TimeSpan m_messageTimeout = TimeSpan.Zero;
		[ObservableProperty]
		private MessageStatus m_messageStatus;
		[ObservableProperty]
		private double m_maskOpacity = 0.5;
		#endregion

		protected AsyncRelayCommand m_closingCommand;
		protected TaskCompletionSource<bool> m_completionSource;

		public MessageViewModel()
		{
			m_closingCommand = new AsyncRelayCommand(ExecuteClosing, CanExecuteClosing);
		}

		public ICommand ClosingCommand => m_closingCommand;

		internal TaskCompletionSource<bool> CompletionSource
		{
			get
			{
				if (m_completionSource is null)
				{
					m_completionSource = new TaskCompletionSource<bool>();
				}
				return m_completionSource;
			}
		}

		public Task<bool> MessageTask => CompletionSource.Task;

		protected virtual bool CanExecuteClosing() => true;

		protected virtual Task ExecuteClosing(CancellationToken cancellationToken) => Task.CompletedTask;

		public virtual void CriticalInvalidateMessageTask()
		{
			if (m_completionSource is null)
			{
				return;
			}
			if (!MessageTask.IsCompleted)
			{
				m_completionSource.TrySetCanceled();
			}
			m_completionSource = null;
		}

		public virtual void CriticalSetMessageTaskCompletion(bool result = true)
		{
			if (m_completionSource is null)
			{
				return;
			}
			if (!MessageTask.IsCompleted)
			{
				m_completionSource.TrySetResult(result);
			}
		}

		protected virtual void OnResetting() { }

		public void Reset()
		{
			MessageResult = null;
			MessageStatus = MessageStatus.RequestOpen;
			OnResetting();
			CriticalInvalidateMessageTask();
		}
	}
}
