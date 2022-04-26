using System;
using System.Collections.Generic;

namespace MetricsManagerService.Models
{
    public class ValuesHolder
    {

        private Dictionary<DateTime, string> _values;

        public ValuesHolder()
        {
            _values = new Dictionary<DateTime, string >();
        }

        public void Add(string value, string time)
        {
            _values.Add(Convert.ToDateTime(time), value);
        }

        public string Get()
        {
            var returnValue = String.Empty;
            foreach (var item in _values)
            {
                returnValue += $"\nTime: {item.Key.ToShortTimeString()}, Temp: {item.Value}" ; 
            }
            return returnValue;
        }

        public string Get(DateTime from, DateTime to)
        {
            var returnValue = String.Empty;
            foreach (var item in _values)
            {
                if (item.Key >= from && item.Key <= to)
                { 
                    returnValue += $"\nTime: {item.Key.ToShortTimeString()}, Temp: {item.Value}";
                }
            }
            return returnValue;
        }

        public bool Update(DateTime time, string value)
        {
            if (_values.ContainsKey(time))
            {
                _values[time] = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Dictionary<DateTime, string> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public bool Delete (DateTime timeFrom, DateTime timeTo)
        {
            bool isDeleted = false;
            foreach (var item in _values)
            {
                if (item.Key >= timeFrom && item.Key <= timeTo)
                {
                    _values.Remove(item.Key);
                    isDeleted = true;
                }
            }
            if (isDeleted)
            {
                return true;
            }
            return false;
        }
    }
}
