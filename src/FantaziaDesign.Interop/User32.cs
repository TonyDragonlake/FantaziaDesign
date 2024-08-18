using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using FantaziaDesign.Core;

namespace FantaziaDesign.Interop
{
	/*
	 *  Copy from WinUser.h
	 *  ** NOT INCLUDED ITEMS LIST **
	 *  AnimateWindow() Commands
	 *  WM_KEYUP/DOWN/CHAR HIWORD(lParam) flags
	 *  Virtual Keys, Standard Set
	 *  SetWindowsHook() codes
	 *  Hook Codes
	 *  CBT Hook Codes
	 *  WM_WTSSESSION_CHANGE Api
	 *  WH_MSGFILTER Filter Proc Codes
	 *  Shell support
	 *  cmd for HSHELL_APPCOMMAND and WM_APPCOMMAND 
	 *  Low level hook flags
	 *  Keyboard Layout API
	 *  MOUSEMOVEPOINT
	 *  GetMouseMovePointsEx
	 *  Desktop-specific access flags
	 *  Desktop-specific control flags
	 *  Desktop Family
	 *  Windowstation-specific access flags
	 *  UserObjectSecurity
	 *  Class field offsets for GetClassLong()
	 *  lParam of WM_COPYDATA message points to...
	 *  MDINEXTMENU
	 *  LOWORD(wParam) values in WM_*UISTATE*
	 *  HIWORD(wParam) values in WM_*UISTATE*
	 *  POWERBROADCAST_SETTING
	 *  SendMessageTimeout values
	 *  WM_MOUSEACTIVATE Return Codes
	 *  WM_SETICON / WM_GETICON Type Codes
	 *  WM_SIZE message wParam values
	 *  Key State Masks for Mouse Messages
	 *  TRACKMOUSEEVENT
	 *  WM_PRINT flags 
	 *  3D border styles 
	 *  Border flags
	 *  DrawEdge
	 *  DrawFrameControl
	 *  DrawCaption
	 *  DrawAnimatedRects
	 *  Predefined Clipboard Formats
	 *  ACCEL
	 *  PAINTSTRUCT
	 *  CREATESTRUCTW
	 *  NMHDR
	 *  STYLESTRUCT
	 *  Owner draw control types
	 *  Owner draw actions
	 *  Owner draw state
	 *  MEASUREITEMSTRUCT for ownerdraw
	 *  DRAWITEMSTRUCT for ownerdraw
	 *  DELETEITEMSTRUCT for ownerdraw
	 *  COMPAREITEMSTUCT for ownerdraw sorting
	 *  SetMessageQueue
	 *  PeekMessage() Options
	 *  Hot key API
	 *  ExitWindowsEx API
	 *  SwapMouseButton
	 *  GetMessagePos
	 *  GetMessageExtraInfo
	 *  GetUnpredictedMessagePos
	 *  IsWow64Message
	 *  SetMessageExtraInfo
	 *  SendMessageTimeout
	 *  SendNotifyMessage
	 *  SendMessageCallback
	 *  BroadcastSystemMessage API
	 *  DeviceNotification API
	 *  PowerSettingNotification API
	 *  SuspendResumeNotification API
	 *  AttachThreadInput
	 *  ReplyMessage
	 *  WaitMessage
	 *  WaitForInputIdle
	 *  PostQuitMessage
	 *  CallWindowProc
	 *  InSendMessage API
	 *  GetDoubleClickTime
	 *  SetDoubleClickTime
	 *  GetClassInfo
	 *  IsWindow
	 *  IsMenu
	 *  IsChild
	 *  DeferWindowPos API
	 *  AnyPopup
	 *  DLGTEMPLATE
	 *  Dialog API
	 *  CallMsgFilter
	 *  Clipboard Manager Functions
	 *  Character Translation Routines
	 *  input & touch API
	 *  input status API (Capture)
	 *  Timer API
	 *  Menu API
	 *  DragObject
	 *  DragDetect
	 *  DrawIcon
	 *  DrawText() Format Flags
	 *  DrawState
	 *  TabbedTextOut API
	 *  SetActiveWindow
	 *  GetForegroundWindow
	 *  SwitchToThisWindow
	 *  SetForegroundWindow
	 *  AllowSetForegroundWindow
	 *  LockSetForegroundWindow
	 *  WindowFromDC
	 *  GetDCEx
	 *  BeginPaint
	 *  EndPaint
	 *  WindowRect API
	 *  WindowRgn API
	 *  Scroll API
	 *  AdjustWindowRectEx
	 *  MessageBox
	 *  Cursor API
	 *  Caret API
	 *  Window Class Functions
	 *  Window op Functions
	 *  WindowsHookEx API
	 *  Bitmap & Cursor API
	 *  Native Controls
	 *  WinEvent
	 *  System Sounds (idChild of system SOUND notification)
	 *  System Alerts (indexChild of system ALERT notification)
	 */

