﻿namespace FantaziaDesign.Interop
{
	public static partial class Gdi32
	{
		public enum ROP_CODE : uint
		{
			BLACKNESS = 0x00000042,
			NOTSRCERASE = 0x001100A6,
			NOTSRCCOPY = 0x00330008,
			SRCERASE = 0x00440328,
			DSTINVERT = 0x00550009,
			PATINVERT = 0x005A0049,
			SRCINVERT = 0x00660046,
			SRCAND = 0x008800C6,
			MERGEPAINT = 0x00BB0226,
			MERGECOPY = 0x00C000CA,
			SRCCOPY = 0x00CC0020,
			SRCPAINT = 0x00EE0086,
			PATCOPY = 0x00F00021,
			PATPAINT = 0x00FB0A09,
			WHITENESS = 0x00FF0062,
			CAPTUREBLT = 0x40000000,
			NOMIRRORBITMAP = 0x80000000,
		}
	}
}