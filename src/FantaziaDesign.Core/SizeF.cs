using System;

namespace FantaziaDesign.Core
{
	public class SizeF : SizeBase<float>, IValueContainer<Vec2f>, IEquatable<SizeF>, IDeepCopyable<SizeF>, ISize<float>
	{
		private Vec2f m_value;
		public  Vec2f Value { get => m_value; set => m_value = value; }
		public override float Width { get => m_value[0]; set => m_value[0] = Math.Abs(value); }
		public override float Height { get => m_value[1]; set => m_value[1] = Math.Abs(value); }

		public SizeF()
		{
			m_value = new Vec2f();
		}

		public SizeF(int width, int height)
		{
			m_value = new Vec2f(width, height);
		}

		public SizeF(Vec2f float2)
		{
			m_value = float2.DeepCopy();
		}

		public SizeF(SizeF sizeF)
		{
			m_value = sizeF is null ? new Vec2f() : sizeF.m_value.DeepCopy();
		}

		public object Clone()
		{
			return DeepCopy();
		}

		public SizeF DeepCopy()
		{
			return new SizeF() { m_value = m_value.DeepCopy() };
		}

		public void DeepCopyValueFrom(SizeF obj)
		{
			if (obj is null)
			{
				return;
			}

			m_value = obj.m_value.DeepCopy();
		}

		public bool Equals(SizeF other)
		{
			if (other is null)
			{
				return false;
			}

			return m_value.Equals(other.m_value);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as SizeF);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public override string ToString()
		{
			return $"{nameof(SizeF)}:{{Width:{m_value[0]}, Height:{m_value[1]}}}";
		}

		public static bool operator ==(SizeF left, SizeF right)
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

		public static bool operator !=(SizeF left, SizeF right)
		{
			return !(left == right);
		}

	}
}
