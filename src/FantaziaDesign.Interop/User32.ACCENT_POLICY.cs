namespace FantaziaDesign.Interop
{

	public static partial class User32
	{
		/// <summary>
		/// WCA window accent policy.
		/// </summary>
		public struct ACCENT_POLICY
		{
			public ACCENT_STATE nAccentState;
			public uint nFlags;
			public uint nColor;
			public uint nAnimationId;
		}


	}
}
