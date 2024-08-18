using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec4i :
		IDeepCopyable<Vec4i>,
		IEquatable<Vec4i>,
		ICloneable,
		IPseudoArray<int>
	{

		[FieldOffset(0)]
		private int m_int1;
		[FieldOffset(4)]
		private int m_int2;
		[FieldOffset(8)]
		private int m_int3;
		[FieldOffset(12)]
		private int m_int4;


		#region IPseudoArray
		public int this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return m_int1;
					case 1: return m_int2;
					case 2: return m_int3;
					case 3: return m_int4;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,3]");
			}
			set
			{
				switch (index)
				{
					case 0: m_int1 = value; return;
					case 1: m_int2 = value; return;
					case 2: m_int3 = value; return;
					case 3: m_int4 = value; return;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,3]");
			}
		}
		int IPseudoArray<int>.Count => 4;
		#endregion

		#region Ctor
		public Vec4i(IPseudoArray<int> array) : this()
		{
			if (array is null)
			{
				return;
			}
			var count = array.Count;
			const int elementCount = 4;
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

		public Vec4i(params int[] array) : this()
		{
			if (array != null)
			{
				var length = array.Length;
				if (length > 4)
				{
					length = 4;
				}
				for (int i = 0; i < length; i++)
				{
					this[i] = array[i];
				}
			}
		}

		#endregion

		#region equality operator
		public static bool operator ==(Vec4i left, Vec4i right) => Equals(left, right);
		public static bool operator !=(Vec4i left, Vec4i right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec4i DeepCopy()
		{
			return new Vec4i(this);
		}

		public void DeepCopyValueFrom(Vec4i obj)
		{
			m_int1 = obj.m_int1;
			m_int2 = obj.m_int2;
			m_int3 = obj.m_int3;
			m_int4 = obj.m_int4;
		}
		#endregion

		#region IEquatable
		private static bool Equals(Vec4i left, Vec4i right)
		{
			return left.m_int1 == right.m_int1
				&& left.m_int2 == right.m_int2
				&& left.m_int3 == right.m_int3
				&& left.m_int4 == right.m_int4;
		}


		public override bool Equals(object obj)
		{
			if (obj is Vec4i vec4i)
			{
				return Equals(vec4i);
			}
			return false;
		}

		public bool Equals(Vec4i other)
		{
			return Equals(this, other);
		}

		public override int GetHashCode()
		{
			return m_int1.GetHashCode()
				^ m_int2.GetHashCode()
				^ m_int3.GetHashCode()
				^ m_int4.GetHashCode();
		}

		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Vec4i(this);
		}
		#endregion

		#region Enumerations
		private static IEnumerable<int> Int32Enumeration(Vec4i vec4i)
		{
			yield return vec4i.m_int1;
			yield return vec4i.m_int2;
			yield return vec4i.m_int3;
			yield return vec4i.m_int4;
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
		#endregion
	}

}
