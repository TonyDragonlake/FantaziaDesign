using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vec2ui :
	IDeepCopyable<Vec2ui>,
	IEquatable<Vec2ui>,
	ICloneable,
	IPseudoArray<uint>,
	IPseudoArray<ulong>
	{
		[FieldOffset(0)]
		private ulong m_val;

		[FieldOffset(0)]
		private uint m_int1;
		[FieldOffset(4)]
		private uint m_int2;

		#region IPseudoArray
		public uint this[int index]
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
		ulong IPseudoArray<ulong>.this[int index] { get => m_val; set => m_val = value; }
		int IPseudoArray<uint>.Count => 2;
		int IPseudoArray<ulong>.Count => 1;
		#endregion

		#region Ctor
		public Vec2ui(IPseudoArray<uint> array) : this()
		{
			if (array is null)
			{
				return;
			}
			if (array is Vec2ui vec2i)
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

		public Vec2ui(ulong val) : this()
		{
			m_val = val;
		}

		public Vec2ui(params uint[] array) : this()
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
		public static implicit operator Vec2ui(ulong val) => new Vec2ui(val);
		public static implicit operator ulong(Vec2ui val) => val.m_val;
		#endregion

		#region equality operator
		public static bool operator ==(Vec2ui left, Vec2ui right) => Equals(left, right);
		public static bool operator !=(Vec2ui left, Vec2ui right) => !Equals(left, right);
		#endregion

		#region IDeepCopyable
		public Vec2ui DeepCopy()
		{
			return new Vec2ui(m_val);
		}

		public void DeepCopyValueFrom(Vec2ui obj)
		{
			m_val = obj.m_val;
		}
		#endregion

		#region IEquatable
		private static bool Equals(Vec2ui left, Vec2ui right)
		{
			return left.m_val == right.m_val;
		}

		public override bool Equals(object obj)
		{
			if (obj is Vec2ui vec2i)
			{
				return Equals(vec2i);
			}
			return false;
		}

		public bool Equals(Vec2ui other)
		{
			return Equals(this, other);
		}

		public override int GetHashCode()
		{
			return m_val.GetHashCode();
		}

		public bool Equals(ulong other)
		{
			return m_val == other;
		}
		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Vec2ui(m_val);
		}
		#endregion

		#region Enumerations
		private static IEnumerable<uint> UInt32Enumeration(Vec2ui vec2i)
		{
			yield return vec2i.m_int1;
			yield return vec2i.m_int2;
			yield break;
		}

		private static IEnumerable<ulong> UInt64Enumeration(Vec2ui vec2i)
		{
			yield return vec2i.m_val;
			yield break;
		}

		#endregion

		#region IEnumerable
		public IEnumerator<uint> GetEnumerator()
		{
			return UInt32Enumeration(this).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return UInt32Enumeration(this).GetEnumerator();
		}

		IEnumerator<ulong> IEnumerable<ulong>.GetEnumerator()
		{
			return UInt64Enumeration(this).GetEnumerator();
		}
		#endregion
	}

}
