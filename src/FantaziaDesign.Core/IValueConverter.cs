//using System.Windows.Data;
namespace FantaziaDesign.Core
{
	public interface IValueConverter<TSource, TTarget> 
	{
		TTarget ConvertTo(TSource source);
		TSource ConvertBack(TTarget target);
	}

}

