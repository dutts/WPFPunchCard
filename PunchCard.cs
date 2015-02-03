using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFPunchCard
{
    public class PunchCard : Canvas
    {
        private const int NumberOfHours = 24;

        private double _punchCardRenderWidth;
        private double _punchCardRenderHeight;
        private double _hourWidth;
        private double _categoryHeight;
        private double _countDiameterMultiplier;

        private long _numberOfCategories;

        #region DPs

        public List<Tuple<string, List<int>>> Data
        {
            get { return (List<Tuple<string,List<int>>>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(List<Tuple<string, List<int>>>), typeof(PunchCard));

        public double LabelMargin
        {
            get { return (double)GetValue(LabelMarginProperty); }
            set { SetValue(LabelMarginProperty, value); }
        }
        public static readonly DependencyProperty LabelMarginProperty =
            DependencyProperty.Register("LabelMargin", typeof(double), typeof(PunchCard), new PropertyMetadata(30.0));

       
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
            Data = new List<Tuple<string, List<int>>>
            {
                new Tuple<string, List<int>>("1", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
                new Tuple<string, List<int>>("2", new List<int> {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1}),
                new Tuple<string, List<int>>("3", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
                new Tuple<string, List<int>>("4", new List<int> {1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
                new Tuple<string, List<int>>("5", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 4, 1, 1, 1, 1, 1}),
                new Tuple<string, List<int>>("6", new List<int> {1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}),
                new Tuple<string, List<int>>("7", new List<int> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1})
            };
        }

        protected override void OnRender(DrawingContext dc)
        {
            _punchCardRenderWidth = ActualWidth - LabelMargin;
            _punchCardRenderHeight = ActualHeight - LabelMargin;
            _numberOfCategories = Data.Count;
            _hourWidth = _punchCardRenderWidth / NumberOfHours;
            _categoryHeight = _punchCardRenderHeight / _numberOfCategories;

            var maxCount = Data.Max((l => l.Item2.Max()));

            _countDiameterMultiplier = Math.Min(_hourWidth / maxCount, _categoryHeight / maxCount);

            DrawCategories(dc);
            DrawLabels(dc);
            //DrawPunches(dc);

            base.OnRender(dc);
        }

        private void DrawLabels(DrawingContext dc)
        {
            for (int i = 0; i < NumberOfHours; i++)
            {
                var xPos = _hourWidth * (i + 1) - _hourWidth / 2 + LabelMargin;
                dc.DrawText(new FormattedText(((i+1).ToString()), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10.0, Brushes.Black), new Point(xPos - 3, ActualHeight - (LabelMargin / 2)));
            }
        }

        private void DrawHourMarkers(DrawingContext dc, double yOffset)
        {
            for (int i = 0; i < NumberOfHours; i++)
            {
                var xPos = _hourWidth * (i + 1) - _hourWidth / 2 + LabelMargin;
                dc.DrawLine(HourMarkerPen, new Point(xPos, yOffset - 10 - LabelMargin), new Point(xPos, yOffset - LabelMargin));
            }
        }

        private void DrawCategories(DrawingContext dc)
        {
            for (int i = 0; i < _numberOfCategories; i++)
            {
                dc.DrawLine(CategoryLinePen, new Point(LabelMargin, _categoryHeight * (i + 1)), new Point(_punchCardRenderWidth + LabelMargin, _categoryHeight * (i + 1)));
                DrawHourMarkers(dc, _categoryHeight * (i + 1) + LabelMargin);
            }
        }

        private void DrawPunches(DrawingContext dc)
        {
            for (int i = 0; i < _numberOfCategories; i++)
            {
                double yOffset = _categoryHeight * (i + 1);

                for (int j = 0; j < NumberOfHours; j++)
                {
                    var xPos = _hourWidth * (j + 1) - _hourWidth / 2;
                    var punchPosition = new Point(xPos, yOffset - (_categoryHeight - 20.0)/2.0 - 10);
                    var punchDiameter = CalculatePunchDiameter(Data[i].Item2[j]);
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
