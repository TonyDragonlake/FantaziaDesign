namespace FantaziaDesign.Core
{
	public abstract class SizeBase<T> : ISize<T>
	{
		public abstract T Width { get; set; }
		public abstract T Height { get; set; }

		public virtual void GetSizeRaw(out T width, out T height)
		{
			width = Width;
			height = Height;
		}

		public virtual void SetSize(T width, T height)
		{
			Width = width;
			Height = height;
		}

		public virtual void ZeroSize()
		{
			Width = default(T);
			Height = default(T);
		}
	}
}
