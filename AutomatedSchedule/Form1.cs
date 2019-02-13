using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatedSchedule
{
    public partial class AutomatedScheduler : Form
    {
        public AutomatedScheduler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            excelView.Visible = true;
            notepadView.Visible = true;
            label5.Visible = true;
            string[] lines = System.IO.File.ReadAllLines(@"userData.txt");
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

            int cMonth = DateTime.Now.Month, cDay = DateTime.Now.Day, cYear = DateTime.Now.Year;
            


            driver.Url = "http://172.21.20.41/cepdotnet/CEPHome.aspx?day=12&month=2&year=2019";

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//*[contains(text(), '" + fName + "') and contains(text(), '" + lName + "')]/ancestor::tbody[1]"));

            foreach (IWebElement element in elements)
            {
                //THIS WRITES ON ONE FUCKING LINE
                System.IO.File.WriteAllText(@"rawSchedule.txt", element.Text);
            }
            /*    //BOLDS DATES ON CALENDAR
            calendar.BoldedDates =
new System.DateTime[] {new System.DateTime(2019, 2, 15, 0, 0, 0, 0),
                       new System.DateTime(2019, 2, 18, 0, 0, 0, 0)
                       new System.DateTime(2019, 2, 18, 0, 0, 0, 0)};
            */
            driver.Quit(); //Quits chrome and CMD
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

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void selectedShift_TextChanged(object sender, EventArgs e)
        {

        }

        private void allShifts_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
