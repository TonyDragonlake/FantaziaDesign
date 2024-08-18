using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec2f :
		IDeepCopyable<Vec2f>,
		IEquatable<Vec2f>,
		ICloneable,
		IPseudoArray<float>,
		IPseudoArray<long>
	{
		[FieldOffset(0)]
		private long m_val;

		[FieldOffset(0)]
		private float m_float1;
		[FieldOffset(4)]
		private float m_float2;

		#region IPseudoArray
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return m_float1;
					case 1: return m_float2;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,1]");
			}
			set
			{
				switch (index)
				{
					case 0: m_float1 = value; return;
					case 1: m_float2 = value; return;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,1]");
			}
		}
		long IPseudoArray<long>.this[int index] { get => m_val; set => m_val = value; }
		int IPseudoArray<float>.Count => 2;
		int IPseudoArray<long>.Count => 1;
		#endregion

		#region Ctor
		public Vec2f(IPseudoArray<float> array) : this()
		{
			if (array is null)
			{
				return;
			}
			;
			if (array is Vec2f vec2f)
			{
				m_val = vec2f.m_val;
			}
			else
			{
				var count = array.Count;
				const int elementCount = 2;
				if (count > elementCount)
				{
					count = elementCount;
				}
				for (int i = 0; i < count; i++)
				{
					try
					{
						this[i] = array[i];
					}
					finally
					{
					}
				}
			}
		}

		public Vec2f(long val) : this()
		{
			m_val = val;
		}

		public Vec2f(params float[] array) : this()
		{
			if (array != null)
			{
				var length = array.Length;
				if (length > 2)
				{
					length = 2;
				}
				for (int i = 0; i < length; i++)
				{
					this[i] = array[i];
				}
			}
		}

		#endregion

		#region implicit operator
		public static implicit operator Vec2f(long val) => new Vec2f(val);
		public static implicit operator long(Vec2f val) => val.m_val;
		#endregion

		#region equality operator
		public static bool operator ==(Vec2f left, Vec2f right) => Equals(left, right);
		public static bool operator !=(Vec2f left, Vec2f right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec2f DeepCopy()
		{
			return new Vec2f(m_val);
		}

		public void DeepCopyValueFrom(Vec2f obj)
		{
			m_val = obj.m_val;
		}
		#endregion

		#region IEquatable
		private static bool Equals(Vec2f left, Vec2f right)
		{
			return left.m_val == right.m_val;
		}

		public override bool Equals(object obj)
		{
			if(obj is Vec2f vec2f)
			{
				return Equals(vec2f);
			}
			return false;
		}

		public bool Equals(Vec2f other)
		{
			return Equals(this, other);
		}

		public override int GetHashCode()
		{
			return m_val.GetHashCode();
		}

		public bool Equals(long other)
		{
			return m_val == other;
		}
		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Vec2f(m_val);
		}
		#endregion

		#region Enumerations
		private static IEnumerable<float> Float32Enumeration(Vec2f vec2f)
		{
			yield return vec2f.m_float1;
			yield return vec2f.m_float2;
			yield break;
		}

		private static IEnumerable<long> Int64Enumeration(Vec2f vec2f)
		{
			yield return vec2f.m_val;
			yield break;
		}
		#endregion

		#region IEnumerable

		public IEnumerator<float> GetEnumerator()
		{
			return Float32Enumeration(this).GetEnumerator();
		}

		IEnumerator<long> IEnumerable<long>.GetEnumerator()
		{
			return Int64Enumeration(this).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Float32Enumeration(this).GetEnumerator();
		}
		#endregion

	}

}
