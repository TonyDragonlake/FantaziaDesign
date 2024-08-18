using System;

namespace FantaziaDesign.Core
{
	public class RectInt : RectBase<int>, IValueContainer<Vec4i>, IEquatable<RectInt>, IDeepCopyable<RectInt>, IRect<int>
	{
		private Vec4i m_value;
		public Vec4i Value { get => m_value; set => m_value = value; }
		public override int Left   { get => m_value[0]; set => m_value[0] = value; }
		public override int Top    { get => m_value[1]; set => m_value[1] = value; }
		public override int Right  { get => m_value[2]; set => m_value[2] = value; }
		public override int Bottom { get => m_value[3]; set => m_value[3] = value; }
		public override bool IsEmpty => Rects.IsEmptyRect(m_value);

		public RectInt()
		{
			m_value = new Vec4i();
		}

		public RectInt(Vec4i integer4)
		{
			m_value = integer4.DeepCopy();
		}

		public RectInt(int left, int top, int right, int bottom)
		{
			m_value = new Vec4i(left, top, right, bottom);
		}

		public RectInt(RectInt rect)
		{
			m_value = rect is null ? new Vec4i() : rect.m_value.DeepCopy();
		}

		public override void SetWidth(int width, HorizontalLocation referenceLocation = HorizontalLocation.Left)
		{
			if (width < 0)
			{
				width = 0;
			}
			switch (referenceLocation)
			{
				case HorizontalLocation.Right:
					{
						Left = Right - width;
					}
					break;
				case HorizontalLocation.Center:
					{
						var dCenter = Left + Right;
						Left = (dCenter - width) / 2;
						Right = (dCenter + width) / 2;
					}
					break;
				default:
					{
						Right = Left + width;
					}
					break;
			}
		}

		public override void SetHeight(int height, VerticalLocation referenceLocation = VerticalLocation.Top)
		{
			if (height < 0)
			{
				height = 0;
			}
			switch (referenceLocation)
			{
				case VerticalLocation.Bottom:
					{
						Top = Bottom - height;
					}
					break;
				case VerticalLocation.Middle:
					{
						var dMiddle = Top + Bottom;
						Top = (dMiddle - height) / 2;
						Bottom = (dMiddle + height) / 2;
					}
					break;
				default:
					{
						Bottom = Top + height;
					}
					break;
			}
		}

		public override string ToString()
		{
			if (IsEmpty)
			{
				return $"{nameof(RectInt)} : Empty";
			}

			return $"{nameof(RectInt)} : {{L:{Left}, T:{Top}, R:{Right}, B:{Bottom}}} [W:{Right - Left}, H:{Bottom - Top}]";
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as RectInt);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public static bool operator ==(RectInt left, RectInt right)
		{
			if (left is null)
			{
				if (right is null)
				{
					return true;
				}
				return false;
			}
			return left.Equals(right);
		}

		public static bool operator !=(RectInt left, RectInt right)
		{
			return !(left == right);
		}

		public bool Contains(PointInt point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Contains(RectInt rect)
		{
			return !Rects.IsEmptyRect(m_value)
				&& !Rects.IsEmptyRect(rect.m_value)
				&& Left <= rect.Left
				&& Top <= rect.Top
				&& Right >= rect.Right
				&& Bottom >= rect.Bottom;
		}

		protected override bool ContainsInternal(int px, int py)
		{
			return px >= Left
				&& px <= Right
				&& py >= Top
				&& py <= Bottom;
		}

		public override void Offset(int offsetX, int offsetY)
		{
			Left += offsetX;
			Top += offsetY;
			Right += offsetX;
			Bottom += offsetY;
		}

		public bool Equals(RectInt other)
		{
			if (other is null)
			{
				return false;
			}
			return m_value.Equals(other.m_value);
		}

		public RectInt DeepCopy()
		{
			return new RectInt(this);
		}

		public void DeepCopyValueFrom(RectInt obj)
		{
			if (obj is null)
			{
				return;
			}

			m_value = obj.m_value.DeepCopy();
		}

		public object Clone()
		{
			return DeepCopy();
		}

		protected override int GetWidth() => Right - Left;

		protected override int GetHeight() => Bottom - Top;

		protected override void EnsureSizeValid(ref int width, ref int height)
		{
			if (width < 0)
			{
				width = 0;
			}
			if (height < 0)
			{
				height = 0;
			}
		}

	}

}
