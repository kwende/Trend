using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendMobile.DataContracts;
using TrendMobile.DataContracts.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrendMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreInputNeeded : ContentPage
    {
        public UserChoice UserChoice { get; set; }
        public double? ExtraValue { get; set; }
        public EntryType ForEntryType { get; private set; }

        public MoreInputNeeded(EntryType entryType)
        {
            InitializeComponent();

            if (entryType.DataType == EntryTypeDataType.Unspecified)
            {
                this.OkayButton.IsVisible = false;
                this.CancelButton.IsVisible = false;
                this.DeleteButton.IsVisible = false;
                this.TypePicker.IsVisible = true; 

                this.TypePicker.SelectedIndexChanged += TypePicker_SelectedIndexChanged;
            }
            else if(entryType.DataType == EntryTypeDataType.OneThroughTen)
            {
                this.OneThroughTenPicker.IsVisible = true; 
            }
            else if(entryType.DataType == EntryTypeDataType.LowMediumHigh)
            {
                this.LowMediumHighPicker.IsVisible = true; 
            }
            else if(entryType.DataType == EntryTypeDataType.Decimal)
            {
                this.DecimalPicker.IsVisible = true; 
            }

            this.OkayButton.Clicked += OkayButton_Clicked;
            this.CancelButton.Clicked += CancelButton_Clicked;
            this.DeleteButton.Clicked += DeleteButton_Clicked;


            ExtraValue = null;
            UserChoice = UserChoice.Unknown;
            ForEntryType = entryType;
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue= TypePicker.SelectedItem.ToString();

            if(selectedValue == "One Through Ten")
            {
                ForEntryType.DataType = EntryTypeDataType.OneThroughTen;
                this.OneThroughTenPicker.IsVisible = true;
            }
            else if(selectedValue == "Low Medium High")
            {
                ForEntryType.DataType = EntryTypeDataType.LowMediumHigh;
                this.LowMediumHighPicker.IsVisible = true; 
            }
            else if(selectedValue == "Decimal")
            {
                ForEntryType.DataType = EntryTypeDataType.Decimal;
                this.DecimalPicker.IsVisible = true;
            }
            else if(selectedValue == "Single Tap")
            {
                ForEntryType.DataType = EntryTypeDataType.SingleTap; 
            }

            this.OkayButton.IsVisible = true;
            this.CancelButton.IsVisible = true;
            this.DeleteButton.IsVisible = true;
            this.TypePicker.IsVisible = false;

            return; 
        }

        async private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            UserChoice = UserChoice.Delete;
            await Navigation.PopModalAsync();
        }

        async private void CancelButton_Clicked(object sender, EventArgs e)
        {
            UserChoice = UserChoice.Cancel;
            await Navigation.PopModalAsync();
        }

        async private void OkayButton_Clicked(object sender, EventArgs e)
        {
            double val = 0.0;
            //if (double.TryParse(AdditionalEntry.Text, out val))
            //{
            //    ExtraValue = val;
            //}

            UserChoice = UserChoice.Create;
            await Navigation.PopModalAsync();
        }
    }
}