using System.Runtime.InteropServices;

namespace FantaziaDesign.Interop
{
	public static class Ntdll
	{
		public const string DLLNAME = "NTDLL.dll";

		[DllImport(DLLNAME, EntryPoint = "RtlGetNtVersionNumbers", SetLastError = true)]
		public static extern bool GetNtVersionNumbers(ref int dwMajorVer, ref int dwMinorVer, ref int dwBuildNumber);
	}
}
