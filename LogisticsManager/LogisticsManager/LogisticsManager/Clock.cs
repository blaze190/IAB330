using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace LogisticsManager
{
    public class Clock : INotifyPropertyChanged
    {

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private int _userID;
        [NotNull]
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                this._userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }

        private DateTime _clockIn;
        [NotNull]
        public DateTime ClockIn
        {
            get
            {
                return _clockIn;
            }
            set
            {
                this._clockIn = value;
                OnPropertyChanged(nameof(ClockIn));
            }
        }

        private DateTime _clockOut;
        public DateTime ClockOut
        {
            get
            {
                return _clockOut;
            }
            set
            {
                this._clockOut = value;
                OnPropertyChanged(nameof(ClockOut));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

    }
}
