using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzureCosmosDBDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TechEventsListPage : ContentPage
    {

        public ObservableCollection<TechEventsModel> TechEvents { get; set; }

        public TechEventsListPage()
        {
            InitializeComponent();
            //TechEvents = new ObservableCollection<TechEventsModel>();
            //techEventsList.ItemsSource = TechEvents;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new TechEventsViewModel();

            //var techEvents = await MongoDBService.GetAllTechEvents();

            //foreach (var item in techEvents)
            //{
            //    if (!TechEvents.Any((arg) => arg.Id == item.Id))
            //        TechEvents.Add(item);
            //}
        }

        protected async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            //var addEventPage = new NavigationPage(new AddNewTechEventPage());
            //await Navigation.PushModalAsync(addEventPage, true);
        }

        protected async void Delete_Item(object sender, EventArgs eventArgs)
        {
            //if (!(sender is MenuItem menuItem))
            //    return;

            //if (!(menuItem.CommandParameter is TechEventsModel techEvents))
            //    return;

            //var success = await MongoDBService.DeleteEvent(techEvents);

            //if (success)
            //{
            //    TechEvents.Remove(techEvents);
            //}
        }
    }
}