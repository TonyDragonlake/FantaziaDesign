﻿using System;

namespace FantaziaDesign.Interop
{
	public static partial class User32
	{
		[Flags]
		public enum SWP
		{
			ASYNCWINDOWPOS = 16384,
			DEFERERASE = 8192,
			DRAWFRAME = 32,
			FRAMECHANGED = 32,
			HIDEWINDOW = 128,
			NOACTIVATE = 16,
			NOCOPYBITS = 256,
			NOMOVE = 2,
			NOOWNERZORDER = 512,
			NOREDRAW = 8,
			NOREPOSITION = 512,
			NOSENDCHANGING = 1024,
			NOSIZE = 1,
			NOZORDER = 4,
			SHOWWINDOW = 64,
			TOPMOST = 1563
		}
	}
}
