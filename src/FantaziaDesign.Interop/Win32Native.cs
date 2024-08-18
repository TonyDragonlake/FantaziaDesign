using System;
using System.Collections.Generic;
using FantaziaDesign.Core;

namespace FantaziaDesign.Interop
{
	public struct MonitorDevInfo
	{
		public User32.MONITORINFOEX MonitorInfo;
		public User32.DEVMODE DevMode;
	}

	public static class Win32Native
	{
		public static IntPtr SetWindowLongPtr(IntPtr hWnd, User32.GWL nIndex, IntPtr dwNewLong)
		{
			if (8 == IntPtr.Size)
			{
				return User32.SetWindowLongPtr(hWnd, (int)nIndex, dwNewLong);
			}
			return new IntPtr(User32.SetWindowLong(hWnd, (int)nIndex, dwNewLong.ToInt32()));
		}

		public static bool TranslateLParamPointToClient(IntPtr hWnd, ref IntPtr lParam)
		{
			UnzipToWords(lParam, out var pt);
			if (User32.ScreenToClient(hWnd, ref pt))
			{
				lParam = new IntPtr((pt[1] << 16) | (pt[0] & 65535));
				return true;
			}
			return false;
		}

		public static bool TranslateLParamPointToClient(IntPtr hWnd, IntPtr lParam, out int x, out int y)
		{
			UnzipToWords(lParam, out var pt);
			if (User32.ScreenToClient(hWnd, ref pt))
			{
				x = pt[0]; y = pt[1];
				return true;
			}
			x = 0; y = 0;
			return false;
		}

		public static bool GetClientRectInScreenCoord(IntPtr hwnd, out Vec4i client)
		{
			if (User32.GetClientRect(hwnd, out client))
			{
				var pt = new Vec2i();
				if (User32.ScreenToClient(hwnd, ref pt))
				{
					Rects.OffsetRect(ref client, -pt[0], -pt[1]);
					return true;
				}
			}
			return false;
		}

		public static void UnzipToWords(IntPtr lparam, ref int loWord, ref int hiWord)
		{
			var ptrValue = lparam.ToInt32();
			loWord = (short)(ptrValue & 65535);
			hiWord = (short)(ptrValue >> 16);
		}

		public static void UnzipToWords(IntPtr lparam, out Vec2i pt)
		{
			var ptrValue = lparam.ToInt32();
			pt = new Vec2i((short)(ptrValue & 65535), (short)(ptrValue >> 16));
		}

		public static void ZipToInt(int loWord, int hiWord, out int res)
		{
			res = hiWord << 16;
			res |= loWord;
		}

		public static int GET_X_LPARAM(IntPtr lParam)
		{
			return LOWORD(lParam.ToInt32());
		}

		public static int GET_Y_LPARAM(IntPtr lParam)
		{
			return HIWORD(lParam.ToInt32());
		}

		public static int HIWORD(int i)
		{
			// ((WORD)((((DWORD_PTR)(_dw)) >> 16) & 0xffff))
			return (short)(i >> 16);
		}

		public static int LOWORD(int i)
		{
			// ((WORD)(((DWORD_PTR)(_dw)) & 0xffff))
			return (short)(i & 65535);
		}

		public static int HIWORD(IntPtr ptr)
		{
			// ((WORD)((((DWORD_PTR)(_dw)) >> 16) & 0xffff))
			return (short)(ptr.ToInt32() >> 16);
		}

		public static int LOWORD(IntPtr ptr)
		{
			// ((WORD)(((DWORD_PTR)(_dw)) & 0xffff))
			return (short)(ptr.ToInt32() & 65535);
		}

		public static Vec4i GetVirtualScreenRect()
		{
			var x = User32.GetSystemMetrics((int)User32.SM.XVIRTUALSCREEN);
			var y = User32.GetSystemMetrics((int)User32.SM.YVIRTUALSCREEN);
			var cx = User32.GetSystemMetrics((int)User32.SM.CXVIRTUALSCREEN);
			var cy = User32.GetSystemMetrics((int)User32.SM.CYVIRTUALSCREEN);
			return new Vec4i(x, y, x + cx, y + cy);
		}

		private class MonitorDevInfosCollector
		{
			private readonly List<MonitorDevInfo> m_monitorDevInfos = new List<MonitorDevInfo>();

			public IReadOnlyList<MonitorDevInfo> MonitorDevInfos => m_monitorDevInfos.ToArray();

			public void Collect()
			{
				User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, CollectCallback, IntPtr.Zero);
			}

			private bool CollectCallback(nint hMonitor, nint hdcMonitor, ref Vec4i lprcMonitor, nint dwData)
			{
				var mInfo = new User32.MONITORINFOEX();
				User32.MONITORINFOEX.Init(ref mInfo);
				if (User32.GetMonitorInfo(hMonitor, ref mInfo))
				{
					var devMode = new User32.DEVMODE();
					string lpszDeviceName = User32.MONITORINFOEX.GetDeviceName(ref mInfo);
					User32.EnumDisplaySettings(lpszDeviceName, -1, ref devMode);
					m_monitorDevInfos.Add(new MonitorDevInfo() { MonitorInfo = mInfo, DevMode = devMode });
				}
				return true;
			}
		}

		public static IReadOnlyList<MonitorDevInfo> GetMonitorDevInfos()
		{
			var collector = new MonitorDevInfosCollector();
			collector.Collect();
			return collector.MonitorDevInfos;
		}



	}
}
