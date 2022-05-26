using System;
using System.Drawing;
using System.Linq;

namespace MissingNopedia
{
	internal static class Extensions
	{
		public static double Distance(this Point point, Rectangle rectangle)
		{
			if (rectangle.Contains(point)) return 0;

			var top = point.DistanceFromLine(new Point(rectangle.Left, rectangle.Top), new Point(rectangle.Right, rectangle.Top));
			var left = point.DistanceFromLine(new Point(rectangle.Left, rectangle.Top), new Point(rectangle.Left, rectangle.Bottom));
			var bottom = point.DistanceFromLine(new Point(rectangle.Left, rectangle.Bottom), new Point(rectangle.Right, rectangle.Bottom));
			var right = point.DistanceFromLine(new Point(rectangle.Right, rectangle.Bottom), new Point(rectangle.Right, rectangle.Bottom));

			return new[] { top, left, bottom, right }.Min();
		}

		public static double DistanceFromLine(this Point point, Point p1, Point p2)
		{
			var A = point.X - p1.X;
			var B = point.Y - p1.Y;
			var C = p2.X - p1.X;
			var D = p2.Y - p1.Y;

			var dot = A * C + B * D;
			var lengthSquare = C * C + D * D;
			var param = -1;
			if (lengthSquare != 0) // in case of 0 length line
				param = dot / lengthSquare;

			int xx, yy;

			if (param < 0)
			{
				xx = p1.X;
				yy = p1.Y;
			}
			else if (param > 1)
			{
				xx = p2.X;
				yy = p2.Y;
			}
			else
			{
				xx = p1.X + param * C;
				yy = p1.Y + param * D;
			}

			var dx = point.X - xx;
			var dy = point.Y - yy;
			return Math.Sqrt(dx * dx + dy * dy);
		}
	}
}
