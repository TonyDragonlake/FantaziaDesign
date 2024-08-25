namespace FantaziaDesign.Core
{
	public interface IReadOnlyKeyStorage<TKeyDescription, TKey>
	{
		TKey GetKey(TKeyDescription keyDescription);
	}
}

