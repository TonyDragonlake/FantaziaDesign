namespace FantaziaDesign.Models.Mvvm.Message
{
	public enum MessageStatus : byte
	{
		Unknown,
		RequestOpen,
		RequestCancelOpen,
		Opened,
		RequestClose,
		RequestCancelClose,
		Closing,
		Closed
	}
}
