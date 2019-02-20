﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;


namespace AutomatedSchedule
{
    public partial class AutomatedScheduler : Form
    {
        //declares List of jobs
        List<Job> jobs;

        String[] lines;

        public AutomatedScheduler()
        {
            InitializeComponent();

            //initializes list of jobs
            jobs = new List<Job>();
            if (File.Exists(@"userData.txt") == true)
            {
                lines = System.IO.File.ReadAllLines(@"userData.txt");
                if (lines[0] != null)
                    fNameBox.Text = lines[0];
                if (lines[1] != null)
                    lNameBox.Text = lines[1];
                if (lines[2] != null)
                    usernameBox.Text = lines[2];
                if (lines[3] != null)
                    passwordBox.Text = lines[3];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fiveDayInc.Checked == false && tenDayInc.Checked == false && twentyDayInc.Checked == false && thirtyDayInc.Checked == false)
            {
                MessageBox.Show("Please select the number of days you'd like to search through.");
            }
            else
            {
                excelView.Visible = true;
                notepadView.Visible = true;
                label5.Visible = true;
                lines = System.IO.File.ReadAllLines(@"userData.txt");
                String fName = lines[0];
                String lName = lines[1];
                IWebDriver driver = new ChromeDriver();
                driver.Url = "http://172.21.20.41/cepdotnet/CEPloginToCEP.aspx";

                IWebElement username = driver.FindElement(By.Id("txtUserName"));
                IWebElement password = driver.FindElement(By.Id("txtPassword"));

                username.Clear();
                username.SendKeys(lines[2]);

                password.Clear();
                password.SendKeys(lines[3]);

                IWebElement lgnBtn = driver.FindElement(By.Id("sbtLogin"));
                lgnBtn.Click();

                ReadOnlyCollection<IWebElement> elements;

                int cMonth = DateTime.Now.Month, cDay = DateTime.Now.Day, cYear = DateTime.Now.Year, dateCounter = 0, scheduledDatesIndex = 0; ;
                if (fiveDayInc.Checked)
                    dateCounter = 5;
                else if (tenDayInc.Checked)
                    dateCounter = 10;
                else if (twentyDayInc.Checked)
                    dateCounter = 20;
                else if (thirtyDayInc.Checked)
                    dateCounter = 30;


                System.DateTime[] scheduledDates = new System.DateTime[dateCounter];

                String[] tempElementArray = null, tempStringDateTime;
                String tempJobName;
                bool tempCancelled = false;
                int tempHour, indexChange, tempMin, tempMonth;
                Job tempJob = null;
                Person tempPerson = null;

                for (int i = dateCounter; i > 0; i--)
                {


                    driver.Url = "http://172.21.20.41/cepdotnet/CEPHome.aspx?day=" + cDay + "&month=" + cMonth + "&year=" + cYear;

                    elements = driver.FindElements(By.XPath("//*[contains(text(), '" + fName + "') and contains(text(), '" + lName + "')]/ancestor::tbody[1]"));

                    try
                    {
                        if (elements[0] != null)
                        {
                            scheduledDates[scheduledDatesIndex] = new System.DateTime(cYear, cMonth, cDay, 0, 0, 0, 0);
                            scheduledDatesIndex++;
                        }
                    }
                    catch (Exception)
                    {
                        tempElementArray = null;
                    }


                    tempJob = null;
                    foreach (IWebElement element in elements)
                    {


                        //for midnight and noon time
                        indexChange = 0;

                        //split job into array of String values
                        tempElementArray = element.Text.Split(new char[] { '\n' });

                        //analyze name
                        if (tempElementArray[0].Contains("CANCELLED"))
                        {
                            tempJobName = tempElementArray[0].Substring(0, tempElementArray[0].IndexOf("CANCELLED"));
                            tempCancelled = true;
                        }
                        else
                        {
                            tempJobName = tempElementArray[0];
                            tempCancelled = false;
                        }

                        //analyze datetime of job
                        tempStringDateTime = tempElementArray[1].Trim().Split(new char[] { ' ' });

                        //analyze time
                        if (tempStringDateTime[0].Equals("NOON"))
                        {
                            indexChange = 1;
                            tempHour = 12;
                            tempMin = 0;

                        }
                        else if (tempStringDateTime[0].Equals("MIDNIGHT"))
                        {
                            indexChange = 1;
                            tempHour = 0;
                            tempMin = 0;
                        }
                        else
                        {


                            tempHour = int.Parse(tempStringDateTime[0].Substring(0, tempStringDateTime[0].IndexOf(":")));
                            if (tempStringDateTime[1].Equals("PM"))
                            {
                                tempHour += 12;

                                //this is more for rare errors in code analyis it is barely used
                                if (tempHour >= 24)
                                {
                                    tempHour -= 24;
                                }
                            }
                            tempMin = int.Parse(tempStringDateTime[0].Substring(tempStringDateTime[0].IndexOf(":") + 1));
                        }

                        //get month int
                        if (tempStringDateTime[3 - indexChange].Equals("January"))
                            tempMonth = 1;
                        else if (tempStringDateTime[3 - indexChange].Equals("February"))
                            tempMonth = 2;
                        else if (tempStringDateTime[3 - indexChange].Equals("March"))
                            tempMonth = 3;
                        else if (tempStringDateTime[3 - indexChange].Equals("April"))
                            tempMonth = 4;
                        else if (tempStringDateTime[3 - indexChange].Equals("May"))
                            tempMonth = 5;
                        else if (tempStringDateTime[3 - indexChange].Equals("June"))
                            tempMonth = 6;
                        else if (tempStringDateTime[3 - indexChange].Equals("July"))
                            tempMonth = 7;
                        else if (tempStringDateTime[3 - indexChange].Equals("August"))
                            tempMonth = 8;
                        else if (tempStringDateTime[3 - indexChange].Equals("September"))
                            tempMonth = 9;
                        else if (tempStringDateTime[3 - indexChange].Equals("October"))
                            tempMonth = 10;
                        else if (tempStringDateTime[3 - indexChange].Equals("November"))
                            tempMonth = 11;
                        else if (tempStringDateTime[3 - indexChange].Equals("December"))
                            tempMonth = 12;
                        else
                            tempMonth = 0;

                        tempJob = new Job(tempJobName, new DateTime(int.Parse(tempStringDateTime[5 - indexChange]), tempMonth, int.Parse(tempStringDateTime[4 - indexChange].Substring(0, tempStringDateTime[4 - indexChange].IndexOf(","))), tempHour, tempMin, 0, 0), tempCancelled);



                        //grab workers
                        for (int tempElementArrayIndex = 3; tempElementArrayIndex < tempElementArray.Length; tempElementArrayIndex++)
                        {
                            tempElementArray[tempElementArrayIndex] = tempElementArray[tempElementArrayIndex].Trim();

                            //reuse for name and start and stop times
                            tempStringDateTime = tempElementArray[tempElementArrayIndex].Trim().Split(new char[] { ' ' });

                            tempPerson = new Person(tempStringDateTime[0], tempStringDateTime[1]);

                            indexChange = 0;

                            //analyze worker start time
                            if (tempStringDateTime[2].Equals("NOON"))
                            {
                                indexChange = 1;
                                tempHour = 12;
                                tempMin = 0;

                            }
                            else if (tempStringDateTime[2].Equals("MIDNIGHT"))
                            {
                                indexChange = 1;
                                tempHour = 0;
                                tempMin = 0;
                            }
                            else
                            {
                                tempHour = int.Parse(tempStringDateTime[2].Substring(0, tempStringDateTime[2].IndexOf(":")));
                                if (tempStringDateTime[3].Equals("PM"))
                                {
                                    tempHour += 12;

                                    //this is more for rare errors in code analyis it is barely used
                                    if (tempHour >= 24)
                                    {
                                        tempHour -= 24;
                                    }
                                }
                                tempMin = int.Parse(tempStringDateTime[2].Substring(tempStringDateTime[2].IndexOf(":") + 1));
                            }

                            if (tempMin == 0)
                                tempHour *= 100;
                            else if (tempMin < 10)
                                tempHour = tempHour * 100 + tempMin;
                            else
                                tempHour = tempHour * 100 + tempMin;

                            //set person attributes
                            tempPerson.setStartTime(tempHour);



                            //analyze worker start time
                            if (tempStringDateTime[4 - indexChange].Equals("NOON"))
                            {
                                tempHour = 12;
                                tempMin = 0;

                            }
                            else if (tempStringDateTime[4 - indexChange].Equals("MIDNIGHT"))
                            {
                                tempHour = 0;
                                tempMin = 0;
                            }
                            else
                            {
                                tempHour = int.Parse(tempStringDateTime[4 - indexChange].Substring(0, tempStringDateTime[4 - indexChange].IndexOf(":")));
                                if (tempStringDateTime[5 - indexChange].Equals("PM"))
                                {
                                    tempHour += 12;

                                    //this is more for rare errors in code analyis it is barely used
                                    if (tempHour >= 24)
                                    {
                                        tempHour -= 24;
                                    }
                                }
                                tempMin = int.Parse(tempStringDateTime[4 - indexChange].Substring(tempStringDateTime[4 - indexChange].IndexOf(":") + 1));
                            }

                            if (tempMin == 0)
                                tempHour *= 100;
                            else if (tempMin < 10)
                                tempHour = tempHour * 100 + tempMin;
                            else
                                tempHour = tempHour * 100 + tempMin;

                            //set person attributes
                            tempPerson.setEndTime(tempHour);

                            //add temp person to job
                            tempJob.addWorker(tempPerson);
                        }
                    }
                    //end job add for day
                    if (tempJob != null)
                        jobs.Add(tempJob);




                    cDay++;
                    int daysInMonth = System.DateTime.DaysInMonth(cYear, cMonth);
                    if (cDay > daysInMonth)
                    {
                        cDay = 1;
                        cMonth++;
                        if (cMonth > 12)
                        {
                            cMonth = 1;
                            cYear++;
                        }

                    }
                }

                //setup calendar
                calendar.BoldedDates = scheduledDates;

                driver.Quit(); //Quits chrome and CMD
            }
        }

