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

namespace AutomatedSchedule
{
    public partial class AutomatedScheduler : Form
    {
        String[] lines, formattedScheduleData, tempScheduleAry;
        int[] elementNumPerDate;
        int elementCount, currElementCount;
        public AutomatedScheduler()
        {
            InitializeComponent();
            lines = System.IO.File.ReadAllLines(@"userData.txt");
            if(lines[0] != null)
                fNameBox.Text = lines[0];
            if (lines[1] != null)
                lNameBox.Text = lines[1];
            if (lines[2] != null)
                usernameBox.Text = lines[2];
            if (lines[3] != null)
                passwordBox.Text = lines[3];
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

                File.WriteAllText(@"rawSchedule.txt", "");
                ReadOnlyCollection<IWebElement> elements;
                elementCount = 0;

                int cMonth = DateTime.Now.Month, cDay = DateTime.Now.Day, cYear = DateTime.Now.Year, dateCounter = 0, scheduledDatesIndex = 0; ;
                if (fiveDayInc.Checked)
                    dateCounter = 5;
                else if (tenDayInc.Checked)
                    dateCounter = 10;
                else if (twentyDayInc.Checked)
                    dateCounter = 20;
                else if (thirtyDayInc.Checked)
                    dateCounter = 30;

                elementNumPerDate = new int[dateCounter];
                System.DateTime[] scheduledDates = new System.DateTime[dateCounter];

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
                        //do nothing when no elements
                    }

                    currElementCount = 0;
                    foreach (IWebElement element in elements)
                    {
                        File.AppendAllText(@"rawSchedule.txt", element.Text + Environment.NewLine + "*" + Environment.NewLine);
                        elementCount++;
                        currElementCount++;
                    }

                    try
                    {
                        elementNumPerDate[scheduledDatesIndex - 1] = currElementCount;
                    }
                    catch (Exception)
                    {
                        //don't store any values if exception
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

                //read in from raw data and input into calendar
                formattedScheduleData = new String[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    //populate each value
                    formattedScheduleData[i] = "";
                }

                tempScheduleAry = System.IO.File.ReadAllLines(@"rawSchedule.txt");
                formattedScheduleData = new string[tempScheduleAry.Length];
                int formattedIndex = 0;
                for (int tempIndex = 0; tempIndex < tempScheduleAry.Length; tempIndex++)
                {
                    if (tempScheduleAry[tempIndex].Equals("*"))
                    {
                        formattedScheduleData[formattedIndex] = "\n";
                        formattedIndex++;
                        formattedScheduleData[formattedIndex] = "\n";
                        formattedIndex++;
                    }
                    else if (!tempScheduleAry[tempIndex].Contains("Scheduled Employees Start Time End Time"))
                    {
                        formattedScheduleData[formattedIndex] = tempScheduleAry[tempIndex].Trim();
                        formattedIndex++;
                    }

                }

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
                MessageBox.Show("Please complete all fields before clicking enter.");
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Matthew Bocharnikov and Greg Fairbanks\n\n" +
                "Enter your information in each textbox. Select 'Save.'\n" +
                "Select the number of days you'd like to search through and press 'Search.'\n" +
                "If you are not on the Virginia Tech campus wifi, you must enable the Pulse Secure VPN.\n" +
                "Do not exit any windows. They will close automatically when the process is complete.\n" +
                "Select a date on the calendar to view the shift for that day. Select the Excel or Notepad button to view all shifts.\n\n" +
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

            try
            {
                //makes array of maximum length needed
                String[] tempAry = new String[formattedScheduleData.Length];
                String dateString = calendar.SelectionRange.Start.ToLongDateString();
                String dayString = dateString.Substring(0, dateString.IndexOf(','));
                String otherDateString = dateString.Substring(dateString.IndexOf(' ') + 1);
                for (int i = 1; i < formattedScheduleData.Length; i++)
                {
                    if (formattedScheduleData[i].Contains(otherDateString))
                    {
                        int k = i - 1;
                        while(!formattedScheduleData[k].Equals("\n"))
                        {
                            tempAry[k] = formattedScheduleData[k];
                            k++;
                        }
                        tempAry[k] = formattedScheduleData[k];
                        k++;
                        tempAry[k] = formattedScheduleData[k];
                        i = k;
                    }
                }
                //String finalString = string.Join("\n", tempAry);
                String finalString = String.Join("\n", tempAry.Where(s => !string.IsNullOrEmpty(s)));


                int indexOfLastNewLine = 0;
                int lastI = 0;
                for(int i = 0; i < finalString.Length; i++)
                {
                    if (!finalString.Substring(i, i + 1).Equals("\n"))
                    {
                        indexOfLastNewLine = i;
                        break;
                    }
                    lastI = i;

                }
                if(lastI == finalString.Length)
                    selectedShift.Text = "NO SHIFTS ON THIS DAY";
                else
                    selectedShift.Text = finalString.Substring(indexOfLastNewLine);
            }
            
            catch(Exception)
            {
                selectedShift.Text = "NO SHIFTS ON THIS DAY";
            }
            
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void selectedShift_TextChanged(object sender, EventArgs e)
        {

        }

        private void notepadView_Click(object sender, EventArgs e)
        {
            
            File.WriteAllLines(@"notepadExportSchedule.txt", formattedScheduleData);
            Console.WriteLine(tempScheduleAry.Length);

        }

        private void allShifts_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
