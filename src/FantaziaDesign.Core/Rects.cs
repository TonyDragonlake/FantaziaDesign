using System;

namespace FantaziaDesign.Core
{
	public static class Rects
	{
		#region IPseudoArray<int>

		private static void ComposeInternal<TPseudoArray>(ref TPseudoArray rect, ref int val1, ref int val2, ref int val3, ref int val4, ref RectMode rectMode, ref RectMode dataMode) 
			where TPseudoArray : IPseudoArray<int>
		{
			rect[0] = val1;// l <=> x
			rect[1] = val2;// t <=> y
			if (rectMode == dataMode)
			{
				rect[2] = val3;
				rect[3] = val4;
			}
			else if (rectMode == RectMode.LTRB)
			{
				rect[2] = val3 + val1; // r = w + l
				rect[3] = val4 + val2; // b = h + t
			}
			else
			{
				rect[2] = val3 - val1; // w = r - l
				rect[3] = val4 - val2; // h = t - b
			}
		}

		private static void DecomposeInternal<TPseudoArray>(ref TPseudoArray rect, out int val1, out int val2, out int val3, out int val4, ref RectMode rectMode, ref RectMode dataMode)
			where TPseudoArray : IPseudoArray<int>
		{
			val1 = rect[0];// l <=> x
			val2 = rect[1];// t <=> y
			if (rectMode == dataMode)
			{
				val3 = rect[2];
				val4 = rect[3];
			}
			else if (dataMode == RectMode.LTRB)
			{
				val3 = rect[2] + val1; // r = w + l
				val4 = rect[3] + val2; // b = h + t
			}
			else
			{
				val3 = rect[2] - val1; // w = r - l
				val4 = rect[3] - val2; // h = t - b
			}
		}

