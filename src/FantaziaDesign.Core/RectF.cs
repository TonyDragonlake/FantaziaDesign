using System;

namespace FantaziaDesign.Core
{
	public class RectF : RectBase<float>, IValueContainer<Vec4f>, IEquatable<RectF>, IDeepCopyable<RectF>, IRect<float>
	{
		private Vec4f m_value;
		public Vec4f Value { get => m_value; set => m_value = value; }
		public override float Left { get => m_value[0]; set => m_value[0] = value; }
		public override float Top { get => m_value[1]; set => m_value[1] = value; }
		public override float Right { get => m_value[2]; set => m_value[2] = value; }
		public override float Bottom { get => m_value[3]; set => m_value[3] = value; }

		public override bool IsEmpty
		{
			get
			{
				return IsEmptyRect(this);
			}
		}

		public RectF()
		{
			m_value = new Vec4f();
		}

		public RectF(Vec4f float4)
		{
			m_value = float4.DeepCopy();
		}

		public RectF(int left, int top, int right, int bottom)
		{
			m_value = new Vec4f(left, top, right, bottom);
		}

		public RectF(float left, float top, float right, float bottom)
		{
			m_value = new Vec4f(left, top, right, bottom);
		}

		public RectF(RectF rectFSrc)
		{
			m_value = rectFSrc is null ? new Vec4f() : rectFSrc.m_value.DeepCopy();
		}

		public override void SetWidth(float width, HorizontalLocation referenceLocation = HorizontalLocation.Left)
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

		public override void SetHeight(float height, VerticalLocation referenceLocation = VerticalLocation.Top)
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
				return $"{nameof(RectF)} : Empty";
			}

			return $"{nameof(RectF)} : {{L:{Left}, T:{Top}, R:{Right}, B:{Bottom}}} [W:{Right - Left}, H:{Bottom - Top}]";
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as RectF);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public static bool operator ==(RectF left, RectF right)
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

		public static bool operator !=(RectF left, RectF right)
		{
			return !(left == right);
		}

		public bool Contains(PointF point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Contains(RectF rect)
		{
			return !IsEmptyRect(this)
				&& !IsEmptyRect(rect)
				&& Left <= rect.Left
				&& Top <= rect.Top
				&& Right >= rect.Right
				&& Bottom >= rect.Bottom;
		}

		public static bool IsEmptyRect(RectF rect)
		{
			if (rect is null)
			{
				return true;
			}
			return rect.Left >= rect.Right || rect.Top >= rect.Bottom;
		}

		public static bool IsEmptyRect(ref float left, ref float top, ref float right, ref float bottom)
		{
			return left >= right || top >= bottom;
		}

		protected override bool ContainsInternal(float px, float py)
		{
			return px >= Left
				&& px <= Right
				&& py >= Top
				&& py <= Bottom;
		}

		public bool Equals(RectF other)
		{
			if (other is null)
			{
				return false;
			}
			return m_value.Equals(other.m_value);
		}

		public RectF DeepCopy()
		{
			return new RectF(this);
		}

		public void DeepCopyValueFrom(RectF obj)
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

		protected override float GetWidth() => Right - Left;

		protected override float GetHeight() => Bottom - Top;

		protected override void EnsureSizeValid(ref float width, ref float height)
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

		public override void Offset(float offsetX, float offsetY)
		{
			Left += offsetX;
			Top += offsetY;
			Right += offsetX;
			Bottom += offsetY;
		}
	}

}
