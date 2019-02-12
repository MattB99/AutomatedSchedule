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



            driver.Url = "http://172.21.20.41/cepdotnet/CEPHome.aspx?day=11&month=2&year=2019";

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//*[contains(text(), '" + fName + "') and contains(text(), '" + lName + "')]/ancestor::tbody[1]"));

            foreach (IWebElement element in elements)
            {
                //THIS WRITES ON ONE FUCKING LINE
                System.IO.File.WriteAllText(@"rawSchedule.txt", element.Text);
            }
        }

        private void editUserDataBtn_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllLines(@"userData.txt", new string[]{fNameBox.Text, lNameBox.Text, usernameBox.Text, passwordBox.Text});
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Matthew Bocharnikov and Greg Fairbanks\nNot for resale or redistribution without permission.");
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
