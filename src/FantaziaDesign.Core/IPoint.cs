namespace FantaziaDesign.Core
{
	public interface IPoint<T>
	{
		T X { get; set; }
		T Y { get; set; }
		void GetPointRaw(out T px, out T py);
		void SetPoint(T px, T py);
		void Offset(T offsetX, T offsetY);
		void ZeroPoint();
	}

}
