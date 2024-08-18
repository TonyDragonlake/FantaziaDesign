using System;

namespace FantaziaDesign.Core
{
	public class EllipseF : IValueContainer<Vec4f>, IEquatable<EllipseF>, IDeepCopyable<EllipseF>
	{
		private Vec4f m_value;
		public Vec4f Value { get => m_value; set => m_value = value; }
		public EllipseF()
		{
			m_value = new Vec4f();
		}

		public EllipseF(float centerX, float centerY, float radiusX, float radiusY)
		{
			m_value = new Vec4f(centerX, centerY, radiusX, radiusY);
		}

		public EllipseF(Vec4f float4)
		{
			m_value = float4.DeepCopy();
		}

		public EllipseF(EllipseF ellipseF)
		{
			if (ellipseF is null)
			{
				m_value = new Vec4f();
			}
			else
			{
				m_value = ellipseF.m_value.DeepCopy();
			}
		}

		public float CenterX { get => m_value[0]; set => m_value[0] = value; }
		public float CenterY { get => m_value[1]; set => m_value[1] = value; }
		public float RadiusX { get => m_value[2]; set => m_value[2] = value; }
		public float RadiusY { get => m_value[3]; set => m_value[3] = value; }

		public object Clone()
		{
			return DeepCopy();
		}

		public EllipseF DeepCopy()
		{
			return new EllipseF(this);
		}

		public void DeepCopyValueFrom(EllipseF obj)
		{
			if (obj is null)
			{
				return;
			}

			m_value = obj.m_value.DeepCopy();
		}

		public bool Equals(EllipseF other)
		{
			if (other is null)
			{
				return false;
			}

			return m_value.Equals(other.m_value);
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}

		public override string ToString()
		{
			return $"EllipseF {{Center ({CenterX},{CenterY}); RadiusX ({RadiusX}); RadiusY ({RadiusY})}}";
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as EllipseF);
		}

		public static bool operator ==(EllipseF left, EllipseF right)
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

		public static bool operator !=(EllipseF left, EllipseF right)
		{
			return !(left == right);
		}
	}

}
