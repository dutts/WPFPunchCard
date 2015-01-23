using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFPunchCard
{
    public class PunchCard : Canvas
    {
        private const int NumberOfHours = 24;

        #region DPs
        public int[][] Data
        {
            get { return (int[][])GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(int[][]), typeof(PunchCard));

        

        public int NumberOfTiers
        {
            get { return (int)GetValue(NumberOfTiersProperty); }
            set { SetValue(NumberOfTiersProperty, value); }
        }
        public static readonly DependencyProperty NumberOfTiersProperty =
            DependencyProperty.Register("NumberOfTiers", typeof(int), typeof(PunchCard), new UIPropertyMetadata(null));

        public Pen TierLinePen
        {
            get { return (Pen)GetValue(TierLinePenProperty); }
            set { SetValue(TierLinePenProperty, value); }
        }
        public static readonly DependencyProperty TierLinePenProperty =
            DependencyProperty.Register("TierLinePen", typeof(Pen), typeof(PunchCard), new PropertyMetadata(new Pen(Brushes.DarkGray, 0.5)));

        public Pen HourMarkerPen
        {
            get { return (Pen)GetValue(HourMarkerPenProperty); }
            set { SetValue(HourMarkerPenProperty, value); }
        }
        public static readonly DependencyProperty HourMarkerPenProperty =
            DependencyProperty.Register("HourMarkerPen", typeof(Pen), typeof(PunchCard), new PropertyMetadata(new Pen(Brushes.LightGray, 0.5)));

        #endregion

        protected override void OnRender(DrawingContext dc)
        {
            DrawTiers(dc);
            base.OnRender(dc);
        }

        private void DrawHourMarkers(DrawingContext dc, double yOffset)
        {
            var hourWidth = ActualWidth / NumberOfHours;
            for (int i = 0; i < NumberOfHours; i++)
            {
                var xPos = hourWidth*(i + 1) - hourWidth/2;
                dc.DrawLine(HourMarkerPen, new Point(xPos, yOffset - 10), new Point(xPos, yOffset));
            }
        }

        private void DrawTiers(DrawingContext dc)
        {
            var tierHeight = ActualHeight/NumberOfTiers;
            for (int i = 0; i < NumberOfTiers; i++)
            {
                dc.DrawLine(TierLinePen, new Point(0.0, tierHeight * (i + 1)), new Point(ActualWidth, tierHeight * (i + 1)));
                DrawHourMarkers(dc, tierHeight * (i + 1));
            }
        }
    }
}
