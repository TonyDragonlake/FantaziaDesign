using System.Collections.Generic;

namespace FantaziaDesign.Core
{
	public interface IPropertyNamesRecorder
	{
		void ClearRecorder();
		IEnumerable<string> OfChanged();
		bool TrySetRecorder(int index, bool changed);
		bool TrySetRecorder(string propName, bool changed);
	}
}