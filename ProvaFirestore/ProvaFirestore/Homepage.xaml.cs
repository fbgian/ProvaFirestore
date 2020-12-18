using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.CloudFirestore;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ProvaFirestore {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage {
        public ObservableCollection<User> Users { get; set; }
        public MainPage() {
            InitializeComponent();
            LoadUtenti();

            
        }

        private async Task DeleteUser() {
            await CrossCloudFirestore.Current
                .Instance
                .Collection("users")
                .Document("ElzBu9jyVxjqfcOy6QYq")
                .SetAsync(new { nome = "lucascemo" });
        }

        private async Task<IEnumerable<User>> GetUsers() {
            var group = await CrossCloudFirestore.Current.
               Instance.
               Collection("users").
               GetAsync();
            List<User> user = group.ToObjects<User>().ToList();
            Console.WriteLine("\n PROVA STAMPA -> " + user);
            return user;
        }

        private async Task LoadUtenti() {
            try {
                var document = await CrossCloudFirestore.Current
                    .Instance
                    .CollectionGroup("users")
                    .GetAsync();

                var utenti = document.ToObjects<User>().ToList();
                //Users = new ObservableCollection<User>(utenti);
                // Console.WriteLine("STAMPA UTENTI" + utenti[0].nome);
                userName.Text = utenti[0].nome;

            } catch(Exception e) {
                Console.WriteLine("EXCEPTION " + e);
            }
        }
    }
}
