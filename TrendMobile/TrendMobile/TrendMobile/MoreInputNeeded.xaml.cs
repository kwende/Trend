using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrendMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoreInputNeeded : ContentPage
	{
		public MoreInputNeeded ()
		{
			InitializeComponent ();

            this.OkayButton.Clicked += OkayButton_Clicked;
            this.CancelButton.Clicked += CancelButton_Clicked;
        }

        async private void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(); 
        }

        async private void OkayButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}