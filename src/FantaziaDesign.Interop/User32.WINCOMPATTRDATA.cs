using System;

namespace FantaziaDesign.Interop
{
	public static partial class User32
	{
		public struct WINDOWCOMPOSITIONATTRIBDATA
		{
			public int attribute;
			public IntPtr pData;
			public int dataSize;
		}
	}
}
