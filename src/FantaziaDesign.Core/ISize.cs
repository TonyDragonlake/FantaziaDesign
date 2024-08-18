namespace FantaziaDesign.Core
{
	public interface ISize<T>
	{
		T Width { get; set; }
		T Height { get; set; }
		void GetSizeRaw(out T width, out T height);
		void SetSize(T width, T height);
		void ZeroSize();
	}
}
