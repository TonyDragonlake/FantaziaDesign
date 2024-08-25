using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using FantaziaDesign.Core;

namespace FantaziaDesign.Models.Mvvm.Message
{
	public partial class SimpleDialog : MessageViewModel
	{
		#region Code Generated Properties
		[ObservableProperty]
		protected string m_dialogTitle;
		[ObservableProperty]
		protected object m_dialogContent;
		[ObservableProperty]
		protected string m_informationContent;
		[ObservableProperty]
		protected InformationSeverity m_informationSeverity;
		#endregion

		private readonly RelayCommand m_cancelCommand;

		public ICommand CancelCommand => m_cancelCommand;

		public string CancelKeyword { get; set; }

		public event AsyncEventHandler<bool> DialogClosing;

		public MessageResults DefinedMessageResult
		{
			get
			{
				var result = MessageResult;
				if (result is null)
				{
					return MessageResults.NoResult;
				}
				if (result is MessageResults mres)
				{
					return mres;
				}
				return MessageResults.NoResult;
			}
		}

		public SimpleDialog()
		{
			m_cancelCommand = new RelayCommand(ExecuteCancelCommand);
		}
		protected virtual void ExecuteCancelCommand()
		{
			MessageResult = MessageResults.Cancel;
			MessageStatus = MessageStatus.RequestClose;
		}

		protected override Task ExecuteClosing(CancellationToken cancellationToken)
		{
			if (DialogClosing is null)
			{
				return Task.FromResult(true);
			}
			return DialogClosing.Invoke(this, EventArgs.Empty)
				.ContinueWith<bool>(AfterExecuteClosing, cancellationToken);
		}

		protected virtual bool AfterExecuteClosing(Task<bool> closingTask)
		{
			var result = closingTask.Result;
			if (!result)
			{
				MessageStatus = MessageStatus.RequestCancelClose;
			}
			return result;
		}

		public override string ToString()
		{
			return $"{nameof(SimpleDialog)} {{{DialogTitle}, {MessageResult}}}";
		}

		protected override void OnResetting()
		{
			InformationContent = null;
			InformationSeverity = InformationSeverity.Informational;
		}

	}

}
