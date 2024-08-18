namespace FantaziaDesign.Interop
{
	public static partial class Dwmapi
	{
		public enum DWM_SYSTEMBACKDROP_TYPE : uint
		{
			/// <summary>
			/// Automatically selects backdrop effect.
			/// </summary>
			DWMSBT_AUTO = 0U,
			/// <summary>
			/// Turns off the backdrop effect.
			/// </summary>
			DWMSBT_DISABLE = 1U,
			/// <summary>
			/// Sets Mica effect with generated wallpaper tint.
			/// </summary>
			DWMSBT_MAINWINDOW = 2U,
			/// <summary>
			/// Sets acrlic effect.
			/// </summary>
			DWMSBT_TRANSIENTWINDOW = 3U,
			/// <summary>
			/// Sets blurred wallpaper effect, like Mica without tint.
			/// </summary>
			DWMSBT_TABBEDWINDOW = 4U
		}

	}


}