        private void editUserDataBtn_Click(object sender, EventArgs e)
        {
            if (fNameBox.Text != "" && lNameBox.Text != "" && usernameBox.Text != "" && passwordBox.Text != "")
            {
                System.IO.File.WriteAllLines(@"userData.txt", new string[] { fNameBox.Text, lNameBox.Text, usernameBox.Text, passwordBox.Text });
                fiveDayInc.Visible = true;
                tenDayInc.Visible = true;
                twentyDayInc.Visible = true;
                thirtyDayInc.Visible = true;
                getDataBtn.Visible = true;
            }
            else
                MessageBox.Show("Please complete all fields before clicking save.");
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Matthew Bocharnikov and Greg Fairbanks\n\n" +
                "Enter your information in each textbox. Select 'Save.'\n" +
                "Select the number of days you'd like to search through and press 'Search.'\n" +
                "If you are not on the Virginia Tech campus wifi, you must enable the Pulse Secure VPN.\n" +
                "Do not exit any windows. They will close automatically when the process is complete.\n" +
                "Select a date on the calendar to view the shift for that day. Select the Excel or Notepad button to view all shifts.\n" +
                "This program is not a substitute for double checking your shifts. We are not responsible if the software malfunctions\n\n" +
                "Not for resale or redistribution without permission.");
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void fiveDayInc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AutomatedScheduler_Load(object sender, EventArgs e)
        {
            fNameBox.TabIndex = 1;
            lNameBox.TabIndex = 2;
            usernameBox.TabIndex = 3;
            passwordBox.TabIndex = 4;

            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.calendar, "Select a bolded day to view a shift.");

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            String finalText = "";
            List<Job> tempList = getJobsOnDay(calendar.SelectionStart);

