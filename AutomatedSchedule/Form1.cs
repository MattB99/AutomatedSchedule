using OpenQA.Selenium;
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
            jobs.Clear();
            selectedShift.Text = "";
            calendar.BoldedDates = null;
            if (fiveDayInc.Checked == false && tenDayInc.Checked == false && twentyDayInc.Checked == false && thirtyDayInc.Checked == false)
            {
                MessageBox.Show("Please select the number of days you'd like to search through.");
            }
            else
            {
                excelView.Visible = true;
                notepadView.Visible = true;
                label5.Visible = true;
                reset.Visible = true;
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
                        //end job add for day
                        if (tempJob != null)
                            jobs.Add(tempJob);
                    }
                    





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
                jobs.Clear();
            }
            else
                MessageBox.Show("Please complete all fields before clicking save.");
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Matthew Bocharnikov and Greg Fairbanks\n\n" +
                "For distribution to Virginia Tech Production Services employees only.\n\n" +
                "Not for resale.");
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
            if(System.IO.File.Exists(@"dm") == true)
            {
                if (System.IO.File.ReadAllText(@"dm") == "dark")
                {
                    DarkMode.Checked = true;
                }
            }
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
            List<Job> tempList = getJobsOnDay(calendar.SelectionStart);
            //Convert.ToString(calendar.SelectionStart);
            int calNum = 1;
            string newstarttime, newendtime;
            string description = "";
            string path = @"Schedule Export";
            System.IO.Directory.CreateDirectory(path);
            for (int i = 0; i < tempList.Count; i++)
            {
                string FileName = tempList[i].getJobDateTime().ToString("yyyy-MM-dd - ");
                StreamWriter cal = new StreamWriter("Schedule Export/" + FileName + "Event#" + calNum + ".ics");
                cal.WriteLine("BEGIN:VCALENDAR");
                cal.WriteLine("METHOD:PUBLISH");
                cal.WriteLine("BEGIN:VEVENT");
                cal.WriteLine("LOCATION:");
                foreach (Person w in tempList[i].getWorkers())
                {
                  description = description + (w.getFirstName() + " " + w.getLastName() + " -> " + w.getFormattedTime(w.getStartTime()) + " - " + w.getFormattedTime(w.getEndTime()) + "\\n");

                    if (w.getLastName() == lNameBox.Text)
                    {
                        if (Convert.ToString(w.getStartTime()).Length < 4)
                        {
                            newstarttime = "0" + Convert.ToString(w.getStartTime());
                        }
                        else
                        {
                                newstarttime = Convert.ToString(w.getStartTime());
                        }
                        if (Convert.ToString(w.getEndTime()).Length < 4)
                        {
                            newendtime = "0" + Convert.ToString(w.getEndTime());
                        }
                        else
                        {
                            newendtime = Convert.ToString(w.getEndTime());
                        }
                        cal.WriteLine("DTSTART;TZID=America/New_York:" + tempList[i].getJobDateTime().ToString("yyyyMMdd") + "T" + newstarttime + "00Z");
                        if(w.getEndTime() < w.getStartTime() || w.getEndTime() > 2400)
                        cal.WriteLine("DTEND;TZID=America/New_York:" + (Convert.ToInt32(tempList[i].getJobDateTime().ToString("yyyyMMdd"))+1) + "T" + newendtime + "00Z");
                        else
                        cal.WriteLine("DTEND;TZID=America/New_York:" + tempList[i].getJobDateTime().ToString("yyyyMMdd") + "T" + newendtime + "00Z");
                    }
                }
                cal.WriteLine("DESCRIPTION:" + description);
                cal.WriteLine("STATUS:CONFIRMED");
                cal.WriteLine("SUMMARY:" + tempList[i].getJobName());
                cal.WriteLine("END:VEVENT");
                cal.WriteLine("END:VCALENDAR");
                cal.Close();
                calNum += 1;
            }
            Process.Start("Schedule Export");

        }

        private void notepadView_Click(object sender, EventArgs e)
        {
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
            String[] jobsData = new String[jobs.Count];
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
                oSheet.Cells[1, 1] = "Virginia Tech Production Services";
                oSheet.Cells[2, 1] = "Shift Name";
                oSheet.Cells[2, 2] = "Shift Date";
                oSheet.Cells[2, 3] = "Shift Staff";
                oSheet.Cells[2, 4] = "Time Scheduled";
                oSheet.Cells[1, 4] = "Number of Shifts: " + jobs.Count;
                int row = 4;
                foreach (Job j in jobs)
                {
                    oSheet.Cells[row, 1] = j.getJobName();
                    oSheet.Cells[row, 2] = j.getJobDateTime().ToString("dddd, dd MMMM yyyy");
                    foreach (Person w in j.getWorkers())
                    {
                        oSheet.Cells[row, 3] = w.getFirstName() + " " + w.getLastName();
                        oSheet.Cells[row, 4] = w.getFormattedTime(w.getStartTime()) + " - " + w.getFormattedTime(w.getEndTime());
                        row += 1;
                    }
                    row += 1;
                }


                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A2", "D2").Font.Underline = true;
                oSheet.get_Range("A2", "D2").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "J1");
                oRng.EntireColumn.AutoFit();
                oXL.Visible = true;
                oXL.UserControl = true;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Font defaultFont = SystemFonts.DefaultFont;
            Color darkdarkGray = ControlPaint.Dark(Color.Gray);
            if (DarkMode.Checked == true)
            {
                this.BackColor = darkdarkGray;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                Title.ForeColor = Color.White;
                DarkMode.ForeColor = Color.White;
                fNameBox.ForeColor = Color.White;
                fNameBox.BackColor = Color.LightSlateGray;
                lNameBox.ForeColor = Color.White;
                lNameBox.BackColor = Color.LightSlateGray;
                usernameBox.ForeColor = Color.White;
                usernameBox.BackColor = Color.LightSlateGray;
                passwordBox.ForeColor = Color.White;
                passwordBox.BackColor = Color.LightSlateGray;
                selectedShift.BackColor = Color.SlateGray;
                selectedShift.ForeColor = Color.White;
                fiveDayInc.ForeColor = Color.White;
                tenDayInc.ForeColor = Color.White;
                twentyDayInc.ForeColor = Color.White;
                thirtyDayInc.ForeColor = Color.White;
                About.BackColor = Color.Gray;
                About.ForeColor = Color.White;
                notepadView.BackColor = Color.Gray;
                notepadView.ForeColor = Color.White;
                excelView.BackColor = Color.Gray;
                excelView.ForeColor = Color.White;
                reset.BackColor = Color.Gray;
                reset.ForeColor = Color.White;
                reset.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                About.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                fiveDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                tenDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                twentyDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                thirtyDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                button1.BackColor = Color.Gray;
                button1.ForeColor = Color.White;
                use.BackColor = Color.Gray;
                use.ForeColor = Color.White;
                button1.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                fNameBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                lNameBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                usernameBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                passwordBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
                selectedShift.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);
            }
            else
            {
                this.BackColor = Control.DefaultBackColor;
                label1.ForeColor = Control.DefaultForeColor;
                label2.ForeColor = Control.DefaultForeColor;
                label3.ForeColor = Control.DefaultForeColor;
                label4.ForeColor = Control.DefaultForeColor;
                label5.ForeColor = Control.DefaultForeColor;
                label6.ForeColor = Control.DefaultForeColor;
                label7.ForeColor = Control.DefaultForeColor;
                Title.ForeColor = Control.DefaultForeColor;
                DarkMode.ForeColor = Control.DefaultForeColor;
                fNameBox.ForeColor = Control.DefaultForeColor;
                fNameBox.BackColor = Control.DefaultBackColor;
                lNameBox.ForeColor = Control.DefaultForeColor;
                lNameBox.BackColor = Control.DefaultBackColor;
                usernameBox.ForeColor = Control.DefaultForeColor;
                usernameBox.BackColor = Control.DefaultBackColor;
                passwordBox.ForeColor = Control.DefaultForeColor;
                passwordBox.BackColor = Control.DefaultBackColor;
                selectedShift.BackColor = Control.DefaultBackColor;
                selectedShift.ForeColor = Control.DefaultForeColor;
                fiveDayInc.ForeColor = Control.DefaultForeColor;
                tenDayInc.ForeColor = Control.DefaultForeColor;
                twentyDayInc.ForeColor = Control.DefaultForeColor;
                thirtyDayInc.ForeColor = Control.DefaultForeColor;
                About.BackColor = Control.DefaultBackColor;
                About.ForeColor = Control.DefaultForeColor;
                notepadView.BackColor = Control.DefaultBackColor;
                notepadView.ForeColor = Control.DefaultForeColor;
                excelView.BackColor = Control.DefaultBackColor;
                excelView.ForeColor = Control.DefaultForeColor;
                reset.BackColor = Control.DefaultBackColor;
                reset.ForeColor = Control.DefaultForeColor;
                reset.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                About.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                fiveDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                tenDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                twentyDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                thirtyDayInc.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                button1.BackColor = Control.DefaultBackColor;
                button1.ForeColor = Control.DefaultForeColor;
                use.BackColor = Control.DefaultBackColor;
                use.ForeColor = Control.DefaultForeColor;
                button1.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                fNameBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                lNameBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                usernameBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                passwordBox.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
                selectedShift.Font = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Regular);
            }
            if (DarkMode.Checked == true)
            {
                System.IO.File.Delete(@"dm");
                System.IO.File.WriteAllText(@"dm", "dark");
            }
            else if(DarkMode.Checked == false)
            {
                System.IO.File.Delete(@"dm");
                System.IO.File.WriteAllText(@"dm", "light");
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            fNameBox.Text = "";
            lNameBox.Text = "";
            usernameBox.Text = "";
            passwordBox.Text = "";
            System.IO.File.WriteAllLines(@"userData.txt", new string[] { fNameBox.Text, lNameBox.Text, usernameBox.Text, passwordBox.Text });
            fiveDayInc.Visible = false;
            tenDayInc.Visible = false;
            twentyDayInc.Visible = false;
            thirtyDayInc.Visible = false;
            getDataBtn.Visible = false;

        }

        private void use_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Connect to Virginia Tech WiFi or enable the Pulse Secure VPN.\n\n" +
                "2. Fill in all blanks with information and press 'Save.'\n\n" +
                "3. Enter how many days you'd like to search through and press 'Search.'\n\n" +
                "4. Once all Google Chrome and Command Prompt windows close, select a bold day on the calendar to view a shift.\n\n" +
                "You can create a calendar file from any specified shift, or you can export the entire schedule to Excel or Notepad.\n\n" +
                "If you would like to save the Excel or Notepad file, select 'Save As.\n\n'" +
                "The calendar output file is intended for Google Calendar. \n\n" +
                "Select the 'dark mode' checkbox to enable dark mode.\n\n\n\n"+
                "This program is not a substitute for double checking your shifts. We are not responsible if the software malfunctions.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
        "Dedicated to Jeff Camp\n2013-2019\n\nPress OK to pay respects", "Dedication", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk
    ) == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=9g3--WYH8SY");
            }
        }
    }
}
