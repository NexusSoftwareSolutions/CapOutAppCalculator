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
using System.Diagnostics;

namespace CapOutAppCalculator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{

			//if (this.IsLoaded)
			InitializeComponent();



		}




		#region Math
		private double ChecksTotal()
		{

			double chk = 0;


			
			{
				if (DeductibleCheckAmountText.Value>0)
					chk += (double)DeductibleCheckAmountText.Value;
				if (FirstCheckAmountText.Value>0)
					chk += (double)FirstCheckAmountText.Value;
				if (DepreciationAmountText.Value>0)
					chk += (double)DepreciationAmountText.Value;
				if (SupplementCheckAmountText.Value>0)
					chk += (double)SupplementCheckAmountText.Value;
				if (UpgradeCheckAmountText.Value>0)
					chk += (double)UpgradeCheckAmountText.Value;
			}


			return chk;



		}

		private double MiscCost()
		{
			double exp = 0;
			

			{
				if (ReceiptAmount1Text.Value>0)
					exp += (double)ReceiptAmount1Text.Value;
				if (ReceiptAmount2Text.Value > 0)
					exp += (double)ReceiptAmount2Text.Value;
				if (ReceiptAmount3Text.Value > 0)
					exp += (double)ReceiptAmount3Text.Value;
				if (ReceiptAmount4Text.Value > 0)
					exp += (double)ReceiptAmount4Text.Value;
				if (ReceiptAmount5Text.Value > 0)
					exp += (double)ReceiptAmount5Text.Value;
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
		private void CapOutJob(double TotChk=0, double TotExp=0, double NoSq = 0, double splitvar =50, double ohvar =10, double smp = .25)
		{
			SettlementDifferenceAmount.Value = (decimal)FigureScopeDiff();
			if (NoSq > 0)
			{
				NoSq = (double)NumberOfSquaresAmountText.Value;

				double OH = TotChk * (ohvar * .01);
				double RawProfit = TotalProfit();
				double SalesProfit =RawProfit - (RawProfit*(100-(splitvar * .01)));
				double MRNSP = RawProfit - SalesProfit;
				double SalesMP = OH * (smp * .01);
				double MRNTP = MRNSP - SalesMP;
				double CPSQ = 0;
				double PPSQ = 0;
				if (NoSq > 0)
				{
					CPSQ = TotExp / NoSq;
					PPSQ = RawProfit / NoSq;
				}
				TotalAmountCollectedText.Value = (decimal)ChecksTotal();
				TotalExpenseText.Value = (decimal)TotalExpense();
				OverheadAmountText.Value = (decimal)OH;
				
				MiscBillAmount.Value = (decimal)MiscCost();
				KnockerReferralAmountText.Value = (decimal)FigureKnockerReferralFee(NoSq);
				RoofLaborBillAmountText.Value = (decimal)FigureRoofersBill(NoSq, (bool)!checkBox.IsChecked);
				RoofMaterialExpenseSubtotalText.Value = (decimal)FigureJobMaterialCost();
				AdjustedRoofSubtotalAmountText.Value = RoofMaterialExpenseSubtotalText.Value;

				#region FigureSalesManagerPortion

			

				if (RecruiterText.Text != string.Empty)
				{
					StringBuilder sb = new StringBuilder();
					sb.Clear();
					if (SalespersonSplitText.PercentValue > 0)
						if (SalespersonName.Text != string.Empty)
						{
							string tempstring = RecruiterText.Text;
							if (tempstring.Contains(" "))
								sb.Append(tempstring.Substring(0, tempstring.IndexOf(" ")));

							else sb.Append(RecruiterText.Text);
							sb.Append(" -- $");
							sb.Append(SalesMP);
							tempstring = sb.ToString();

							if (tempstring.Length - tempstring.IndexOf(".") > 2)
								RecruiterText.Text = tempstring.Substring(0, tempstring.IndexOf(".") + 3);
						}
				}
				#endregion



				if (CostPerSquareAmount.Value > 0) CostPerSquareAmount.Value = 0;
				CostPerSquareAmount.Value = (decimal)CPSQ;
				if (ProfitPerSquareAmount.Value > 0) ProfitPerSquareAmount.Value = 0;
				ProfitPerSquareAmount.Value = (decimal)PPSQ;
				ProfitAmountText.Value = (decimal)RawProfit;
				AmountCollectedSubTotal.Value = (decimal)ChecksTotal();
				SalespersonSplitAmountText.Value = (decimal)SalesProfit;
				MRNAmountDueText.Value = (decimal)MRNTP;
				if (ProfitAmountText.Value > 0 && ProfitAmountText.Value != 500) InitialDrawAmountText.Value = 500;
				else InitialDrawAmountText.Value = 500;
				SalespersonAmountDueText.Value = (decimal)SalesProfit - (decimal)InitialDrawAmountText.Value;
				
				
			}
			if (NumberOfSquaresAmountText.Value < .01 || NumberOfSquaresAmountText.Value==null)
			{
				NumberOfSquaresAmountText.Focus();
				NumberOfSquaresAmountText.SelectAll();
			}

		}

		private double FigureJobMaterialCost()
		{

			

		if ((MaterialBillAmountText.Value>0) && (BringBackAmountText.Value!=null))
			return (double)MaterialBillAmountText.Value + (double)BringBackAmountText.Value;
			return 0;


		}


		private double FinalPaymentToSalesperson(double SalesProfit, double draw = 500)
		{
			if (InitialDrawAmountText.Value == null)
				return 0;
			if (InitialDrawAmountText.Value == 500)
				return 500;
			draw = (double)InitialDrawAmountText.Value;
			return SalesProfit - draw;


		}

		private double FinalPaymentToMRN(double multiplyerpercent = .25)  //2.5% = .025
		{
		
			if (SalespersonName.Text == string.Empty)
			{
				RecruiterText.Text = string.Empty;
					return TotalProfit();
			}

			if (RecruiterText.Text != string.Empty)
			{
				StringBuilder sb = new StringBuilder();
				sb.Clear();
				if (SalespersonSplitText.PercentValue>0)
					if (SalespersonName.Text != string.Empty)
					{
						string tempstring = RecruiterText.Text;
						if (tempstring.Contains(" "))
							sb.Append(tempstring.Substring(0, tempstring.IndexOf(" ")));

						else sb.Append(RecruiterText.Text);
						sb.Append(" -- $");
						sb.Append((TotalProfit() * ((100 - (double)SalespersonSplitText.PercentValue) * .01)) * (multiplyerpercent * .01));
						tempstring = sb.ToString();

						if (tempstring.Length - tempstring.IndexOf(".") > 2)
							RecruiterText.Text = tempstring.Substring(0, tempstring.IndexOf(".") + 3);
					}
			}
			if (SalespersonSplitText.PercentValue > 0)
				return (TotalProfit() * ((100 - (double)SalespersonSplitText.PercentValue) * .01)) - (TotalProfit() * ((100 - (double)SalespersonSplitText.PercentValue) * .01)) * (multiplyerpercent * .01);

			return (TotalProfit() * (100 - (double)SalespersonSplitText.PercentValue) *.01) - (TotalProfit() * ((100 - (double)SalespersonSplitText.PercentValue) * .01));

		}


		private double FigureScopeDiff()
		{
			

			if ((double)OriginalScopeAmountText.Value>0 &&( (double)FinalScopeAmount.Value>0 || FinalScopeAmount.Value!=null))
				return (double)OriginalScopeAmountText.Value - (double)FinalScopeAmount.Value;

			return 0;




		}


		
		private double TotalExpense()
		{

			double TotalExpense = 0;
			

			if (RoofLaborBillAmountText.Value>0)
				TotalExpense += (double)RoofLaborBillAmountText.Value;
			if (InteriorBillAmountText.Value>0)
				TotalExpense += (double)InteriorBillAmountText.Value;
			if (ExteriorBillAmountText.Value>0)
				TotalExpense += (double)ExteriorBillAmountText.Value;
			if (GutterBillAmountText.Value>0)
				TotalExpense += (double)GutterBillAmountText.Value;
			if (MiscBillAmount.Value>0)
				TotalExpense += (double)MiscBillAmount.Value;
			if (KnockerReferralAmountText.Value>0)
				TotalExpense += (double)KnockerReferralAmountText.Value;
			if (RoofMaterialExpenseSubtotalText.Value > 0) 
				TotalExpense += (double)RoofMaterialExpenseSubtotalText.Value;
			if (OverheadAmountText.Value > 0)
				TotalExpense += (double)OverheadAmountText.Value;

			return TotalExpense;


		}


		
		private double TotalProfit()
		{


			if (ChecksTotal() > 0 && TotalExpense() > 0)
				return ChecksTotal() - TotalExpense();

			return 0;

		}

		private double FigureRoofersBill(double NOS, bool isGA = true)
		{
			
				if (!isGA)
					return NOS*67.5;
				else
					return NOS*56.25;
			
		}



		private double FigureKnockerReferralFee(double NOS)
		{
			if (ReferralKnockerText.Text != string.Empty)
			{
				if (NOS > 0 && NOS < 40)
					return 250;
				else
					return Math.Floor(NOS / 40) * 250;
			}
			return 0;
		}

		public void CapOut()
		{
			{if(NumberOfSquaresAmountText.Value>0)
				CapOutJob((double)ChecksTotal(),(double)TotalExpense(), (double)NumberOfSquaresAmountText.Value, (double)SalespersonSplitText.PercentValue, (double)OverheadMultiplierAmountText.PercentValue,(double)2.5);
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


		private void OverheadMultiplierAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{
			CapOut();
		}

		private void SalespersonSplitText_TextChanged(object sender, TextChangedEventArgs e)
		{
			CapOut();
		}


		private void Window_Initialized(object sender, EventArgs e)
		{
			SalespersonSplitText.PercentValue = 50;
			OverheadMultiplierAmountText.PercentValue = 10;
			NumberOfSquaresAmountText.Value = 0;
			MaterialBillAmountText.Value = 0;
			BringBackAmountText.Value = 0;

		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SalespersonSplitText.PercentValue = 50;
			OverheadMultiplierAmountText.PercentValue = 10;
			NumberOfSquaresAmountText.Value = 0;
			MaterialBillAmountText.Value = 0;
			BringBackAmountText.Value = 0;
		}

		private void PeekABoo(bool bVisible = false)
		{
			if (!bVisible)
			{
				RecruiterText.Visibility = Visibility.Hidden;
				
				PrintButton.Visibility = Visibility.Hidden;
				Print();
			}
			else
			{
				RecruiterText.Visibility = Visibility.Visible;
			
				PrintButton.Visibility = Visibility.Visible;
			}
		}

		private void Print()
		{
			PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
			
			if (printDlg.ShowDialog() == true)
				printDlg.PrintVisual(this, "CapOut Sheet");

			PeekABoo(true);
		}

		#endregion

		private void PrintButton_Click(object sender, RoutedEventArgs e)
		{
			PeekABoo();
		}

	
		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Application.ResourceAssembly.Location);
			Application.Current.Shutdown();
			

		}

		private void OriginalScopeAmountText_TextChanged(object sender, TextChangedEventArgs e)
		{
			FigureScopeDiff();
		}

		private void FinalScopeAmount_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			FigureScopeDiff();
		}

		private void ReferralKnockerText_TextChanged(object sender, TextChangedEventArgs e)
		{
			CapOut();
		}

		private void RecruiterText_TextChanged(object sender, TextChangedEventArgs e)
		{
			CapOut();

		}

		private void SalespersonName_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void checkBox_Checked(object sender, RoutedEventArgs e)
		{
			if (checkBox.IsChecked == true)
				CapOut();
			else CapOut();
		}
	}
}