            for (int i = 0; i < tempList.Count; i++)
            {
                finalText += tempList[i].getJobName() + Environment.NewLine + tempList[i].getJobDateTime().ToString("dddd, dd MMMM yyyy") + Environment.NewLine;
                foreach (Person w in tempList[i].getWorkers())
                {
                    finalText += w.getFirstName() + " " + w.getLastName() + " -> " + w.getFormattedTime(w.getStartTime()) + " - " + w.getFormattedTime(w.getEndTime()) + Environment.NewLine;

                }
                finalText += Environment.NewLine;
            }
            selectedShift.Text = finalText;

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void selectedShift_TextChanged(object sender, EventArgs e)
        {

        }

        private void Title_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StreamWriter cal = new StreamWriter(@"testCal.ics");
            cal.WriteLine("BEGIN:VCALENDAR");
            cal.WriteLine("METHOD:PUBLISH");
            cal.WriteLine("BEGIN:VEVENT");
            cal.WriteLine("LOCATION:");
            cal.WriteLine("STATUS:CONFIRMED");
            cal.WriteLine("SUMMARY:");
            cal.WriteLine("END:VEVENT");
            cal.WriteLine("END:VCALENDAR");
            cal.Close();
            /*
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FileStream fileStream = File.Create("currentEvent.ics"))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                int boxLocation = selectedShift.Find(lNameBox.Text);
                string lineText = selectedShift.Lines[selectedShift.GetLineFromCharIndex(boxLocation)];
                int length1 = fNameBox.TextLength;
                int length2 = lNameBox.TextLength;
                lineText = lineText.Substring(length1 + length2 + 2);
                string[] time = lineText.Split(' ');
                bool datechecker = false;
                int counter = 0;
                foreach (string time1 in time)
                {
                    time[counter] = time1.Replace(":", "");
                    counter++;
                }
                counter = 0;
                foreach (string time2 in time)
                {
                    if (time2 == "NOON")
                        time[counter] = "1200";
                    if (time2 == "MIDNIGHT")
                        time[counter] = "2400";
                    if (time2 == "PM")
                    {
                        if (time[counter - 1].Contains("12") == false)
                            time[counter - 1] = Convert.ToString(Convert.Toint(time[counter - 1]) + 1200);
                    }
                        counter++;
                }
                    if (time[1] == "MIDNIGHT" || time[2] == "MIDNIGHT" || time[3] == "MIDNIGHT" || (time[2] == "AM" && Convert.Toint(time[2]) < Convert.Toint(time[0])) || (time[3] == "AM" && Convert.Toint(time[2]) < Convert.Toint(time[0])))
                    {
                        datechecker = true;
                    }
                counter = 0;
                int counter2 = 0;
                string[] startandend = new string[2];
                foreach (string time3 in time)
                {
                    if (time3.Length < 4 && time3 != "PM" && time3 != "AM")
                    {
                        time[counter] = "0" + time3;
                        startandend[counter2] = time[counter];
                        counter2++;
                    }
                    else if(time3 != "PM" && time3 != "AM")
                    {
                        startandend[counter2] = time[counter];
                        counter2++;
                    }
                    counter++;
                }
                startandend[0] = Convert.ToString(Convert.Toint(startandend[0]) + 500);
                startandend[1] = Convert.ToString(Convert.Toint(startandend[1]) + 500);
                writer.WriteLine("BEGIN:VCALENDAR");
                writer.WriteLine("CALSCALE:GREGORIAN");
                writer.WriteLine("METHOD:PUBLISH");
                writer.WriteLine("X-WR-TIMEZONE:America/New_York");
                writer.WriteLine("BEGIN:VEVENT");
                writer.WriteLine("DTSTART:" + calendar.SelectionStart.ToString("yyyyMMdd") + "T" + startandend[0] + "00Z");
                if(datechecker == false)
                    writer.WriteLine("DTEND:" + calendar.SelectionStart.ToString("yyyyMMdd") + "T"  + startandend[1] + "00Z");
                else
                    writer.WriteLine("DTEND:" + Convert.ToString(Convert.Toint(calendar.SelectionStart.ToString("yyyyMMdd")) + 1) + "T" + startandend[1] + "00Z");
                writer.WriteLine("DESCRIPTION:" + selectedShift.Lines[1] + "\\n" + selectedShift.Lines[2] + "\\n" +selectedShift.Lines[3] + "\\n" + selectedShift.Lines[4]);
                writer.WriteLine("LOCATION:");
                writer.WriteLine("STATUS:CONFIRMED");
                writer.WriteLine("SUMMARY:" + selectedShift.Lines[0]);
                writer.WriteLine("END:VEVENT");
                writer.WriteLine("END:VCALENDAR");
            }*/
        }

