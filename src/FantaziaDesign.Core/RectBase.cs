namespace FantaziaDesign.Core
{
	public abstract class RectBase<T> : IRect<T>
	{
		public abstract T Left { get; set; }
		public abstract T Top { get; set; }
		public abstract T Right { get; set; }
		public abstract T Bottom { get; set; }
		public abstract bool IsEmpty { get; }
		protected abstract T GetWidth();
		protected abstract T GetHeight();
		public abstract void SetWidth(T width, HorizontalLocation referenceLocation = HorizontalLocation.Left);
		public abstract void SetHeight(T height, VerticalLocation referenceLocation = VerticalLocation.Top);
		protected abstract void EnsureSizeValid(ref T width, ref T height);
		public abstract void Offset(T offsetX, T offsetY);
		protected abstract bool ContainsInternal(T px, T py);

		public T X { get => Left; set => Left = value; }
		public T Y { get => Top; set => Top = value; }
		public T Width { get { return GetWidth(); } set { SetWidth(value); } }
		public T Height { get { return GetHeight(); } set { SetHeight(value); } }

		public virtual void GetLocationAndSizeRaw(out T px, out T py, out T width, out T height)
		{
			px = Left;
			py = Top;
			width = Width;
			height = Height;
		}

		public virtual void GetLocationRaw(out T px, out T py)
		{
			px = Left;
			py = Top;
		}

		public virtual void GetSizeRaw(out T width, out T height)
		{
			width = Width;
			height = Height;
		}

		public virtual void SetLocationAndSize(T px, T py, T width, T height)
		{
			Left = px;
			Top = py;
			EnsureSizeValid(ref width, ref height);
			SetWidth(width);
			SetHeight(height);
		}

		public virtual void SetLocation(T px, T py)
		{
			Left = px;
			Top = py;
		}

		public virtual void SetRect(T left, T top, T right, T bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public virtual void SetSize(T width, T height)
		{
			EnsureSizeValid(ref width, ref height);
			SetWidth(width);
			SetHeight(height);
		}

		public bool Contains(T px, T py)
		{
			return !IsEmpty
				&& ContainsInternal(px, py);
		}

		public virtual void ZeroSize()
		{
			SetWidth(default(T));
			SetHeight(default(T));
		}

		public virtual void ZeroRect()
		{
			Left = default(T);
			Top = default(T);
			Right = default(T);
			Bottom = default(T);
		}

	}

}
