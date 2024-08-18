using System;
using System.Runtime.InteropServices;
using FantaziaDesign.Core;

namespace FantaziaDesign.Interop
{
	public static partial class Dwmapi
	{
		public const string DLL_NAME = "dwmapi.dll";

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool DwmDefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref IntPtr plResult);
		
		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, ref DWM_BLURBEHIND pBlurBehind);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, Vec4i pMarInset);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		public static extern void DwmIsCompositionEnabled(ref bool pfEnabled);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		public static extern void DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		public static extern void DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

	}


}
