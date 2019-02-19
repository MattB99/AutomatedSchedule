using System;

namespace AutomatedSchedule
{
    internal class Person
    {
        //military time
        private int startTime, endTime;
        private String firstName, lastName;

        //for creating instance of person without time
        public Person(String fName, String lName)
        {
            firstName = fName;
            lastName = lName;
            startTime = 0;
            endTime = 0;
        }

        public Person(String fName, String lName, int sTime, int eTime)
        {
            firstName = fName;
            lastName = lName;
            startTime = sTime;
            endTime = eTime;
        }

        public String getFirstName()
        {
            return firstName;
        }

        public String getLastName()
        {
            return lastName;
        }

        public int getStartTime()
        {
            return startTime;
        }

        public int getEndTime()
        {
            return endTime;
        }

        public void setFirstName(String name)
        {
            firstName = name;
        }

        public void setLastName(String name)
        {
            lastName = name;
        }

        public void setStartTime(int sTime)
        {
            startTime = sTime;
        }

        public void setEndTime(int eTime)
        {
            endTime = eTime;
        }

        public String getFormattedTime(int militaryTime)
        {
            String formattedTime = "";
            int minutes = militaryTime % 100, hours = militaryTime / 100;
            
            if(hours < 12)
            {
                formattedTime += hours + ":" + minutes + " AM";
            }
            else
            {
                formattedTime += (hours-12) + ":" + minutes + " PM";
            }

            return formattedTime;

        }
    }
}