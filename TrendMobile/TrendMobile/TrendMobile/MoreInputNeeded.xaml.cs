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

            this.OkayButton.Clicked += OkayButton_Clicked;
            this.CancelButton.Clicked += CancelButton_Clicked;
            this.DeleteButton.Clicked += DeleteButton_Clicked;

            ExtraValue = null;
            UserChoice = UserChoice.Unknown;
            ForEntryType = entryType; 
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
            if (double.TryParse(AdditionalEntry.Text, out val))
            {
                ExtraValue = val;
            }

            UserChoice = UserChoice.Create;
            await Navigation.PopModalAsync();
        }
    }
}