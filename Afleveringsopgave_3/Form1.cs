using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afleveringsopgave_3
{
    public partial class Form1 : Form
    {
        //Variables to be shared between methods
        public double TotalSale;
        public double TotalCost;
        public double TotalIncome;

        public Form1()
        {
            InitializeComponent();
            UpdateTextPosition();
            ResetBoxes();
        }

        //Event for adding the entered game day data to the list
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Constants for calculations
                const double SaleFromShopvisitsPercent = 0.20;
                const int TicketPriceAvg = 175;
                const int SaleBeverageAvg = 70;
                const int SaleSportsGoodsAvg = 245;
                const int SubscriptionPrice6Months = 999;
                const int TvRightsPerChannel = 1000000;
                const double TotalCostsPercent = 0.64;

                //Input variables for calculations
                int ticketsSold = Convert.ToInt32(TicketsSoldTextBox.Text);
                int channelsCoverage = Convert.ToInt32(TvCoverageBox.Text);
                int visitsSportsshop = Convert.ToInt32(ShopVisitorsBox.Text);
                int motionSubscription = Convert.ToInt32(FitnessSubBox.Text);
                double attendenceShowup = Convert.ToDouble(AttendanceBox.Text) / 100;

                //Line item calculations
                double saleFromShop = ((double)visitsSportsshop * SaleFromShopvisitsPercent) * (double)SaleSportsGoodsAvg;
                int ticketSaleIncome = TicketPriceAvg * ticketsSold;
                int subscriptionIncome = (SubscriptionPrice6Months * 2) * motionSubscription;
                int TvRightsIncome = TvRightsPerChannel * channelsCoverage;
                double beverageIncome = ((double)ticketsSold * attendenceShowup) * (double)SaleBeverageAvg;

                //Calculate the totals for a given game day
                double Sale = ticketSaleIncome + subscriptionIncome + TvRightsIncome + (double)beverageIncome + (double)saleFromShop;
                double costs = Sale * TotalCostsPercent;
                double income = Sale - costs;

                //Adding to the overall total figures instantiated in the beginning of the code
                TotalSale += Sale;
                TotalCost += costs;
                TotalIncome += income;

                //Adding the game day data to the list view
                ListViewItem newItem = new ListViewItem(CalendarBox.Text);
                newItem.SubItems.Add(Sale.ToString("C"));
                newItem.SubItems.Add(costs.ToString("C"));
                newItem.SubItems.Add(income.ToString("C"));
                listView1.Items.Add(newItem);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Event for stop adding game day data and calculate the totals
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            TotalSaleTextBox.Text = TotalSale.ToString("C");
            CostBox.Text = TotalCost.ToString("C");
            IncomeBox.Text = TotalIncome.ToString("C");
        }

        //Event for reset button which is calling the ResetBoxes method
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetBoxes();
        }

        //Code to align the title name center of the form
        private void UpdateTextPosition()
        {
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }

            this.Text = tmp + this.Text.Trim();
        }

        //Method for reset the input boxes and put 0 in them (used on form load and for reset button)
        private void ResetBoxes()
        {
            this.TicketsSoldTextBox.Text = "0";
            this.TvCoverageBox.Text = "0";
            this.ShopVisitorsBox.Text = "0";
            this.FitnessSubBox.Text = "0";
            AttendanceBox.Text = "0";
        }
    }
}
