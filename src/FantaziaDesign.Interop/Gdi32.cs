using System;
using System.Runtime.InteropServices;

namespace FantaziaDesign.Interop
{
	public static partial class Gdi32
	{
		public const string DLL_NAME = "GDI32.dll";

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, ROP_CODE rop);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, DIB_USAGE usage, out IntPtr ppvBits, IntPtr hSection, uint offset);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hGdiObj);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern int GetObject(IntPtr hGdiObj, int c, IntPtr pv);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern int GetDIBits(IntPtr hdc, IntPtr hbm, uint start, uint cLines, IntPtr lpvBits, ref BITMAPINFO lpbmi, DIB_USAGE usage);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool DeleteObject(IntPtr ho);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetDeviceCaps(IntPtr hdc, int index);


	}
}