        private void notepadView_Click(object sender, EventArgs e)
        {
            /*Process.Start(@"rawSchedule.txt");
            File.WriteAllLines(@"notepadExportSchedule.txt", formattedScheduleData);
            Console.WriteLine(tempScheduleAry.Length);
            */

            //create new array to hold data
            String[] jobsData = new String[jobs.Count];
            int jobIndex = 0;

            foreach (Job j in jobs)
            {
                jobsData[jobIndex] = j.getJobName() + Environment.NewLine + j.getJobDateTime().ToString("dddd, dd MMMM yyyy") + Environment.NewLine;
                foreach (Person w in j.getWorkers())
                {
                    jobsData[jobIndex] += w.getFirstName() + " " + w.getLastName() + " -> " + w.getFormattedTime(w.getStartTime()) + " - " + w.getFormattedTime(w.getEndTime()) + Environment.NewLine;
                }
                jobsData[jobIndex] += Environment.NewLine;
                jobIndex++;
            }
            File.WriteAllLines(@"notepadExportSchedule.txt", jobsData);
            Process.Start(@"notepadExportSchedule.txt");
        }

        private void allShifts_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Test";
                oSheet.Cells[1, 2] = "Shift Date";
                oSheet.Cells[1, 3] = "First Name";
                oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";

                saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = saNames;

                //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("test.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
            }
            catch
            {

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private List<Job> getJobsOnDay(DateTime date)
        {
            List<Job> tempJobList = new List<Job>();


            for(int i = 0; i < jobs.Count; i++)
            {
                if (jobs[i].getJobDateTime().Day == date.Day && jobs[i].getJobDateTime().Month == date.Month && jobs[i].getJobDateTime().Year == date.Year)
                    tempJobList.Add(jobs[i]);
            }

            return tempJobList;

        }
    }
}
