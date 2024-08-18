using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec2i :
		IDeepCopyable<Vec2i>,
		IEquatable<Vec2i>,
		ICloneable,
		IPseudoArray<int>,
		IPseudoArray<long>
	{
		[FieldOffset(0)]
		private long m_val;

		[FieldOffset(0)]
		private int m_int1;
		[FieldOffset(4)]
		private int m_int2;

		#region IPseudoArray
		public int this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return m_int1;
					case 1: return m_int2;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,1]");
			}
			set
			{
				switch (index)
				{
					case 0: m_int1 = value; return;
					case 1: m_int2 = value; return;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,1]");
			}
		}
		long IPseudoArray<long>.this[int index] { get => m_val; set => m_val = value; }
		int IPseudoArray<int>.Count => 2;
		int IPseudoArray<long>.Count => 1;
		#endregion

		#region Ctor
		public Vec2i(IPseudoArray<int> array) : this()
		{
			if (array is null)
			{
				return;
			}
			if (array is Vec2i vec2i)
			{
				m_val = vec2i.m_val;
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

		public Vec2i(long val) : this()
		{
			m_val = val;
		}

		public Vec2i(params int[] array) : this()
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
		public static implicit operator Vec2i(long val) => new Vec2i(val);
		public static implicit operator long(Vec2i val) => val.m_val;
		#endregion

		#region equality operator
		public static bool operator ==(Vec2i left, Vec2i right) => Equals(left, right);
		public static bool operator !=(Vec2i left, Vec2i right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec2i DeepCopy()
		{
			return new Vec2i(m_val);
		}

		public void DeepCopyValueFrom(Vec2i obj)
		{
			m_val = obj.m_val;
		}
		#endregion

		#region IEquatable
		private static bool Equals(Vec2i left, Vec2i right)
		{
			return left.m_val == right.m_val;
		}

		public override bool Equals(object obj)
		{
			if (obj is Vec2i vec2i)
			{
				return Equals(vec2i);
			}
			return false;
		}

		public bool Equals(Vec2i other)
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
			return new Vec2i(m_val);
		}
		#endregion

		#region Enumerations
		private static IEnumerable<int> Int32Enumeration(Vec2i vec2i)
		{
			yield return vec2i.m_int1;
			yield return vec2i.m_int2;
			yield break;
		}

		private static IEnumerable<long> Int64Enumeration(Vec2i vec2i)
		{
			yield return vec2i.m_val;
			yield break;
		}

		#endregion

		#region IEnumerable
		public IEnumerator<int> GetEnumerator()
		{
			return Int32Enumeration(this).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Int32Enumeration(this).GetEnumerator();
		}

		IEnumerator<long> IEnumerable<long>.GetEnumerator()
		{
			return Int64Enumeration(this).GetEnumerator();
		}
		#endregion
	}

}
