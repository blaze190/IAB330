using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace LogisticsManager
{
    public class Report : INotifyPropertyChanged
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

        private string _type;
        [NotNull]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                this._type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private string _desc;
        [NotNull]
        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                this._desc = value;
                OnPropertyChanged(nameof(Desc));
            }
        }

        private int _companyID;
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                this._companyID = value;
                OnPropertyChanged(nameof(CompanyID));
            }
        }

        private int _fromUserID;
        public int FromUserID
        {
            get
            {
                return _fromUserID;
            }
            set
            {
                this._fromUserID = value;
                OnPropertyChanged(nameof(FromUserID));
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            set
            {
                this._creationDate = value;
                OnPropertyChanged(nameof(CreationDate));
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
