using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FantaziaDesign.Models.Mvvm.Message
{
	public interface IMessageObject : INotifiableObject
	{
		MessageStatus MessageStatus { get; set; }
		object MessageResult { get; set; }
		bool CloseOnTimeout { get; set; }
		bool CloseOnClickAway { get; set; }
		TimeSpan MessageTimeout { get; set; }
		ICommand ClosingCommand { get; }
		Task<bool> MessageTask { get; }
		double MaskOpacity { get; set; }
		void CriticalInvalidateMessageTask();
		void CriticalSetMessageTaskCompletion(bool result = true);
		void Reset();
	}

}
