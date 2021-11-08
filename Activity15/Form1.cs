using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity15
{
    public partial class Form1 : Form
    {

        List<int> leapYears = new List<int>(); // A List of integers that will contain all of the leap years within the range of available years

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Sets the comboboxes to be editable ony through their dropdown lists, as opposed to by typing. Made with help from Microsoft documentation (n.d.)
            Year.DropDownStyle = ComboBoxStyle.DropDownList;
            Month.DropDownStyle = ComboBoxStyle.DropDownList;
            Day.DropDownStyle = ComboBoxStyle.DropDownList;
            Color.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int i = 2021; i > 1910; i--) // This for loop populates the Year combobox control with every year between 2021 and 1910, iterating in reverse so that the most recent years will be on top. It also populates the leapYears List object with every leap year in that range
            {
                Year.Items.Add(i); // Adds the current number to the Items of the Years combobox

                // This logical sequence determines if the current number is a leap year and, if so, adds it to the leapYears List. Made with help from Microsoft documentation (2021)
                if (i % 4 == 0) // If the number is evenly divisible by four, then it is a candidate to be a leap year
                {
                    if(i % 100 != 0) // If the potential leap year is not divisible by 100 then it is a leap year and is added to the leapYears List
                    {
                        leapYears.Add(i);
                    }
                    else // If the potential leap year is divisble by 100 then it must also be divisible by 400 to be a leap year
                    {
                        if(i % 400 == 0) // If this is the case, it is added to the leapYears List
                        {
                            leapYears.Add(i);
                        }
                    }
                }
            }
        }

        // This code runs when the Calculate button is clicked. It parses information from the comboboxes into numeric data types and uses them to calculate the users lucky number, which is then displayed in a second form
        private void Calculate_Click(object sender, EventArgs e)
        {
            try
            {
                int day = int.Parse(Day.Text); // An integer variable that contains the day entered by the user
                int month = Month.SelectedIndex + 1; // An integer variable that contains the numerical value of the month entered by the user
                int lastDigitOfYear = int.Parse(Year.Text[3].ToString()); // An integer variable that contains the last digit of the year entered by the user
                int color = Color.SelectedIndex; // An integer variable that contains the numerical value of the color chosen by the user, as determined by its index in the Color combobox
                double luckyNumber; // A double variable that will contain the result of calculating the user's lucky number
                ResultForm form2; // A ResultForm object that will be used to display the results of the calculation

                luckyNumber = Math.Round((double)(day + lastDigitOfYear) / (color + month) * 10); // The lucky number is calculated by dividing the sum of the day and the last digit of the year entered by the sum of the numerical values of the color and month entered, multiplying by 100 and rounding
                form2 = new ResultForm(luckyNumber); // The ResultForm is instantiated with the luckyNumber variable as an argument. Made with help from Microsoft documentation (n.d.)
                form2.Visible = true; // The ResultForm is made visible
            }
            catch(Exception ex) // If an exception occurs while processing the data entered by the user, an error message is shown
            {
                MessageBox.Show(ex.Message);
            }
        }

        // This method populates the Items collection of the Day combobox based on the content of the Year and Month boxes
        private void PopulateDayBox()
        {
            Day.Items.Clear(); // Clears the Items of the Day box
            string[] monthsWith30Days; // A string array that will contain the names of the months that have thirty days
            string[] monthsWith31Days; // A string array that will contain the names of the months that have thirty-one days
            if(Year.Text != "" && Month.Text != "") // This code runs if both the Year and Month boxes contain data from the user
            {
                monthsWith30Days = new string[] { "April", "June", "September", "November" }; // The monthsWtih30Days array is initialized with the names of the months that have thirty days
                monthsWith31Days = new string[] { "January", "March", "May", "July", "August", "October", "December" }; // The monthsWtih31Days array is initialized with the names of the months that have thirty-one days
                if (monthsWith30Days.Contains(Month.Text)) // If the text of the Month box is within the monthsWith30Days array, the Items collection for the Day box is populated with all numbers between one and thirty
                {
                    for (int i = 1; i < 31; i++) // This loop iterates for every number between one and thirty, adding each number to the Items collection of the day box
                    {
                        Day.Items.Add(i);
                    }
                }
                else if (monthsWith31Days.Contains(Month.Text)) // If the text of the Month box is within the monthsWith31Days array, the Items collection for the Day box is populated with all numbers between one and thirty-one
                {
                    for (int i = 1; i <= 31; i++) // This loop iterates for every number between one and thirty-one, adding each number to the Items collection of the day box
                    {
                        Day.Items.Add(i);
                    }
                }
                else if (Month.Text == "February") // If the text of the Month box is the string February, then the Items collection of the Day box is populated differently depending on the text of the Year box
                {
                    if (leapYears.Contains(int.Parse(Year.Text))) // If the data entered in the Year box is a leap year, then the Items collection of the Day box is populated with all numbers between one and twenty-nine
                    {
                        for (int i = 1; i < 30; i++) // This loop iterates for every number between one and twenty-nine, adding each number to the Items collection of the day box
                        {
                            Day.Items.Add(i);
                        }
                    }
                    else  // If the data entered in the Year box is not a leap year, then the Items collection of the Day box is populated with all numbers between one and twenty-eight
                    {
                        for (int i = 1; i < 29; i++)
                        {
                            Day.Items.Add(i); // This loop iterates for every number between one and twenty-eight, adding each number to the Items collection of the day box
                        }
                    }
                }
            }
        }

        // This code runs when the SelectedIndex of the Year box is changed. It calls the PopulateDayBox method
        private void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDayBox();
        }

        // This code runs when the SelectedIndex of the Year box is changed. It calls the PopulateDayBox method
        private void Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDayBox();
        }
    }
}

// References:
// Microsoft. (2021, April 7). Method to determine whether a year is a leap year. https://docs.microsoft.com/en-us/office/troubleshoot/excel/determine-a-leap-year
// Microsoft. (n.d.). ComboBox Class. https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.combobox?view=windowsdesktop-5.0
// Microsoft. (n.d.). Form.Load Event. https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.load?view=windowsdesktop-5.0