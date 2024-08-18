namespace FantaziaDesign.Core
{
	public abstract class ThicknessBase<T> : IThickness<T>
	{
		public abstract T Left { get; set; }
		public abstract T Top { get; set; }
		public abstract T Right { get; set; }
		public abstract T Bottom { get; set; }

		public abstract bool IsZeroThickness { get; }

		public abstract T TotalThicknessInWidth { get; }

		public abstract T TotalThicknessInHeight { get; }

		public virtual void GetThicknessRaw(out T left, out T top, out T right, out T bottom)
		{
			left = Left; top = Top; right = Right; bottom = Bottom;
		}

		public abstract void MaximizeThickness(T left, T top, T right, T bottom);

		public abstract void MinimizeThickness(T left, T top, T right, T bottom);

		public virtual void SetThickness(T left, T top, T right, T bottom)
		{
			Left = left; Top = top;
			Right = right; Bottom = bottom;
		}

		public virtual void SetUniformThickness(T thickness)
		{
			Left = thickness; Top = thickness; Right = thickness; Bottom = thickness;
		}

		public virtual void ZeroThickness()
		{
			Left = default(T); Top = default(T); Right = default(T); Bottom = default(T);
		}
	}

}
