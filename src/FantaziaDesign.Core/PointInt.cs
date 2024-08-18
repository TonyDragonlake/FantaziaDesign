using System;

namespace FantaziaDesign.Core
{
	public class PointInt : PointBase<int>, IValueContainer<Vec2i>, IEquatable<PointInt>, IDeepCopyable<PointInt>, IPoint<int>
	{
		private Vec2i m_value;
		public Vec2i Value { get => m_value; set => m_value = value; }
		public override int X { get => m_value[0]; set => m_value[0] = value; }
		public override int Y { get => m_value[1]; set => m_value[1] = value; }

		public PointInt(int x, int y)
		{
			m_value = new Vec2i(x, y);
		}

		public PointInt()
		{
			m_value = new Vec2i();
		}

		public PointInt(Vec2i integer2)
		{
			m_value = integer2.DeepCopy();
		}

		public PointInt(PointInt pointInt)
		{
			m_value = pointInt is null ? new Vec2i() : pointInt.m_value.DeepCopy();
		}

		public static PointInt Zero => new PointInt();

		public override string ToString()
		{
			return $"{nameof(PointInt)}:{{X:{m_value[0]}, Y:{m_value[1]}}}";
		}

		public bool Equals(PointInt other)
		{
			return m_value.Equals(other.m_value);
		}

		public PointInt DeepCopy()
		{
			return new PointInt(this);
		}

		public void DeepCopyValueFrom(PointInt obj)
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

		public override bool Equals(object obj)
		{
			return Equals(obj as PointInt);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public static bool operator ==(PointInt left, PointInt right)
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

		public static bool operator !=(PointInt left, PointInt right)
		{
			return !(left == right);
		}

		public override void Offset(int offsetX, int offsetY)
		{
			X += offsetX;
			Y += offsetY;
		}

	}

}
