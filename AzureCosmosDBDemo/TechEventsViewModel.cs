using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureCosmosDBDemo
{
    public class TechEventsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static IMongoCollection<TechEventsModel> techEventsCollection;
        readonly static string dbName = "BlogDemo";
        readonly static string collectionName = "TechEvents";
        static MongoClient client;

        private List<TechEventsModel> _techEventsList;

        private string _location, _event, _when;

        public TechEventsViewModel()
        {
            GetAllTechEvents();

            AddEventCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddNewTechEventPage());
            });

            SaveEventCommand = new Command(InsertTechEvent);
            DeleteEventCommand = new Command(DeleteEvent);
        }

      
        public string Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged("Event");
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        public string When
        {
            get { return _when; }
            set
            {
                _when = value;
                OnPropertyChanged("When");
            }
        }

        public ICommand AddEventCommand { get; set; }
        public ICommand SaveEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public IMongoCollection<TechEventsModel> MongoConnection
        {
            get
            {
                if (client == null || techEventsCollection == null)
                {
                    string connectionString = "PLACE YOUR CONNECTION STRING HERE...";
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(connectionString)
                    );
                    settings.SslSettings =
                      new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);

                    client = new MongoClient(settings);
                    var db = client.GetDatabase(dbName);

                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    techEventsCollection = db.GetCollection<TechEventsModel>(collectionName, collectionSettings);
                }

                return techEventsCollection;
            }
        }

       
        public List<TechEventsModel> TechEventsList
        {
            get { return _techEventsList; }
            set
            {

                _techEventsList = value;
                OnPropertyChanged("TechEventsList");
            }
        }

        public async void GetAllTechEvents()
        {
            var allEvents = await MongoConnection
                .Find(new BsonDocument())
                .ToListAsync();

            if (allEvents.Count == 0)
            {
                var items = new TechEventsModel
                {
                    Event = "MONDPH Xamarin Workshop",
                    Location = "Microsoft Partners Philippines",
                    When = "08/31/2019"
                };

                await MongoConnection.InsertOneAsync(items);

                allEvents = await MongoConnection
                .Find(new BsonDocument())
                .ToListAsync();
            }

            TechEventsList = new List<TechEventsModel>();
            TechEventsList.AddRange(allEvents);
        }

        

        public async void InsertTechEvent()
        {
            var items = new TechEventsModel
            {
                Event = Event,
                Location = Location,
                When = When,
            };

            await MongoConnection.InsertOneAsync(items);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void DeleteEvent(object obj)
        {
            var items = (TechEventsModel)obj;
            var result = await MongoConnection.DeleteOneAsync(tdi => tdi.Id == items.Id);

            if (result.IsAcknowledged && result.DeletedCount == 1)
            {
                GetAllTechEvents();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }
    }
}
