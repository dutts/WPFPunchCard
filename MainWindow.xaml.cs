using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace WPFPunchCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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

            TestData = new PunchCardData(new List<string>{"1","2","3","4","5","6","7"});
            TestData[0]. = new ObservableCollection<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        }

        public PunchCardData TestData
        {
            get { return (PunchCardData)GetValue(TestDataProperty); }
            set { SetValue(TestDataProperty, value); }
        }

        public static readonly DependencyProperty TestDataProperty =
            DependencyProperty.Register("TestData", typeof(PunchCardData), typeof(MainWindow));

        
        public List<Tuple<string, List<int>>> Data
        {
            get { return (List<Tuple<string, List<int>>>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(List<Tuple<string, List<int>>>), typeof(MainWindow));

        public bool ToolTips
        {
            get { return (bool)GetValue(ToolTipsProperty); }
            set { SetValue(ToolTipsProperty, value); }
        }
        public static readonly DependencyProperty ToolTipsProperty =
            DependencyProperty.Register("ToolTips", typeof(bool), typeof(MainWindow));

    }
}
