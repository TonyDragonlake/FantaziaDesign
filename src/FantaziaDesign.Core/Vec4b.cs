using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec4b :
		IDeepCopyable<Vec4b>,
		IEquatable<Vec4b>,
		ICloneable,
		IPseudoArray<byte>,
		IPseudoArray<int>,
		IPseudoArray<uint>
	{
		[FieldOffset(0)]
		private int m_val;

		[FieldOffset(0)]
		private uint m_uval;

		[FieldOffset(0)]
		private byte m_byte1;
		[FieldOffset(1)]
		private byte m_byte2;
		[FieldOffset(2)]
		private byte m_byte3;
		[FieldOffset(3)]
		private byte m_byte4;

		#region IPseudoArray
		public byte this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return m_byte1;
					case 1: return m_byte2;
					case 2: return m_byte3;
					case 3: return m_byte4;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,3]");
			}
			set
			{
				switch (index)
				{
					case 0: m_byte1 = value; return;
					case 1: m_byte2 = value; return;
					case 2: m_byte3 = value; return;
					case 3: m_byte4 = value; return;
					default:
						break;
				}
				throw new IndexOutOfRangeException("index should be in range of [0,3]");
			}
		}
		int IPseudoArray<int>.this[int index] { get => m_val; set => m_val = value; }
		uint IPseudoArray<uint>.this[int index] { get => m_uval; set => m_uval = value; }
		int IPseudoArray<byte>.Count => 4;
		int IPseudoArray<int>.Count => 1;
		int IPseudoArray<uint>.Count => 1;
		#endregion

		#region Ctor

		public Vec4b(IPseudoArray<byte> array): this()
		{
			if (array is null)
			{
				return;
			}

			if (array is Vec4b vec4b)
			{
				m_val = vec4b.m_val;
			}
			else
			{
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
		}

		public Vec4b(int val) : this()
		{
			m_val = val;
		}

		public Vec4b(uint uval) : this()
		{
			m_uval = uval;
		}

		public Vec4b(params byte[] array) : this()
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

		#region implicit operator
		public static implicit operator Vec4b(int val) => new Vec4b(val);
		public static implicit operator Vec4b(uint val) => new Vec4b(val);
		public static implicit operator int(Vec4b val) => val.m_val;
		#endregion

		#region equality operator
		public static bool operator ==(Vec4b left, Vec4b right) => Equals(left, right);
		public static bool operator !=(Vec4b left, Vec4b right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec4b DeepCopy()
		{
			return new Vec4b(m_val);
		}

		public void DeepCopyValueFrom(Vec4b obj)
		{
			m_val = obj.m_val;
		}
		#endregion

		#region IEquatable

		private static bool Equals(Vec4b left, Vec4b right)
		{
			return left.m_val == right.m_val;
		}

		public override bool Equals(object obj)
		{
			if (obj is Vec4b vec4b)
			{
				return Equals(vec4b);
			}
			return false;
		}

		public bool Equals(Vec4b other)
		{
			return Equals(this, other);
		}

		public override int GetHashCode()
		{
			return m_val.GetHashCode();
		}

		public bool Equals(int other)
		{
			return m_val == other;
		}

		public bool Equals(uint other)
		{
			return m_uval == other;
		}
		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Vec4b(m_val);
		}
		#endregion

		#region Enumeration
		private static IEnumerable<byte> BytesEnumeration(Vec4b vec4b)
		{
			yield return vec4b.m_byte1;
			yield return vec4b.m_byte2;
			yield return vec4b.m_byte3;
			yield return vec4b.m_byte4;
			yield break;
		}

		private static IEnumerable<int> Int32Enumeration(Vec4b vec4b)
		{
			yield return vec4b.m_val;
			yield break;
		}

		private static IEnumerable<uint> UInt32Enumeration(Vec4b vec4b)
		{
			yield return vec4b.m_uval;
			yield break;
		}
		#endregion

		#region IEnumerable
		public IEnumerator<byte> GetEnumerator()
		{
			return BytesEnumeration(this).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return BytesEnumeration(this).GetEnumerator();
		}

		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			return Int32Enumeration(this).GetEnumerator();
		}

		IEnumerator<uint> IEnumerable<uint>.GetEnumerator()
		{
			return UInt32Enumeration(this).GetEnumerator();
		}
		#endregion
	}

}
