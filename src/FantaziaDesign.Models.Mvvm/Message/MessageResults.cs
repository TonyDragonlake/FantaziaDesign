using CommunityToolkit.Mvvm.Input;
namespace FantaziaDesign.Models.Mvvm.Message
{
	public enum MessageResults : short
	{
		No = -2,
		Cancel = -1,
		NoResult,
		Accept,
		Yes = Accept
	}
}
