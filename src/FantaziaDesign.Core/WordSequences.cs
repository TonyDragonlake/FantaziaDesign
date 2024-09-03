using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Core
{
	public enum EndianLayout
	{
		Little,
		Big,
		System
	}

	public static class EndianLayouts
	{
		private static EndianLayout s_endian = EndianLayout.Big;
		public static EndianLayout Endian { get => s_endian; set => s_endian = value; }
		public static bool IsLittleEndian => s_endian == EndianLayout.System ? BitConverter.IsLittleEndian : s_endian == EndianLayout.Little;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct Word : IPseudoArray<byte>, IPseudoArray<ushort>, IEquatable<Word>, ICloneable
	{
		public const int ByteCount = 2;
		public const int WordCount = 1;

		[FieldOffset(0)]
		private byte m_byte1;
		[FieldOffset(1)]
		private byte m_byte2;

		[FieldOffset(0)]
		private ushort m_value;

		#region Ctor
		public Word(byte loByte, byte hiByte) : this()
		{
			LoByte = loByte;
			HiByte = hiByte;
		}

		public Word(ushort value) : this()
		{
			Value = value;
		}
		#endregion

		#region Props
		public byte LoByte { get => GetLoByte(); set => SetLoByte(value); }

		public byte HiByte { get => GetHiByte(); set => SetHiByte(value); }

		public ushort Value { get => m_value; set => m_value = value; }

		private byte GetLoByte()
		{
			if (EndianLayouts.IsLittleEndian)
			{
				return m_byte1;
			}
			return m_byte2;
		}

		private void SetLoByte(byte value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_byte1 = value;
				return;
			}
			m_byte2 = value;
		}

		private byte GetHiByte()
		{
			if (EndianLayouts.IsLittleEndian)
			{
				return m_byte2;
			}
			return m_byte1;
		}

		private void SetHiByte(byte value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_byte2 = value;
				return;
			}
			m_byte1 = value;
		}
		#endregion

		#region IPseudoArray
		public byte this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return LoByte;
					case 1: return HiByte;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{ByteCount - 1}]");
				}
			}
			set
			{
				switch (index)
				{
					case 0: LoByte = value; break;
					case 1: HiByte = value; break;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{ByteCount - 1}]");
				}
			}
		}
		public int Count => ByteCount;

		int IPseudoArray<ushort>.Count => WordCount;

		ushort IPseudoArray<ushort>.this[int index] { get => m_value; set => m_value = value; }
		#endregion

		#region implicit operator
		public static implicit operator Word(ushort val) => new Word(val);
		public static implicit operator ushort(Word val) => val.m_value;
		#endregion

		#region equality operator
		public static bool operator ==(Word left, Word right) => left.m_value == right.m_value;
		public static bool operator !=(Word left, Word right) => left.m_value != right.m_value;
		#endregion

		#region IEquatable
		public override bool Equals(object obj)
		{
			return obj is Word word && Equals(word);
		}

		public bool Equals(Word other)
		{
			return m_value == other.m_value;
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}
		#endregion

		#region ICloneable
		public object Clone()
		{
			return new Word(m_value);
		}
		#endregion

		#region Enumeration
		private static IEnumerable<byte> BytesEnumeration(Word word)
		{
			yield return word.LoByte;
			yield return word.HiByte;
			yield break;
		}

		private static IEnumerable<ushort> UInt16Enumeration(Word word)
		{
			yield return word.Value;
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
			return GetEnumerator();
		}

		IEnumerator<ushort> IEnumerable<ushort>.GetEnumerator()
		{
			return UInt16Enumeration(this).GetEnumerator();
		}
		#endregion

		public override string ToString()
		{
			return m_value.ToString();
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct DWord : IPseudoArray<byte>, IPseudoArray<ushort>, IPseudoArray<Word>, IPseudoArray<uint>, IEquatable<DWord>, ICloneable
	{
		public const int ByteCount = 4;
		public const int WordCount = 2;
		public const int DWordCount = 1;

		[FieldOffset(0)]
		private Word m_word1;
		[FieldOffset(2)]
		private Word m_word2;

		[FieldOffset(0)]
		private uint m_value;

		#region Ctor
		public DWord(ushort loUInt16, ushort hiUInt16) : this()
		{
			LoWord = loUInt16;
			HiWord = hiUInt16;
		}

		public DWord(Word loWord, Word hiWord) : this()
		{
			LoWord = loWord;
			HiWord = hiWord;
		}

		public DWord(uint value) : this()
		{
			Value = value;
		}
		#endregion

		#region Props
		public Word LoWord { get => GetLoWord(); set => SetLoWord(value); }

		public Word HiWord { get => GetHiWord(); set => SetHiWord(value); }

		public ushort LoUInt16 { get => GetLoWord(); set => SetLoWord(value); }

		public ushort HiUInt16 { get => GetHiWord(); set => SetHiWord(value); }

		public uint Value { get => m_value; set => m_value = value; }

		private Word GetLoWord()
		{
			if (EndianLayouts.IsLittleEndian)
			{
				return m_word1;
			}
			return m_word2;
		}

		private void SetLoWord(Word value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_word1 = value;
				return;
			}
			m_word2 = value;
		}

		public void SetLoWordLoByte(byte value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_word1.LoByte = value;
				return;
			}
			m_word2.LoByte = value;
		}

		public void SetLoWordHiByte(byte value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_word1.HiByte = value;
				return;
			}
			m_word2.HiByte = value;
		}

		private Word GetHiWord()
		{
			if (EndianLayouts.IsLittleEndian)
			{
				return m_word2;
			}
			return m_word1;
		}

		private void SetHiWord(Word value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_word2 = value;
				return;
			}
			m_word1 = value;
		}

		public void SetHiWordLoByte(byte value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_word2.LoByte = value;
				return;
			}
			m_word1.LoByte = value;
		}

		public void SetHiWordHiByte(byte value)
		{
			if (EndianLayouts.IsLittleEndian)
			{
				m_word2.HiByte = value;
				return;
			}
			m_word1.HiByte = value;
		}
		#endregion

		#region IPseudoArray
		public int Count => ByteCount;

		int IPseudoArray<ushort>.Count => WordCount;

		int IPseudoArray<Word>.Count => WordCount;

		int IPseudoArray<uint>.Count => DWordCount;

		uint IPseudoArray<uint>.this[int index] { get => m_value; set => m_value = value; }
		Word IPseudoArray<Word>.this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return LoWord;
					case 1: return HiWord;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{WordCount - 1}]");
				}
			}
			set
			{
				switch (index)
				{
					case 0: LoWord = value; break;
					case 1: HiWord = value; break;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{WordCount - 1}]");
				}
			}
		}
		ushort IPseudoArray<ushort>.this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return LoWord;
					case 1: return HiWord;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{WordCount - 1}]");
				}
			}
			set
			{
				switch (index)
				{
					case 0: LoWord = value; break;
					case 1: HiWord = value; break;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{WordCount - 1}]");
				}
			}
		}
		public byte this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return LoWord.LoByte;
					case 1:
						return LoWord.HiByte;
					case 2:
						return HiWord.LoByte;
					case 3:
						return HiWord.HiByte;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{ByteCount - 1}]");
				}

			}
			set
			{
				switch (index)
				{
					case 0:
						SetLoWordLoByte(value); break;
					case 1:
						SetLoWordHiByte(value); break;
					case 2:
						SetHiWordLoByte(value); break;
					case 3:
						SetHiWordHiByte(value); break;
					default:
						throw new IndexOutOfRangeException($"index should be in range of [0,{ByteCount - 1}]");
				}
			}
		}
		#endregion

		#region implicit operator
		public static implicit operator DWord(uint val) => new DWord(val);
		public static implicit operator uint(DWord val) => val.m_value;
		#endregion

		#region equality operator
		public static bool operator ==(DWord left, DWord right) => left.m_value == right.m_value;
		public static bool operator !=(DWord left, DWord right) => left.m_value != right.m_value;
		#endregion

		#region IEquatable
		public override bool Equals(object obj)
		{
			return obj is DWord dword && Equals(dword);
		}

		public bool Equals(DWord other)
		{
			return m_value == other.m_value;
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode();
		}
		#endregion

		#region ICloneable
		public object Clone()
		{
			return new DWord(m_value);
		}
		#endregion

		#region Enumeration
		private static IEnumerable<byte> BytesEnumeration(DWord dword)
		{
			yield return dword.LoWord.LoByte;
			yield return dword.LoWord.HiByte;
			yield return dword.HiWord.LoByte;
			yield return dword.HiWord.HiByte;
			yield break;
		}

		private static IEnumerable<Word> WordEnumeration(DWord dword)
		{
			yield return dword.LoWord;
			yield return dword.HiWord;
			yield break;
		}

		private static IEnumerable<ushort> UInt16Enumeration(DWord dword)
		{
			yield return dword.LoWord;
			yield return dword.HiWord;
			yield break;
		}

		private static IEnumerable<uint> UInt32Enumeration(DWord dword)
		{
			yield return dword.m_value;
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
			return GetEnumerator();
		}

		IEnumerator<ushort> IEnumerable<ushort>.GetEnumerator()
		{
			return UInt16Enumeration(this).GetEnumerator();
		}

		IEnumerator<Word> IEnumerable<Word>.GetEnumerator()
		{
			return WordEnumeration(this).GetEnumerator();
		}

		IEnumerator<uint> IEnumerable<uint>.GetEnumerator()
		{
			return UInt32Enumeration(this).GetEnumerator();
		}
		#endregion

		public override string ToString()
		{
			return m_value.ToString();
		}
	}

}
