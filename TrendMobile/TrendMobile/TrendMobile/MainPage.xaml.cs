using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendMobile.DataContracts;
using Xamarin.Forms;

namespace TrendMobile
{
    public partial class MainPage : ContentPage
    {
        public List<EntryType> EntryTypes { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            this.ButtonEntry.Clicked += ButtonEntry_Clicked;
            EntryTypes = new List<EntryType>();

            LoadFromDisk(EntryTypes);
            RefreshUI(); 
        }

        private void LoadFromDisk(List<EntryType> list)
        {
            list.Clear();

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            ExistenceCheckResult result = rootFolder.CheckExistsAsync("db.txt").Result; 

            if(result == ExistenceCheckResult.FileExists)
            {
                IFile file = rootFolder.GetFileAsync("db.txt").Result;

                string content = file.ReadAllTextAsync().Result;
                if (!string.IsNullOrEmpty(content))
                {
                    list.AddRange(JsonConvert.DeserializeObject<List<EntryType>>(content));
                }
            }
        }

        private async Task SaveToDisk(List<EntryType> list)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile file = rootFolder.CreateFileAsync("db.txt", CreationCollisionOption.ReplaceExisting).Result;

            string jsonResult = JsonConvert.SerializeObject(list);
            await file.WriteAllTextAsync(jsonResult);
        }

        private void RefreshUI()
        {
            ItemsList.Children.Clear();
            foreach (EntryType entry in EntryTypes)
            {
                Button button = new Button();
                button.Text = entry.Name;
                button.Clicked += Button_Clicked;

                ItemsList.Children.Add(button);
            }
        }

        private async void RefreshData()
        {
            RefreshUI(); 

            await SaveToDisk(EntryTypes);
        }

        private void ButtonEntry_Clicked(object sender, EventArgs e)
        {
            if (!EntryTypes.Any(n => n.Name == EntryBox.Text) && !string.IsNullOrEmpty(EntryBox.Text))
            {
                EntryType entryType = new EntryType
                {
                    Name = EntryBox.Text,
                };

                EntryTypes.Add(entryType);

                RefreshData();
            }

            EntryBox.Text = "";
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            MoreInputNeeded moreInputPage = new MoreInputNeeded(EntryTypes.First(n => n.Name == ((Button)sender).Text));
            moreInputPage.Disappearing += MoreInputPage_Disappearing;
            await Navigation.PushModalAsync(moreInputPage);
        }

        private async void MoreInputPage_Disappearing(object sender, EventArgs e)
        {
            MoreInputNeeded page = (MoreInputNeeded)sender;

            if (page.UserChoice == DataContracts.Enums.UserChoice.Create)
            {
                DataContracts.Entry entry = new DataContracts.Entry();
                entry.Value = page.ExtraValue;
                entry.Created = DateTime.Now;

                if(page.ForEntryType.Entries == null)
                {
                    page.ForEntryType.Entries = new List<DataContracts.Entry>(); 
                }
                page.ForEntryType.Entries.Add(entry);

                await SaveToDisk(EntryTypes); 
            }
            else if (page.UserChoice == DataContracts.Enums.UserChoice.Delete)
            {
                EntryTypes.Remove(page.ForEntryType);

                RefreshData();
            }

            return;
        }
    }
}
