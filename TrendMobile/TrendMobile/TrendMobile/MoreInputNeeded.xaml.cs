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

            this.Appearing += MoreInputNeeded_Appearing;
		}

        private void MoreInputNeeded_Appearing(object sender, EventArgs e)
        {
            this.OkayButton.Clicked += OkayButton_Clicked;
            this.CancelButton.Clicked += CancelButton_Clicked;
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
        }

        private void OkayButton_Clicked(object sender, EventArgs e)
        {
        }
    }
}