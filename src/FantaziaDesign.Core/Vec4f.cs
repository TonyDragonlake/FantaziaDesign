using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec4f :
		IDeepCopyable<Vec4f>,
		IEquatable<Vec4f>,
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
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,3]");
			}
			set
			{
				switch (index)
				{
					case 0: m_float1 = value; return;
					case 1: m_float2 = value; return;
					case 2: m_float3 = value; return;
					case 3: m_float4 = value; return;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,3]");
			}
		}
		int IPseudoArray<float>.Count => 4;
		#endregion

		#region Ctor
		public Vec4f(IPseudoArray<float> array) : this()
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

		public Vec4f(params float[] array) : this()
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
		public static bool operator ==(Vec4f left, Vec4f right) => Equals(left, right);
		public static bool operator !=(Vec4f left, Vec4f right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec4f DeepCopy()
		{
			return new Vec4f(this);
		}

		public void DeepCopyValueFrom(Vec4f obj)
		{
			m_float1 = obj.m_float1;
			m_float2 = obj.m_float2;
			m_float3 = obj.m_float3;
			m_float4 = obj.m_float4;
		}
		#endregion

		#region IEquatable
		private static bool Equals(Vec4f left, Vec4f right)
		{
			return left.m_float1 == right.m_float1
				&& left.m_float2 == right.m_float2
				&& left.m_float3 == right.m_float3
				&& left.m_float4 == right.m_float4;
		}


		public override bool Equals(object obj)
		{
			if (obj is Vec4f vec4f)
			{
				return Equals(vec4f);
			}
			return false;
		}

		public bool Equals(Vec4f other)
		{
			return Equals(this, other);
		}

		public override int GetHashCode()
		{
			return m_float1.GetHashCode()
				^ m_float2.GetHashCode()
				^ m_float3.GetHashCode()
				^ m_float4.GetHashCode();
		}

		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Vec4f(this);
		}
		#endregion

		#region Enumerations
		private static IEnumerable<float> Float32Enumeration(Vec4f vec4f)
		{
			yield return vec4f.m_float1;
			yield return vec4f.m_float2;
			yield return vec4f.m_float3;
			yield return vec4f.m_float4;
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
