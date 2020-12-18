using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProvaFirestore.Model;
using Acr.UserDialogs;

using Plugin.FirebaseAuth;
using Plugin.CloudFirestore;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProvaFirestore.TPages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sign : ContentPage {
        public Sign() {
            InitializeComponent();
            var user = CrossFirebaseAuth.Current.Instance.CurrentUser;
            CrossFirebaseAuth.Current.Instance.AuthState += (sender, e) => {
                if (user == null) {
                    logBool.Text = "non sei loggato";
                } else {
                    logBool.Text = "loggato";
                }
            };
            
        }

        private void Button_Off(object sender, EventArgs e) {
            CrossFirebaseAuth.Current.Instance.SignOut();
            UserDialogs.Instance.Toast("Logout effettuato", new TimeSpan(3));

        }

        private void SignUp(object sender, EventArgs e) {
            var email = userEmail.Text;
            var pass = userPass.Text;
            var name = userName.Text;
            var surname = userSurname.Text;
            var nick = userNick.Text;

            sign(email, pass,nick, name, surname);
        }

        //login
        private void SignIn(object sender, EventArgs e) {
            var email = userEmail.Text;
            var pass = userPass.Text;
            Login(email, pass);
        }

        private async Task Login(string email, string pass) {
            var result = await CrossFirebaseAuth.Current
                            .Instance
                            .SignInWithEmailAndPasswordAsync(email, pass);
            UserDialogs.Instance.Toast("Login effettuato", new TimeSpan(3));
        }



        //REgistrazione
        private async Task sign(string em, string pw, string nick, string nam, string sur) {
            try { 
                var result = await CrossFirebaseAuth.Current
                    .Instance
                    .CreateUserWithEmailAndPasswordAsync(em, pw);
                var Useruid = CrossFirebaseAuth.Current.Instance.CurrentUser.Uid;
                await CrossCloudFirestore.Current
                    .Instance
                    .Collection("users")
                    .Document(Useruid)
                    .SetAsync(new User(nam, sur, nick, em));
                UserDialogs.Instance.Toast("Registrazione effettuata", new TimeSpan(3));

            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}