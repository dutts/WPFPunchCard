using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFPunchCard
{
    public class PunchCardCanvas : Canvas
    {
        private const int NumberOfHours = 24;

        private double _hourWidth;
        private double _categoryHeight;

        #region DPs

        public int NumberOfCategories
        {
            get { return (int)GetValue(NumberOfCategoriesProperty); }
            set { SetValue(NumberOfCategoriesProperty, value); }
        }
        public static readonly DependencyProperty NumberOfCategoriesProperty =
            DependencyProperty.Register("NumberOfCategories", typeof(int), typeof(PunchCardCanvas), new UIPropertyMetadata(null));

        public Pen CategoryLinePen
        {
            get { return (Pen)GetValue(CategoryLinePenProperty); }
            set { SetValue(CategoryLinePenProperty, value); }
        }
        public static readonly DependencyProperty CategoryLinePenProperty =
            DependencyProperty.Register("CategoryLinePen", typeof(Pen), typeof(PunchCardCanvas), new PropertyMetadata(new Pen(Brushes.DarkGray, 0.5)));

        public Pen HourMarkerPen
        {
            get { return (Pen)GetValue(HourMarkerPenProperty); }
            set { SetValue(HourMarkerPenProperty, value); }
        }
        public static readonly DependencyProperty HourMarkerPenProperty =
            DependencyProperty.Register("HourMarkerPen", typeof(Pen), typeof(PunchCardCanvas), new PropertyMetadata(new Pen(Brushes.LightGray, 0.5)));

        #endregion

        protected override void OnRender(DrawingContext dc)
        {
            _hourWidth = ActualWidth / NumberOfHours;
            _categoryHeight = ActualHeight / NumberOfCategories;

            DrawTiers(dc);

            base.OnRender(dc);
        }

        private void DrawHourMarkers(DrawingContext dc, double yOffset)
        {
            for (int i = 0; i < NumberOfHours; i++)
            {
                var xPos = _hourWidth * (i + 1) - _hourWidth / 2;
                dc.DrawLine(HourMarkerPen, new Point(xPos, yOffset - 10), new Point(xPos, yOffset));
            }
        }

        private void DrawTiers(DrawingContext dc)
        {
            for (int i = 0; i < NumberOfCategories; i++)
            {
                dc.DrawLine(CategoryLinePen, new Point(0.0, _categoryHeight * (i + 1)), new Point(ActualWidth, _categoryHeight * (i + 1)));
                DrawHourMarkers(dc, _categoryHeight * (i + 1));
            }
        }
    }
}
