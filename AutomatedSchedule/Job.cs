using System;
using System.Collections.Generic;

namespace AutomatedSchedule
{
    internal class Job
    {
        private List<Person> workers;
        private bool cancelled;
        private String jobName;
        private DateTime jobDateTime;

        public Job(String jName, DateTime jDateTime)
        {
            workers = new List<Person>();
            jobName = jName;
            jobDateTime = jDateTime;
            cancelled = false;
        }

        public Job(String jName, DateTime jDateTime, bool canc)
        {
            workers = new List<Person>();
            jobName = jName;
            jobDateTime = jDateTime;
            cancelled = canc;
        }

        public List<Person> getWorkers()
        {
            return workers;
        }

        public String getJobName()
        {
            return jobName;
        }

        public DateTime getJobDateTime()
        {
            return jobDateTime;
        }

        public bool isCancelled()
        {
            return cancelled;
        }

        public void setWorkers(List<Person> w)
        {
            workers = w;
        }

        public void setJobName(String jName)
        {
            jobName = jName;
        }

        public void setJobDateTime(DateTime jDateTime)
        {
            jobDateTime = jDateTime;
        }

        public void setCancelled(bool canc)
        {
            cancelled = canc;
        }

        public void addWorker(Person p)
        {
            workers.Add(p);
        }
    }
}