using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFPunchCard
{
    public class PunchCard : Canvas
    {
        private const int NumberOfHours = 24;

        private double _hourWidth;
        private double _categoryHeight;
        private double _countDiameterMultiplier;

        #region DPs

        public List<List<int>> Data
        {
            get { return (List<List<int>>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("MyProperty", typeof(List<List<int>>), typeof(PunchCard));

        public int NumberOfCategories
        {
            get { return (int)GetValue(NumberOfCategoriesProperty); }
            set { SetValue(NumberOfCategoriesProperty, value); }
        }
        public static readonly DependencyProperty NumberOfCategoriesProperty =
            DependencyProperty.Register("NumberOfCategories", typeof(int), typeof(PunchCard), new UIPropertyMetadata(null));

        public Pen CategoryLinePen
        {
            get { return (Pen)GetValue(CategoryLinePenProperty); }
            set { SetValue(CategoryLinePenProperty, value); }
        }
        public static readonly DependencyProperty CategoryLinePenProperty =
            DependencyProperty.Register("CategoryLinePen", typeof(Pen), typeof(PunchCard), new PropertyMetadata(new Pen(Brushes.DarkGray, 0.5)));

        public Pen HourMarkerPen
        {
            get { return (Pen)GetValue(HourMarkerPenProperty); }
            set { SetValue(HourMarkerPenProperty, value); }
        }
        public static readonly DependencyProperty HourMarkerPenProperty =
            DependencyProperty.Register("HourMarkerPen", typeof(Pen), typeof(PunchCard), new PropertyMetadata(new Pen(Brushes.LightGray, 0.5)));

        

        #endregion

        public PunchCard()
        {
            Data = new List<List<int>>
            {
                new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new List<int> {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 4, 1, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };
        }

        protected override void OnRender(DrawingContext dc)
        {
            _hourWidth = ActualWidth / NumberOfHours;
            _categoryHeight = ActualHeight / NumberOfCategories;

            var maxCount = Data.Max((l => l.Max()));
            _countDiameterMultiplier = (_categoryHeight - 20.0)/maxCount;

            DrawTiers(dc);
            DrawPunches(dc);
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

        private void DrawPunches(DrawingContext dc)
        {
            double yOffset;
            for (int i = 0; i < NumberOfCategories; i++)
            {
                yOffset = _categoryHeight * (i + 1);

                for (int j = 0; j < NumberOfHours; j++)
                {
                    var xPos = _hourWidth * (j + 1) - _hourWidth / 2;
                    var punchPosition = new Point(xPos, yOffset - (_categoryHeight - 20.0)/2.0 - 10);
                    var punchDiameter = CalculatePunchDiameter(Data[i][j]);
                    dc.DrawEllipse(Brushes.Aqua, HourMarkerPen, punchPosition, punchDiameter / 2, punchDiameter / 2);
                }
            }
        }


        private double CalculatePunchDiameter(int count)
        {
            return count * _countDiameterMultiplier;
        }
    }
}
