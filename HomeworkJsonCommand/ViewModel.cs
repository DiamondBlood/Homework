using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace HomeworkJsonCommand
{
    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Geo geo { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Company company { get; set; }
    }
    public class ViewModel : INotifyPropertyChanged
    {
        private const string url = "https://jsonplaceholder.typicode.com/users";
        private HttpClient _Client = new HttpClient();
        private int _value = 123;
        private ObservableCollection<Root> Root;

        public ObservableCollection<Root> _root
        {
            get => Root;
            set{
                Root = value;
                OnPropertyChanged();
            }
        }
        public async Task Output()
        {
            var content = await _Client.GetStringAsync(url);
            var root = JsonConvert.DeserializeObject<List<Root>>(content);
            _root = new ObservableCollection<Root>(root);
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public async Task Changevalue()
        {
            await Task.Delay(3000);
            int rnd = new Random().Next(1000);
            Value = rnd;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand ChangeCommand => new Command(async value =>
        {
            await Changevalue();
        });

        public ICommand LoadData => new Command(async value =>
        {

            await Output();
        });
    }
}