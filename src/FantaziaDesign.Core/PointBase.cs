namespace FantaziaDesign.Core
{
	public abstract class PointBase<T> : IPoint<T>
	{
		public abstract T X { get; set; }
		public abstract T Y { get; set; }

		public virtual void GetPointRaw(out T px, out T py)
		{
			px = X;
			py = Y;
		}

		public abstract void Offset(T offsetX, T offsetY);

		public virtual void SetPoint(T px, T py)
		{
			X = px;
			Y = py;
		}

		public virtual void ZeroPoint()
		{
			X = default(T);
			Y = default(T);
		}
	}

}
