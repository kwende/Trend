using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrendMobile
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            this.ButtonEntry.Clicked += ButtonEntry_Clicked;
            this.SomethingButton.Clicked += SomethingButton_Clicked;
        }

        async private void SomethingButton_Clicked(object sender, EventArgs e)
        {
            MoreInputNeeded moreInputPage = new MoreInputNeeded();

            await Navigation.PushModalAsync(moreInputPage); 
        }

        private void ButtonEntry_Clicked(object sender, EventArgs e)
        {
        }
    }
}
