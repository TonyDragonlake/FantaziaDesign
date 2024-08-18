using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec8f :
		IDeepCopyable<Vec8f>,
		IEquatable<Vec8f>,
		ICloneable,
		IPseudoArray<float>
	{

		[FieldOffset(0)]
		private float m_float1;
		[FieldOffset(4)]
		private float m_float2;
		[FieldOffset(8)]
		private float m_float3;
		[FieldOffset(12)]
		private float m_float4;
		[FieldOffset(16)]
		private float m_float5;
		[FieldOffset(20)]
		private float m_float6;
		[FieldOffset(24)]
		private float m_float7;
		[FieldOffset(28)]
		private float m_float8;

		#region IPseudoArray
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return m_float1;
					case 1: return m_float2;
					case 2: return m_float3;
					case 3: return m_float4;
					case 4: return m_float5;
					case 5: return m_float6;
					case 6: return m_float7;
					case 7: return m_float8;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,7]");
			}
			set
			{
				switch (index)
				{
					case 0: m_float1 = value; return;
					case 1: m_float2 = value; return;
					case 2: m_float3 = value; return;
					case 3: m_float4 = value; return;
					case 4: m_float5 = value; return;
					case 5: m_float6 = value; return;
					case 6: m_float7 = value; return;
					case 7: m_float8 = value; return;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,7]");
			}
		}
		int IPseudoArray<float>.Count => 8;
		#endregion

		#region Ctor

		public Vec8f(IPseudoArray<float> array) : this()
		{
			if (array is null)
			{
				return;
			}
			var count = array.Count;
			const int elementCount = 8;
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

		public Vec8f(params float[] array) : this()
		{
			if (array != null)
			{
				var length = array.Length;
				if (length > 8)
				{
					length = 8;
				}
				for (int i = 0; i < length; i++)
				{
					this[i] = array[i];
				}
			}
		}

		#endregion

		#region equality operator
		public static bool operator ==(Vec8f left, Vec8f right) => Equals(left, right);
		public static bool operator !=(Vec8f left, Vec8f right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec8f DeepCopy()
		{
			return new Vec8f(this);
		}

		public void DeepCopyValueFrom(Vec8f obj)
		{
			m_float1 = obj.m_float1;
			m_float2 = obj.m_float2;
			m_float3 = obj.m_float3;
			m_float4 = obj.m_float4;

			m_float5 = obj.m_float5;
			m_float6 = obj.m_float6;
			m_float7 = obj.m_float7;
			m_float8 = obj.m_float8;
		}
		#endregion

		#region IEquatable
		private static bool Equals(Vec8f left, Vec8f right)
		{
			return left.m_float1 == right.m_float1
				&& left.m_float2 == right.m_float2
				&& left.m_float3 == right.m_float3
				&& left.m_float4 == right.m_float4
				&& left.m_float5 == right.m_float5
				&& left.m_float6 == right.m_float6
				&& left.m_float7 == right.m_float7
				&& left.m_float8 == right.m_float8;
		}


		public override bool Equals(object obj)
		{
			if (obj is Vec8f vec8f)
			{
				return Equals(vec8f);
			}
			return false;
		}

		public bool Equals(Vec8f other)
		{
			return Equals(this, other);
		}

		public override int GetHashCode()
		{
			return m_float1.GetHashCode()
				^ m_float2.GetHashCode()
				^ m_float3.GetHashCode()
				^ m_float4.GetHashCode()
				^ m_float5.GetHashCode()
				^ m_float6.GetHashCode()
				^ m_float7.GetHashCode()
				^ m_float8.GetHashCode();
		}

		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Vec8f(this);
		}
		#endregion

		#region Enumerations
		private static IEnumerable<float> Float32Enumeration(Vec8f vec8f)
		{
			yield return vec8f.m_float1;
			yield return vec8f.m_float2;
			yield return vec8f.m_float3;
			yield return vec8f.m_float4;
			yield return vec8f.m_float5;
			yield return vec8f.m_float6;
			yield return vec8f.m_float7;
			yield return vec8f.m_float8;
			yield break;
		}
		#endregion

		#region IEnumerable
		public IEnumerator<float> GetEnumerator()
		{
			return Float32Enumeration(this).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Float32Enumeration(this).GetEnumerator();
		}
		#endregion
	}

}
