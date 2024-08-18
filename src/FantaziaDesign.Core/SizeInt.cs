using System;

namespace FantaziaDesign.Core
{
	public class SizeInt : SizeBase<int>, IValueContainer<Vec2i>, IEquatable<SizeInt>, IDeepCopyable<SizeInt>, ISize<int>
	{
		private Vec2i m_value;
		public Vec2i Value { get => m_value; set => m_value = value; }
		public override int Width { get => m_value[0]; set => m_value[0] = Math.Abs(value); }
		public override int Height { get => m_value[1]; set => m_value[1] = Math.Abs(value); }

		public SizeInt()
		{
			m_value = new Vec2i();
		}

		public SizeInt(int width, int height)
		{
			m_value = new Vec2i(width, height);
		}

		public SizeInt(Vec2i integer2)
		{
			m_value = integer2.DeepCopy();
		}

		public SizeInt(SizeInt sizeInt)
		{
			m_value = sizeInt is null ? new Vec2i() : sizeInt.m_value.DeepCopy();
		}

		public object Clone()
		{
			return DeepCopy();
		}

		public SizeInt DeepCopy()
		{
			return new SizeInt() { m_value = m_value.DeepCopy() };
		}

		public void DeepCopyValueFrom(SizeInt obj)
		{
			if (obj is null)
			{
				return;
			}

			m_value = obj.m_value.DeepCopy();
		}

		public bool Equals(SizeInt other)
		{
			if (other is null)
			{
				return false;
			}

			return m_value.Equals(other.m_value);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as SizeInt);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public override string ToString()
		{
			return $"{nameof(SizeInt)}:{{Width:{m_value[0]}, Height:{m_value[1]}}}";
		}

		public static bool operator ==(SizeInt left, SizeInt right)
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

		public static bool operator !=(SizeInt left, SizeInt right)
		{
			return !(left == right);
		}
	}
}
