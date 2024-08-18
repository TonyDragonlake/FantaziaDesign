namespace FantaziaDesign.Core
{
	public interface IThickness<T>
	{
		T Left { get; set; }
		T Top { get; set; }
		T Right { get; set; }
		T Bottom { get; set; }
		bool IsZeroThickness { get; }
		T TotalThicknessInWidth { get; }
		T TotalThicknessInHeight { get; }

		void GetThicknessRaw(out T left, out T top, out T right, out T bottom);
		void SetThickness(T left, T top, T right, T bottom);
		void SetUniformThickness(T thickness);
		void ZeroThickness();
		void MaximizeThickness(T left, T top, T right, T bottom);
		void MinimizeThickness(T left, T top, T right, T bottom);
	}

}
