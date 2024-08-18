using System;

namespace FantaziaDesign.Core
{
	public class PointF : PointBase<float>, IValueContainer<Vec2f>, IEquatable<PointF>, IDeepCopyable<PointF>, IPoint<float>
	{
		private Vec2f m_value;

		public Vec2f Value { get => m_value; set => m_value = value; }

		public override float X { get => m_value[0]; set => m_value[0] = value; }
		public override float Y { get => m_value[1]; set => m_value[1] = value; }

		public PointF()
		{
			m_value = new Vec2f();
		}

		public PointF(float x, float y)
		{
			m_value = new Vec2f(x, y);
		}

		public PointF(Vec2f float2)
		{
			m_value = float2.DeepCopy();
		}

		public PointF(PointF pointF)
		{
			m_value = pointF is null ? new Vec2f() : pointF.m_value.DeepCopy();
		}

		public static PointF Zero
		{
			get => new PointF();
		}

		public override string ToString()
		{
			return $"{nameof(PointF)}:{{X:{m_value[0]}, Y:{m_value[1]}}}";
		}

		public bool Equals(PointF other)
		{
			return m_value.Equals(other.m_value);
		}

		public PointF DeepCopy()
		{
			return new PointF(this);
		}

		public void DeepCopyValueFrom(PointF obj)
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
			return Equals(obj as PointF);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public static bool operator ==(PointF left, PointF right)
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

		public static bool operator !=(PointF left, PointF right)
		{
			return !(left == right);
		}

		public override void Offset(float offsetX, float offsetY)
		{
			X += offsetX;
			Y += offsetY;
		}

	}

}
