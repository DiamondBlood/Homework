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
    public class User
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }
    public class ViewModel : INotifyPropertyChanged
    {
        private const string url = "https://jsonplaceholder.typicode.com/albums";
        private HttpClient _Client = new HttpClient();
        private int _someInt = 123;
        private ObservableCollection<User> User;

        public ObservableCollection<User> _user
        {
            get => User;
            set{
                User = value;
                OnPropertyChanged();
            }
        }
        public async Task Output()
        {
            var content = await _Client.GetStringAsync(url);
            var root = JsonConvert.DeserializeObject<List<User>>(content);
            _user = new ObservableCollection<User>(root);
        }

        public int SomeInt
        {
            get => _someInt;
            set
            {
                _someInt = value;
                OnPropertyChanged();
            }
        }

        public async Task ChangeInt()
        {
            await Task.Delay(2000);
            int rnd = new Random().Next(100);
            SomeInt = rnd;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand SomeCommand => new Command(async value =>
        {
            await ChangeInt();
        });

        public ICommand LoadData => new Command(async value =>
        {
            await Output();
        });
    }
}