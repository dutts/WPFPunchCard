using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WPFPunchCard
{
    public class PunchCardData : INotifyCollectionChanged
    {
        private readonly ObservableCollection<Tuple<string, ObservableCollection<int>>>  _data;

        public PunchCardData(IEnumerable<string> categories)
        {
            _data = new ObservableCollection<Tuple<string, ObservableCollection<int>>>();
            foreach (var category in categories)
            {
                var categoryCollection = new ObservableCollection<int>();
                categoryCollection.CollectionChanged += this.CollectionChanged;

                _data.Add(new Tuple<string, ObservableCollection<int>>(category, categoryCollection));
            }
        }

        public ObservableCollection<int> this[int i]
        {
            get { return _data[i].Item2; }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