	/// <summary>
	/// static class for winuser.h & USER32.dll
	/// </summary>
	public static partial class User32
	{
		internal static readonly IReadOnlyDictionary<Type, object> marshallers = new Dictionary<Type, object>() {
			{typeof(MINMAXINFO), new GenericMarshaller<MINMAXINFO>() },
			{typeof(MONITORINFOEX),new GenericMarshaller<MONITORINFOEX>() },
			{typeof(NCCALCSIZE_PARAMS), new GenericMarshaller<NCCALCSIZE_PARAMS>() },
			{typeof(WINDOWPLACEMENT), new GenericMarshaller<WINDOWPLACEMENT>() },
			{typeof(WINDOWPOS), new GenericMarshaller<WINDOWPOS>() },
			{typeof(WNDCLASSEXW), new GenericMarshaller<WNDCLASSEXW>() }
		};

		public static IMarshaller<T> GetMarshaller<T>()
		{
			return Marshallers.GetMarshallerFromSource<T>(marshallers);
		}

		public delegate IntPtr WNDPROC(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		public delegate bool MONITORENUMPROC(IntPtr hMonitor, IntPtr hdcMonitor, ref Vec4i lprcMonitor, IntPtr dwData);

		public const string DLL_NAME = "USER32.dll";

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern uint RegisterWindowMessage(string lpString);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetMessage(ref MSG msg, IntPtr hWnd, int uMsgFilterMin, int uMsgFilterMax);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool TranslateMessage(ref MSG msg);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr DispatchMessage(ref MSG msg);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PeekMessage(ref MSG msg, IntPtr hwnd, int msgMin, int msgMax, int remove);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern int GetMessageTime();

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PostThreadMessage(int idThread, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern short RegisterClassEx(WNDCLASSEXW wc);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool UnregisterClass(string className, IntPtr hInstance);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool DestroyWindow(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlags);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CloseWindow(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetWindowDisplayAffinity(IntPtr hWnd, out int pdwAffinity);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetWindowDisplayAffinity(IntPtr hWnd, int dwAffinity);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool IsIconic(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);
		
		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool IsZoomed(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern int GetSystemMetrics(int nIndex);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool UpdateWindow(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PaintDesktop(IntPtr hdc);

		[DllImport(DLL_NAME, ExactSpelling = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetWindowText(IntPtr hWnd, char[] lpString);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPArray)] char[] lpString, int nMaxCount);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool GetClientRect(IntPtr hWnd, out Vec4i lpRect);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool GetWindowRect(IntPtr hWnd, out Vec4i lpRect);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ClientToScreen(IntPtr hWnd, ref Vec2i pt);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetCursorPos(out Vec2i pt);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ScreenToClient(IntPtr hWnd, ref Vec2i pt);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetDesktopWindow();

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, IntPtr pvParam, int fWinIni);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr MonitorFromPoint(Vec2i pt, int dwFlags);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr MonitorFromRect(ref Vec4i lprc, int dwFlags);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MONITORENUMPROC lpfnEnum, IntPtr lParam);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto)]
		public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetDpiForWindow(IntPtr hwnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetDpiForSystem();

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPArray)] char[] lpClassName, int nMaxCount);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetCapture();

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SetCapture(IntPtr hWnd);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ReleaseCapture();

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr WindowFromPoint(Vec2i Point);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT tme);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetWindowCompositionAttribute(IntPtr hWnd, ref WINDOWCOMPOSITIONATTRIBDATA pwcad);

		[DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetWindowCompositionAttribute(IntPtr hWnd, ref WINDOWCOMPOSITIONATTRIBDATA pwcad);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct DEVMODE
		{
			private const int CCHDEVICENAME = 32;

			private const int CCHFORMNAME = 32;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
			public string dmDeviceName;

			public short dmSpecVersion;

			public short dmDriverVersion;

			public short dmSize;

			public short dmDriverExtra;

			public int dmFields;

			public int dmPositionX;

			public int dmPositionY;

			public int dmDisplayOrientation;

			public int dmDisplayFixedOutput;

			public short dmColor;

			public short dmDuplex;

			public short dmYResolution;

			public short dmTTOption;

			public short dmCollate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
			public string dmFormName;

			public short dmLogPixels;

			public int dmBitsPerPel;

			public int dmPelsWidth;

			public int dmPelsHeight;

			public int dmDisplayFlags;

			public int dmDisplayFrequency;

			public int dmICMMethod;

			public int dmICMIntent;

			public int dmMediaType;

			public int dmDitherType;

			public int dmReserved1;

			public int dmReserved2;

			public int dmPanningWidth;

			public int dmPanningHeight;
		}
	}
}