		public static void Compose<TPseudoArray>(ref TPseudoArray rect, int val1, int val2, int val3, int val4, RectMode rectMode = RectMode.LTRB, RectMode dataMode = RectMode.LTRB)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException("Cannot cast PseudoArray as Rect");
			}
			ComposeInternal(ref rect, ref val1, ref val2, ref val3, ref val4, ref rectMode, ref dataMode);
		}

		public static void Compose<TPseudoArray>(ref TPseudoArray rect, IPseudoArray<int> point, IPseudoArray<int> sizeOrPoint, RectMode rectMode = RectMode.LTRB, RectMode dataMode = RectMode.LTRB)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (!PseudoArrays.MinimumElementCountEquals(point, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(point)} ) as Point");
			}
			if (!PseudoArrays.MinimumElementCountEquals(sizeOrPoint, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(sizeOrPoint)} ) as Size or Point");
			}
			int val1;
			int val2;
			int val3;
			int val4;
			if (dataMode == RectMode.LTRB)
			{
				val1 = Math.Min(point[0], sizeOrPoint[0]);
				val2 = Math.Max(point[0], sizeOrPoint[0]);
				val3 = Math.Min(point[1], sizeOrPoint[1]);
				val4 = Math.Max(point[1], sizeOrPoint[1]);
			}
			else
			{
				val1 = point[0];
				val2 = point[1];
				val3 = sizeOrPoint[0];
				val4 = sizeOrPoint[1];
			}
			ComposeInternal(ref rect, ref val1, ref val2, ref val3, ref val4, ref rectMode, ref dataMode);
		}

		public static void Decompose(out int val1, out int val2, out int val3, out int val4, IPseudoArray<int> rect, RectMode rectMode = RectMode.LTRB, RectMode dataMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			DecomposeInternal(ref rect, out val1, out val2, out val3, out val4, ref rectMode, ref dataMode);
		}

		public static void Decompose<TPseudoArray>(ref TPseudoArray point, ref TPseudoArray sizeOrPoint, IPseudoArray<int> rect, RectMode rectMode = RectMode.LTRB, RectMode dataMode = RectMode.LTRB)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (!PseudoArrays.MinimumElementCountEquals(point, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(point)} ) as Point");
			}
			if (!PseudoArrays.MinimumElementCountEquals(sizeOrPoint, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(sizeOrPoint)} ) as Size or Point");
			}
			DecomposeInternal(ref rect, out var val1, out var val2, out var val3, out var val4, ref rectMode, ref dataMode);
			point[0] = val1;
			point[1] = val2;
			sizeOrPoint[0] = val3;
			sizeOrPoint[1] = val4;
		}

		public static void GetLocation(out int ptx, out int pty, IPseudoArray<int> rect)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			ptx = rect[0];
			pty = rect[1];
		}

		public static void SetLocation<TPseudoArray>(ref TPseudoArray rect, int ptx, int pty)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			rect[0] = ptx;
			rect[1] = pty;
		}

		public static void GetWidth(out int width, IPseudoArray<int> rect, RectMode rectMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (rectMode == RectMode.LTRB)
			{
				width = rect[2] - rect[0];
			}
			else
			{
				width = rect[2];
			}
		}

		public static void SetWidth<TPseudoArray>(ref TPseudoArray rect, int width, RectMode rectMode = RectMode.LTRB, HorizontalLocation refHLoc = HorizontalLocation.Left)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (width < 0)
			{
				width = 0;
			}
			if (rectMode == RectMode.LTRB)
			{
				SetWidthInLTRBMode(ref rect, ref width, ref refHLoc);
			}
			else
			{
				SetWidthInXYWHMode(ref rect, ref width, ref refHLoc);
			}
		}

		public static void GetHeight(out int height, IPseudoArray<int> rect, RectMode rectMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (rectMode == RectMode.LTRB)
			{
				height = rect[3] - rect[1];
			}
			else
			{
				height = rect[3];
			}
		}

		public static void SetHeight<TPseudoArray>(ref TPseudoArray rect, int height, RectMode rectMode = RectMode.LTRB, VerticalLocation refVLoc = VerticalLocation.Top)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (height < 0)
			{
				height = 0;
			}
			if (rectMode == RectMode.LTRB)
			{
				SetHeightInLTRBMode(ref rect, ref height, ref refVLoc);
			}
			else
			{
				SetHeightInXYWHMode(ref rect, ref height, ref refVLoc);
			}
		}

		private static void SetWidthInLTRBMode<TPseudoArray>(ref TPseudoArray rect, ref int width, ref HorizontalLocation refHLoc) 
			where TPseudoArray : IPseudoArray<int>
		{
			switch (refHLoc)
			{
				case HorizontalLocation.Right:
					{
						rect[0] = rect[2] - width;
					}
					break;
				case HorizontalLocation.Center:
					{
						var dCenter = rect[0] + rect[2];
						rect[0] = (dCenter - width) / 2;
						rect[2] = rect[0] + width;
					}
					break;
				default:
					{
						rect[2] = rect[0] + width;
					}
					break;
			}
		}

		private static void SetWidthInXYWHMode<TPseudoArray>(ref TPseudoArray rect, ref int width, ref HorizontalLocation refHLoc) 
			where TPseudoArray : IPseudoArray<int>
		{
			switch (refHLoc)
			{
				case HorizontalLocation.Right:
					{
						rect[0] += rect[2] - width;
					}
					break;
				case HorizontalLocation.Center:
					{
						rect[0] += (rect[2] - width) / 2;
					}
					break;
				default:
					break;
			}
			rect[2] = width;
		}

		private static void SetHeightInLTRBMode<TPseudoArray>(ref TPseudoArray rect, ref int height, ref VerticalLocation refVLoc)
			where TPseudoArray : IPseudoArray<int>
		{
			switch (refVLoc)
			{
				case VerticalLocation.Bottom:
					{
						rect[1] = rect[3] - height;
					}
					break;
				case VerticalLocation.Middle:
					{
						var dCenter = rect[1] + rect[3];
						rect[1] = (dCenter - height) / 2;
						rect[3] = rect[1] + height;
					}
					break;
				default:
					{
						rect[3] = rect[1] + height;
					}
					break;
			}
		}

		private static void SetHeightInXYWHMode<TPseudoArray>(ref TPseudoArray rect, ref int height, ref VerticalLocation refVLoc)
			where TPseudoArray : IPseudoArray<int>
		{
			switch (refVLoc)
			{
				case VerticalLocation.Bottom:
					{
						rect[1] += rect[3] - height;
					}
					break;
				case VerticalLocation.Middle:
					{
						rect[1] += (rect[3] - height) / 2;
					}
					break;
				default:
					break;
			}
			rect[3] = height;
		}

		public static void GetSize(out int width, out int height, IPseudoArray<int> rect, RectMode rectMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (rectMode == RectMode.LTRB)
			{
				width = rect[2] - rect[0];
				height = rect[3] - rect[1];
			}
			else
			{
				width = rect[2];
				height = rect[3];
			}
		}

		public static void SetSize<TPseudoArray>(ref TPseudoArray rect, int width, int height, RectMode rectMode = RectMode.LTRB, HorizontalLocation refHLoc = HorizontalLocation.Left, VerticalLocation refVLoc = VerticalLocation.Top)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (rectMode == RectMode.LTRB)
			{
				SetWidthInLTRBMode(ref rect, ref width, ref refHLoc);
				SetHeightInLTRBMode(ref rect, ref height, ref refVLoc);
			}
			else
			{
				SetWidthInXYWHMode(ref rect, ref width, ref refHLoc);
				SetHeightInXYWHMode(ref rect, ref height, ref refVLoc);
			}
		}

		public static bool IsEmptyRect(IPseudoArray<int> rect, RectMode rectMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (rect is null)
			{
				return false;
			}
			if (rectMode == RectMode.LTRB)
			{
				return rect[0] >= rect[2] || rect[1] >= rect[3];
			}
			return rect[2] <= 0 || rect[3] <= 0;
		}

		public static void OffsetRect<TPseudoArray>(ref TPseudoArray rect, int x, int y, RectMode rectMode = RectMode.LTRB) 
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			rect[0] += x;
			rect[1] += y;
			if (rectMode == RectMode.LTRB)
			{
				rect[2] += x;
				rect[3] += y;
			}
		}

		public static bool ContainsPoint(IPseudoArray<int> rect, int px, int py, RectMode rectMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			var dataMode = RectMode.LTRB;
			DecomposeInternal(ref rect, out int l, out int t, out int r, out int b, ref rectMode, ref dataMode);
			return px >= l && px <= r && py >= t && py <= b;
		}

		public static bool ContainsPoint(IPseudoArray<int> rect, IPseudoArray<int> pt, RectMode rectMode = RectMode.LTRB)
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			var dataMode = RectMode.LTRB;
			DecomposeInternal(ref rect, out int l, out int t, out int r, out int b, ref rectMode, ref dataMode);
			var px = pt[0];
			var py = pt[1];
			return px >= l && px <= r && py >= t && py <= b;
		}

		public static void InflateRect<TPseudoArray>(ref TPseudoArray rect, IPseudoArray<int> thickness, RectMode rectMode = RectMode.LTRB)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (!PseudoArrays.MinimumElementCountEquals(thickness, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(thickness)} ) as Thickness");
			}

			if (thickness.Count < 4)
			{
				rect[0] -= thickness[0];
				rect[1] -= thickness[1];
				rect[2] += thickness[0];
				rect[3] += thickness[1];
				if (rectMode == RectMode.XYWH)
				{
					rect[2] += thickness[0];
					rect[3] += thickness[1];
				}
			}
			else
			{
				rect[0] -= thickness[0];
				rect[1] -= thickness[1];
				rect[2] += thickness[2];
				rect[3] += thickness[3];
				if (rectMode == RectMode.XYWH)
				{
					rect[2] += thickness[0];
					rect[3] += thickness[1];
				}
			}
		}

		public static void DeflateRect<TPseudoArray>(ref TPseudoArray rect, IPseudoArray<int> thickness, RectMode rectMode = RectMode.LTRB)
			where TPseudoArray : IPseudoArray<int>
		{
			if (!PseudoArrays.MinimumElementCountEquals(rect, 4))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(rect)} ) as Rect");
			}
			if (!PseudoArrays.MinimumElementCountEquals(thickness, 2))
			{
				throw new InvalidCastException($"Cannot cast PseudoArray ( {nameof(thickness)} ) as Thickness");
			}
			if (thickness.Count < 4)
			{
				rect[0] += thickness[0];
				rect[1] += thickness[1];
				rect[2] -= thickness[0];
				rect[3] -= thickness[1];
				if (rectMode == RectMode.XYWH)
				{
					rect[2] -= thickness[0];
					rect[3] -= thickness[1];
				}
			}
			else
			{
				rect[0] += thickness[0];
				rect[1] += thickness[1];
				rect[2] -= thickness[2];
				rect[3] -= thickness[3];
				if (rectMode == RectMode.XYWH)
				{
					rect[2] -= thickness[0];
					rect[3] -= thickness[1];
				}
			}
		}

		#endregion

		public static RectInt FromPointAndSize(int px, int py, int width, int height)
		{
			if (width < 0)
			{
				width = 0;
			}
			if (height < 0)
			{
				height = 0;
			}
			return new RectInt(px, py, px + width, py + height);
		}

		public static RectInt FromPointAndSize(IPoint<int> point, ISize<int> size) 
			=> FromPointAndSize(point.X, point.Y, size.Width, size.Height);

		public static bool IsEmptyRect(ref int val1, ref int val2, ref int val3, ref int val4, RectMode rectMode = RectMode.LTRB)
		{
			if (rectMode == RectMode.LTRB)
			{
				return val1 >= val3 || val2 >= val4;
			}
			return val3 <= 0 || val4 <= 0;
		}

		public static void OffsetRect(ref int val1, ref int val2, ref int val3, ref int val4, int x, int y, RectMode rectMode = RectMode.LTRB)
		{
			val1 += x;
			val2 += y;
			if (rectMode == RectMode.LTRB)
			{
				val3 += x;
				val4 += y;
			}
		}

		public static bool TryGetLocationAndSizeRaw(IPseudoArray<int> rect, out int left, out int top, out int width, out int height)
		{
			left = 0;
			top = 0;
			width = 0;
			height = 0;

			if (rect is null)
			{
				return false;
			}

			left = rect[0];
			top = rect[1];
			width = Math.Abs(rect[3] - left);
			height = Math.Abs(rect[4] - top);
			return true;
		}

		public static bool ContainsPoint(ref int val1, ref int val2, ref int val3, ref int val4, int px, int py, RectMode mode = RectMode.LTRB)
		{
			if (mode == RectMode.XYWH)
			{
				if (0 >= val3 || 0 >= val4)
				{
					return false;
				}
				return px >= val1
					&& px <= val1 + val3
					&& py >= val2
					&& py <= val2 + val4;
			}

			if (val1 >= val3 || val2 >= val4)
			{
				return false;
			}
			return px >= val1
				&& px <= val3
				&& py >= val2
				&& py <= val4;
		}

		public static void ExtendFromRect(this IRect<int> target, IRect<int> source, IThickness<int> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left = source.Left - thickness.Left;
			target.Top = source.Top - thickness.Top;
			target.Right = source.Right + thickness.Right;
			target.Bottom = source.Bottom + thickness.Bottom;
		}

		public static void ExtendSelf(this IRect<int> target, IThickness<int> thickness)
		{
			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left -= thickness.Left;
			target.Top -= thickness.Top;
			target.Right += thickness.Right;
			target.Bottom += thickness.Bottom;
		}

		public static RectInt ExtendedRect(IRect<int> source, IThickness<int> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}
			return new RectInt(
				source.Left - thickness.Left,
				source.Top - thickness.Top,
				source.Right + thickness.Right,
				source.Bottom + thickness.Bottom
				);
		}

		public static void ShrinkFromRect(this IRect<int> target, IRect<int> source, IThickness<int> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left = source.Left + thickness.Left;
			target.Top = source.Top + thickness.Top;
			target.Right = source.Right - thickness.Right;
			target.Bottom = source.Bottom - thickness.Bottom;
		}

		public static void ShrinkSelf(this IRect<int> target, IThickness<int> thickness)
		{
			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left += thickness.Left;
			target.Top += thickness.Top;
			target.Right -= thickness.Right;
			target.Bottom -= thickness.Bottom;
		}

		public static RectInt ShrunkRect(IRect<int> source, IThickness<int> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}
			return new RectInt(
				source.Left + thickness.Left,
				source.Top + thickness.Top,
				source.Right - thickness.Right,
				source.Bottom - thickness.Bottom
				);
		}

		public static RectF FromPointAndSize(float px, float py, float width, float height)
		{
			if (width < 0)
			{
				width = 0;
			}
			if (height < 0)
			{
				height = 0;
			}
			return new RectF(px, py, px + width, py + height);
		}

		public static RectF FromPointAndSize(IPoint<float> point, ISize<float> size)
			=> FromPointAndSize(point.X, point.Y, size.Width, size.Height);

		public static bool IsEmptyRect(ref float val1, ref float val2, ref float val3, ref float val4, RectMode rectMode = RectMode.LTRB)
		{
			if (rectMode == RectMode.LTRB)
			{
				return val1 >= val3 || val2 >= val4;
			}
			return val3 <= 0 || val4 <= 0;
		}

		public static bool IsEmptyRect(IPseudoArray<float> rect, RectMode rectMode = RectMode.LTRB)
		{
			if (rect is null)
			{
				return false;
			}
			if (rectMode == RectMode.LTRB)
			{
				return rect[0] >= rect[2] || rect[1] >= rect[3];
			}
			return rect[2] <= 0 || rect[3] <= 0;
		}

		public static void OffsetRect(ref float val1, ref float val2, ref float val3, ref float val4, float x, float y, RectMode rectMode = RectMode.LTRB)
		{
			val1 += x;
			val2 += y;
			if (rectMode == RectMode.LTRB)
			{
				val3 += x;
				val4 += y;
			}
		}

		public static void OffsetRect(IPseudoArray<float> rect, float x, float y, RectMode rectMode = RectMode.LTRB)
		{
			if (rect is null)
			{
				throw new ArgumentNullException(nameof(rect));
			}
			rect[0] += x;
			rect[1] += y;
			if (rectMode == RectMode.LTRB)
			{
				rect[2] += x;
				rect[3] += y;
			}
		}

		public static bool TryGetLocationAndSizeRaw(IPseudoArray<float> integer4, out float left, out float top, out float width, out float height)
		{
			left = 0;
			top = 0;
			width = 0;
			height = 0;

			if (integer4 is null)
			{
				return false;
			}

			left = integer4[0];
			top = integer4[1];
			width = Math.Abs(integer4[2] - left);
			height = Math.Abs(integer4[3] - top);
			return true;
		}

		public static bool ContainsPoint(ref float val1, ref float val2, ref float val3, ref float val4, float px, float py, RectMode mode = RectMode.LTRB)
		{
			if (mode == RectMode.XYWH)
			{
				if (0 >= val3 || 0 >= val4)
				{
					return false;
				}
				return px >= val1
					&& px <= val1 + val3
					&& py >= val2
					&& py <= val2 + val4;
			}

			if (val1 >= val3 || val2 >= val4)
			{
				return false;
			}
			return px >= val1
				&& px <= val3
				&& py >= val2
				&& py <= val4;
		}

		public static bool ContainsPoint(IPseudoArray<float> integer4, int px, int py, RectMode mode = RectMode.LTRB)
		{
			if (mode == RectMode.XYWH)
			{
				if (0 >= integer4[2] || 0 >= integer4[3])
				{
					return false;
				}
				return px >= integer4[0]
					&& px <= integer4[0] + integer4[2]
					&& py >= integer4[1]
					&& py <= integer4[1] + integer4[3];
			}

			if (integer4[0] >= integer4[2] || integer4[1] >= integer4[3])
			{
				return false;
			}
			return px >= integer4[0]
				&& px <= integer4[2]
				&& py >= integer4[1]
				&& py <= integer4[3];
		}

		public static void ExtendFromRect(this IRect<float> target, IRect<float> source, IThickness<float> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left = source.Left - thickness.Left;
			target.Top = source.Top - thickness.Top;
			target.Right = source.Right + thickness.Right;
			target.Bottom = source.Bottom + thickness.Bottom;
		}

		public static void ExtendSelf(this IRect<float> target, IThickness<float> thickness)
		{
			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left -= thickness.Left;
			target.Top -= thickness.Top;
			target.Right += thickness.Right;
			target.Bottom += thickness.Bottom;
		}

		public static RectF ExtendedRect(IRect<float> source, IThickness<float> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}
			return new RectF(
				source.Left - thickness.Left,
				source.Top - thickness.Top,
				source.Right + thickness.Right,
				source.Bottom + thickness.Bottom
				);
		}

		public static void ShrinkFromRect(this IRect<float> target, IRect<float> source, IThickness<float> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left = source.Left + thickness.Left;
			target.Top = source.Top + thickness.Top;
			target.Right = source.Right - thickness.Right;
			target.Bottom = source.Bottom - thickness.Bottom;
		}

		public static void ShrinkSelf(this IRect<float> target, IThickness<float> thickness)
		{
			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}

			target.Left += thickness.Left;
			target.Top += thickness.Top;
			target.Right -= thickness.Right;
			target.Bottom -= thickness.Bottom;
		}

		public static RectF ShrunkRect(IRect<float> source, IThickness<float> thickness)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (thickness is null)
			{
				throw new ArgumentNullException(nameof(thickness));
			}
			return new RectF(
				source.Left + thickness.Left,
				source.Top + thickness.Top,
				source.Right - thickness.Right,
				source.Bottom - thickness.Bottom
				);
		}

	}

}
