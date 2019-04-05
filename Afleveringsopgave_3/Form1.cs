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
        public Form1()
        {
            InitializeComponent();
            UpdateTextPosition();
            ResetBoxes();
        }

        private void SaveButton_Click(object sender, EventArgs e)
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
            double totalSale = ticketSaleIncome + subscriptionIncome + TvRightsIncome + (double)beverageIncome + (double)saleFromShop;
            double costs = totalSale * TotalCostsPercent;
            double income = totalSale - costs;

            ListViewItem newList = new ListViewItem(CalendarBox.Text);
            newList.SubItems.Add(totalSale.ToString("C"));
            newList.SubItems.Add(costs.ToString("C"));
            newList.SubItems.Add(income.ToString("C"));
            listView1.Items.Add(newList);
        }

        //Event for stop adding game day data and calculate the totals
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            double sale = 0;
            for (int x = 0; x < listView1.Items.Count; ++x)
            {
                sale = double.Parse(listView1.Items[x].SubItems[2].Text);
            }
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
