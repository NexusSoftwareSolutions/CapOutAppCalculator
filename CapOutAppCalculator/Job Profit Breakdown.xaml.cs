using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapOutAppCalculator
{
	/// <summary>
	/// Interaction logic for Job_Profit_Breakdown.xaml
	/// </summary>
	public partial class Job_Profit_Breakdown : Page
	{
		public Job_Profit_Breakdown()
		{
			InitializeComponent();
		}
		#region Math
		private double ChecksTotal()
		{

			double chk = 0;


			double tempdbl = 0;
			{
				if (double.TryParse(DeductibleCheckAmountText.Value.ToString(), out tempdbl))
					chk += tempdbl;
				if (double.TryParse(FirstCheckAmountText.Value.ToString(), out tempdbl))
					chk += tempdbl;
				if (double.TryParse(DepreciationAmountText.Value.ToString(), out tempdbl))
					chk += tempdbl;
				if (double.TryParse(SupplementCheckAmountText.Value.ToString(), out tempdbl))
					chk += tempdbl;
				if (double.TryParse(UpgradeCheckAmountText.Value.ToString(), out tempdbl))
					chk += tempdbl;
			}


			return chk;



		}

		private double MiscCost()
		{
			double exp = 0;
			double tempdbl = 0;

			{
				if (double.TryParse(ReceiptAmount1Text.Value.ToString(), out tempdbl))
					exp += tempdbl;
				if (double.TryParse(ReceiptAmount2Text.Value.ToString(), out tempdbl))
					exp += tempdbl;
				if (double.TryParse(ReceiptAmount3Text.Value.ToString(), out tempdbl))
					exp += tempdbl;
				if (double.TryParse(ReceiptAmount4Text.Value.ToString(), out tempdbl))
					exp += tempdbl;
				if (double.TryParse(ReceiptAmount5Text.Value.ToString(), out tempdbl))
					exp += tempdbl;
			}
			return exp;



		}

		private double FigureSplit(string salessplit = "50")
		{
			double split = 0;

			double tempdbl = 0;
			int isplit = 0;


			if (double.TryParse(ProfitAmountText.Value.ToString(), out tempdbl))
				if (int.TryParse(salessplit, out isplit))
					split = tempdbl * isplit;

			return split;



		}


		private double FigureJobMaterialCost(string takeback = "0")
		{

			double materialcost = 0;

			double takebacktemp = 0;

			if (double.TryParse(MaterialBillAmountText.Value.ToString(), out materialcost))
				double.TryParse(takeback, out takebacktemp);

			return materialcost += takebacktemp;


		}


		private double FinalPaymentToSalesperson(string draw = "500")
		{

			double drawtemp = 0;
			double tempSplitAmount = 0;

			if (double.TryParse(SalespersonSplitText.Text, out tempSplitAmount))
				double.TryParse(draw, out drawtemp);


			return (TotalProfit() * ((100 - tempSplitAmount) * .01)) - drawtemp;


		}

		private double FinalPaymentToMRN(double multiplyerpercent = 2.5)  //2.5% = .025
		{
			double tempSalespersonSplit = 0;
			if (SalespersonName.Text == string.Empty)
			{
				RecruiterText.Text = string.Empty;
				tempSalespersonSplit = 0;

				return TotalProfit();
			}

			if (RecruiterText.Text != string.Empty)
			{
				StringBuilder sb = new StringBuilder();
				sb.Clear();
				if (double.TryParse(SalespersonSplitText.Text, out tempSalespersonSplit))
					if (SalespersonName.Text != string.Empty)
					{
						string tempstring = RecruiterText.Text;
						if (tempstring.Contains(" "))
							sb.Append(tempstring.Substring(0, tempstring.IndexOf(" ")));

						else sb.Append(RecruiterText.Text);
						sb.Append(" -- $");
						sb.Append((TotalProfit() * ((100 - tempSalespersonSplit) * .01)) * (multiplyerpercent * .01));
						tempstring = sb.ToString();

						if (tempstring.Length - tempstring.IndexOf(".") > 2)
							RecruiterText.Text = tempstring.Substring(0, tempstring.IndexOf(".") + 3);
					}
			}
			if (tempSalespersonSplit > 0)
				return (TotalProfit() * ((100 - tempSalespersonSplit) * .01)) - (TotalProfit() * ((100 - tempSalespersonSplit) * .01)) * (multiplyerpercent * .01);

			return (TotalProfit() * ((100 - tempSalespersonSplit) * .01)) - (TotalProfit() * ((100 - tempSalespersonSplit) * .01));

		}


		private double FigureScopeDiff()
		{
			double result = 0;
			double tempOrigScope = 0;
			double tempFinalScope = 0;

			if (double.TryParse(OriginalScopeAmountText.Value.ToString(), out tempOrigScope) && double.TryParse(FinalScopeAmount.Value.ToString(), out tempFinalScope))
				result = tempFinalScope - tempOrigScope;

			return result;




		}


		private double FigureCostPerSquare(double totalExpense)
		{
			double result = 0;
			double tempNoOfSQ = 0;

			if (double.TryParse(NumberOfSquaresAmountText.Text, out tempNoOfSQ) && totalExpense < 0)
				result = totalExpense / tempNoOfSQ;

			return result;


		}

		private double FigureProfitPerSquare(double totalProfit)
		{
			double result = 0;
			double tempNoOfSQ = 0;

			if (double.TryParse(NumberOfSquaresAmountText.Text, out tempNoOfSQ) && totalProfit > 0)
				result = totalProfit / tempNoOfSQ;

			return result;


		}

		private double TotalExpense()
		{

			double TotalExpense = 0;
			double TempVariable = 0;

			if (double.TryParse(RoofLaborBillAmountText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(InteriorBillAmountText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(ExteriorBillAmountText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(GutterBillAmountText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(MiscBillAmount.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(KnockerReferralAmountText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(OverheadAmountText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;
			if (double.TryParse(RoofMaterialExpenseSubtotalText.Value.ToString(), out TempVariable))
				TotalExpense += TempVariable;


			return TotalExpense *= (-1);


		}


		private double FigureOverhead(string overheadpercent)
		{
			int TempVariable = 0;

			if (ChecksTotal() > 0 && int.TryParse(overheadpercent, out TempVariable))
				if (TempVariable < 100)
					return ChecksTotal() * (TempVariable * .01);

			return 0;

		}

		private double TotalProfit()
		{


			if (ChecksTotal() > 0 && TotalExpense() < 0)
				return ChecksTotal() + TotalExpense();

			return 0;

		}

		private double FigureRoofersBill(string strNOS, bool bNC = false, bool newmath = true, double dumpfeemultiplyer = (6.25))
		{
			double dumpsterfee = 0;

			double roofmultiplier = 0;

			double NOS = 0;
			if (strNOS != string.Empty)
				if (double.TryParse(strNOS, out NOS))
				{
					if (bNC)
						roofmultiplier = 60;
					else
						roofmultiplier = 50;

					if (NOS > 0)
					{
						if (NOS < 55 && NOS > 40)
							dumpsterfee = 350;
						else
							if (NOS < 41)
							dumpsterfee = 250;
						else
						{
							dumpsterfee = 250 * Math.Round(NOS / 40);
						}
					}

					if (newmath)
						dumpsterfee = (NOS * dumpfeemultiplyer);
				}

			return NOS * roofmultiplier + dumpsterfee;

		}

		private double FigureRecruitingCommission(double multiplyerpercent = 2.5)
		{

			double tempSalespersonSplit = 0;
			if (RecruiterText.Text != string.Empty)
				if (double.TryParse(SalespersonSplitText.Text, out tempSalespersonSplit))
					if (SalespersonName.Text != string.Empty)
						if (TotalProfit() > 0)
							return ((TotalProfit() * ((100 - tempSalespersonSplit) * .01)) -
								(TotalProfit() * ((100 - tempSalespersonSplit) * .01)) * multiplyerpercent);
			return 0;

		}

		private void FigureKnockerReferralFee()
		{
			double tempNOS = 0;
			if (ReferralKnockerText.Text != string.Empty)
			{
				double.TryParse(NumberOfSquaresAmountText.Text, out tempNOS);
				if (tempNOS > 0)
					if (tempNOS < 40)
						KnockerReferralAmountText.Value = 250;
					else KnockerReferralAmountText.Value = 500;
				else KnockerReferralAmountText.Value = 0;
			}
			else KnockerReferralAmountText.Value = 0;
			return;

		}

		public void CapOut()
		{

			{

				SettlementDifferenceAmount.Value = (decimal)FigureScopeDiff();
				MiscBillAmount.Value = (decimal)MiscCost();
				TotalAmountCollectedText.Value = (decimal)ChecksTotal();
				OverheadAmountText.Value = (decimal)FigureOverhead(OverheadMultiplierAmountText.Text);
				RoofLaborBillAmountText.Value = (decimal)FigureRoofersBill(NumberOfSquaresAmountText.Text);
				RoofMaterialExpenseSubtotalText.Value = (decimal)FigureJobMaterialCost(BringBackAmountText.Value.ToString());
				AdjustedRoofSubtotalAmountText.Value = RoofMaterialExpenseSubtotalText.Value;
				CostPerSquareAmount.Value = (decimal)FigureCostPerSquare(TotalExpense());
				ProfitPerSquareAmount.Value = (decimal)FigureProfitPerSquare(TotalProfit());
				ProfitAmountText.Value = (decimal)TotalProfit();
				AmountCollectedSubTotal.Value = (decimal)ChecksTotal();
				decimal RC = (decimal)(FigureRecruitingCommission());
				string str = FinalPaymentToMRN().ToString();
				SalespersonSplitAmountText.Value = (decimal)FinalPaymentToSalesperson("0");
				if (str.Contains("."))
					if (str.Length - str.IndexOf(".") > 2)
						MRNAmountDueText.Text = "$" + str.Substring(0, str.IndexOf(".") + 3);
					else if (str.Length - str.IndexOf(".") == 2)
						MRNAmountDueText.Text = "$" + str.Substring(0, str.IndexOf(".") + 2) + "0";
					else if (str.Length - str.IndexOf(".") == 1)
						MRNAmountDueText.Text = "$" + str.Substring(0, str.IndexOf(".") + 1) + "00";
				if (ProfitAmountText.Value > 0) InitialDrawAmountText.Value = 500;
				SalespersonAmountDueText.Value = (decimal)FinalPaymentToSalesperson(InitialDrawAmountText.Value.ToString());
				FigureKnockerReferralFee();


			}
		}



		#endregion


		#region Controls


		private void FirstCheckAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void SupplementCheckAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void DeductibleCheckAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void DepreciationAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void UpgradeCheckAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void RoofLaborBillAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void GutterBillAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void InteriorBillAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void ExteriorBillAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void BringBackAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void MaterialBillAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void ReceiptAmount4Text_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void ReceiptAmount5Text_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void ReceiptAmount3Text_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void ReceiptAmount2Text_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void ReceiptAmount1Text_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void NumberOfSquaresAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}


		private void FinalScopeAmount_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}


		private void ZipcodeText_GotFocus(object sender, RoutedEventArgs e)
		{

			ZipcodeText.SelectAll();

		}

		private void BringBackAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			BringBackAmountText.SelectAll();


		}

		private void MaterialBillAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			MaterialBillAmountText.SelectAll();
		}

		private void ReceiptAmount5Text_GotFocus(object sender, RoutedEventArgs e)
		{

			ReceiptAmount5Text.SelectAll();
		}

		private void ReceiptAmount4Text_GotFocus(object sender, RoutedEventArgs e)
		{

			ReceiptAmount4Text.SelectAll();
		}

		private void ReceiptAmount3Text_GotFocus(object sender, RoutedEventArgs e)
		{

			ReceiptAmount3Text.SelectAll();
		}

		private void ReceiptAmount2Text_GotFocus(object sender, RoutedEventArgs e)
		{

			ReceiptAmount2Text.SelectAll();
		}

		private void ReceiptAmount1Text_GotFocus(object sender, RoutedEventArgs e)
		{

			ReceiptAmount1Text.SelectAll();
		}

		private void RoofLaborBillAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			RoofLaborBillAmountText.SelectAll();
		}

		private void GutterBillAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			GutterBillAmountText.SelectAll();
		}

		private void InteriorBillAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			InteriorBillAmountText.SelectAll();
		}

		private void ExteriorBillAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			ExteriorBillAmountText.SelectAll();
		}

		private void FirstCheckAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			FirstCheckAmountText.SelectAll();
		}

		private void SupplementCheckAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			SupplementCheckAmountText.SelectAll();
		}

		private void DeductibleCheckAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			DeductibleCheckAmountText.SelectAll();
		}

		private void DepreciationAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			DepreciationAmountText.SelectAll();
		}

		private void UpgradeCheckAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			UpgradeCheckAmountText.SelectAll();
		}

		private void OverheadMultiplierAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			OverheadMultiplierAmountText.SelectAll();
		}

		private void OverheadMultiplierAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void SalespersonSplitText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void SalespersonSplitText_GotFocus(object sender, RoutedEventArgs e)
		{

			SalespersonSplitText.SelectAll();
		}

		private void RecruiterText_GotFocus(object sender, RoutedEventArgs e)
		{

			RecruiterText.SelectAll();
		}

		private void RecruitingCommision_TextChanged(object sender, TextChangedEventArgs e)
		{

			//CapOut();
		}
		private void CustomerNameText_GotFocus(object sender, RoutedEventArgs e)
		{

			CustomerNameText.SelectAll();
		}

		private void CustomerAddressText_GotFocus(object sender, RoutedEventArgs e)
		{

			CustomerAddressText.SelectAll();
		}

		private void OriginalScopeAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			OriginalScopeAmountText.SelectAll();
		}

		private void OriginalScopeAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void FinalScopeAmount_GotFocus(object sender, RoutedEventArgs e)
		{

			FinalScopeAmount.SelectAll();
		}

		private void ReferralKnocker_GotFocus(object sender, RoutedEventArgs e)
		{

			ReferralKnockerText.SelectAll();
		}

		private void ReferralKnocker_TextChanged(object sender, TextChangedEventArgs e)
		{

			CapOut();
		}

		private void NumberOfSquaresAmountText_GotFocus(object sender, RoutedEventArgs e)
		{

			NumberOfSquaresAmountText.SelectAll();
		}


		private void Window_Initialized(object sender, EventArgs e)
		{
			OverheadMultiplierAmountText.Text = "10";
			OverheadMultiplierAmountText.IsEnabled = false;
			SalespersonSplitText.Text = "50";
			SalespersonSplitText.IsEnabled = false;
			OverheadMultiplierAmountText.IsTabStop = false;
			SalespersonSplitText.IsTabStop = false;
			RoofLaborBillAmountText.IsEnabled = false;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void PeekABoo(bool bVisible = false)
		{
			if (!bVisible)
			{
				RecruiterText.Visibility = Visibility.Hidden;
				label_Copy10.Visibility = Visibility.Hidden;
				PrintButton.Visibility = Visibility.Hidden;
				Print();
			}
			else
			{
				RecruiterText.Visibility = Visibility.Visible;
				label_Copy10.Visibility = Visibility.Visible;
				PrintButton.Visibility = Visibility.Visible;
			}
		}

		private void Print()
		{
			PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
			//FigureProfit();
			if (printDlg.ShowDialog() == true)
				printDlg.PrintVisual(this, "CapOut Sheet");

			PeekABoo(true);
		}

		#endregion

		private void PrintButton_Click(object sender, RoutedEventArgs e)
		{
			PeekABoo();
		}
	}

}