using System;

namespace FantaziaDesign.Core
{
	[Flags]
	public enum OrientationFlags : byte
	{
		Unknown,
		Horizontal,
		Vertical,
		Both = Horizontal | Vertical
	}

}
