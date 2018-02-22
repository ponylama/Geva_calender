using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjSellor.Dal;

namespace prjSellor.Models
{
    public class TimeTable
    {
        public DefaultSchedule sundaySche { get; set; }
        public DefaultSchedule mondaySche { get; set; }
        public DefaultSchedule tuesdaySche { get; set; }
        public DefaultSchedule wednesdaySche { get; set; }
        public DefaultSchedule thursdaySche { get; set; }
        public DefaultSchedule fridaySche { get; set; }
        public DefaultSchedule saturdaySche { get; set; }

        public User[] sundayUsers { get; set; }
        public User[] mondayUsers { get; set; }
        public User[] tuesdayUsers { get; set; }
        public User[] wednesdayUsers { get; set; }
        public User[] thursdayUsers { get; set; }
        public User[] fridayUsers { get; set; }
        public User[] saturdayUsers { get; set; }
        public TimeTable()
        {
            DateTime date = DateTime.Today;
            buildDay(date);
            buildDay(date.AddDays(1));
            buildDay(date.AddDays(2));
            buildDay(date.AddDays(3));
            buildDay(date.AddDays(4));
            buildDay(date.AddDays(5));
            buildDay(date.AddDays(6));
        }
        private void buildDay(DateTime date)
        {
            DataLayer DataDal = new DataLayer();
            int size; //size of array
            DefaultSchedule newDefault;
            string dayOfWeek = date.DayOfWeek.ToString();
            List<DefaultSchedule> defaultSchedule = (from u in DataDal.defaultSchedule
                                                     where (u.day == dayOfWeek)
                                                     select u).ToList<DefaultSchedule>();
            newDefault = defaultSchedule[0];
            DateTime tempDate = new DateTime(date.Year, date.Month, date.Day, newDefault.startTime.Hour, newDefault.startTime.Minute, 0);

            size = (int)((newDefault.endTime - newDefault.startTime).TotalHours * 60 / newDefault.timeSlace);
            //end calculate size 
            User[] temp = new User[size];
            for (int j = 0; j < size; j++)
                temp[j] = null;
            List<Breaks> breaks = (from u in DataDal.breaks
                          where (u.day == date.DayOfWeek.ToString())
                          select u).ToList<Breaks>();
            //fill array with breaks
            int i = 0;
            while (i < breaks.Count)
            {
                DateTime tempBreakStart = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, breaks[i].startTime.Hour, breaks[i].startTime.Minute, 0);
                DateTime tempBreakEnd = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, breaks[i].endTime.Hour, breaks[i].endTime.Minute, 0);

                int firstIndexOfBreak = (int)((tempBreakStart - tempDate).TotalHours * 60 / newDefault.timeSlace);
                int lastIndexOfBreak = (int)(firstIndexOfBreak + 60 / newDefault.timeSlace * ((tempBreakEnd - tempBreakStart).TotalHours));

                for (int j = firstIndexOfBreak; j < lastIndexOfBreak; j++)
                {
                    temp[j] = new User(0);//blank user. only for lock a cell.

                }
                i++;
            }

            //fill array with users
            DateTime nextDay = new DateTime(date.Year, date.Month, date.Day);
            nextDay = nextDay.AddDays(1);
            List<Schedule> usersInThisDay = (from u in DataDal.schedule
                                             where (u.startDate >= date && u.startDate < nextDay)
                                             select u).ToList<Schedule>();
            i = 0;
            while (i < usersInThisDay.Count)
            {
                double index = (usersInThisDay[i].startDate - tempDate).TotalHours * 60 / newDefault.timeSlace;
                int val = 0;
                double decimalpoints = Math.Abs(index - Math.Floor(index));
                if (decimalpoints > 0.5)
                    val = (int)Math.Round(index);
                else
                    val = (int)Math.Floor(index);
                temp[val] = new User(usersInThisDay[i].userName);
                i++;
            }
            string dayName = date.DayOfWeek.ToString();
            if (dayName == "Sunday")
            {
                sundayUsers = temp;
                sundaySche = newDefault;
            }
            if (dayName == "Monday")
            {
                mondayUsers = temp;
                mondaySche = newDefault;
            }
            if (dayName == "Tuesday")
            {
                tuesdayUsers = temp;
                tuesdaySche = newDefault;
            }
            if (dayName == "Wednesday")
            {
                wednesdayUsers = temp;
                wednesdaySche = newDefault;
            }
            if (dayName == "Thursday")
            {
                thursdayUsers = temp;
                thursdaySche = newDefault;
            }
            if (dayName == "Friday")
            {
                fridayUsers = temp;
                fridaySche = newDefault;
            }
            if (dayName == "Saturday")
            {
                saturdayUsers = temp;
                saturdaySche = newDefault;
            }


        }

        public DefaultSchedule getSchedule(string dayName)
        {
            if (dayName == "Sunday")
                return sundaySche;
            else if (dayName == "Monday")
                return mondaySche;
            else if (dayName == "Tuesday")
                return tuesdaySche;
            else if (dayName == "Wednesday")
                return wednesdaySche;
            else if (dayName == "Thursday")
                return thursdaySche;
            else if (dayName == "Friday")
                return fridaySche;
            else if (dayName == "Saturday")
                return saturdaySche;
            else
                return new DefaultSchedule();    //error?

        }

        public User[] getDayUsers(string dayName)
        {
            if (dayName == "Sunday")
                return sundayUsers;
            else if (dayName == "Monday")
                return mondayUsers;
            else if (dayName == "Tuesday")
                return tuesdayUsers;
            else if (dayName == "Wednesday")
                return wednesdayUsers;
            else if (dayName == "Thursday")
                return thursdayUsers;
            else if (dayName == "Friday")
                return fridayUsers;
            else if (dayName == "Saturday")
                return saturdayUsers;
            else
                return new User[0];
        }

    }
}