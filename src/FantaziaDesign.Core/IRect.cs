namespace FantaziaDesign.Core
{
	public interface IRect<T> : ISize<T>
	{
		T X { get; set; }
		T Y { get; set; }
		T Left { get; set; }
		T Top { get; set; }
		T Right { get; set; }
		T Bottom { get; set; }
		void SetWidth(T width, HorizontalLocation referenceLocation = HorizontalLocation.Left);
		void SetHeight(T height, VerticalLocation referenceLocation = VerticalLocation.Top);
		void Offset(T offsetX, T offsetY);
		void GetLocationAndSizeRaw(out T px, out T py, out T width, out T height);
		void GetLocationRaw(out T px, out T py);
		void SetLocationAndSize(T px, T py, T width, T height);
		void SetLocation(T px, T py);
		void SetRect(T left, T top, T right, T bottom);
		bool Contains(T px, T py);
		void ZeroRect();
	}

}
