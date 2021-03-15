using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTrashCheck
{
    public class StockTakingDetails
    {
        private DateTime _date;
        private string _department;
        private string _person;


        public StockTakingDetails()
        {
            this._date = new DateTime();
            this._department = string.Empty;
            this._person = string.Empty;
        }

        public StockTakingDetails(DateTime date, string department, string person)
        {
            this._date = date;
            this._department = department;
            this._person = person;
        }

        public string Person
        {
            get { return _person; }
            set { _person = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

    }
}