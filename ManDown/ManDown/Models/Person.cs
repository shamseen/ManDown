using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManDown.Models
{
    public class Person
    {
        [PrimaryKey]
        public ContactType ContactType { get; set; }

        public string FirstName;
        public string LastName;
        public string PhoneNumber;

        public Person() { }

        public Person(string first, string last, string phone, int contactType)
        {
            FirstName = first;
            LastName = last;
            PhoneNumber = phone;
            ContactType = (ContactType)contactType;
        }
    }

    public enum ContactType
    {
        Patient = 0,
        Emergency = 1
    }
}
