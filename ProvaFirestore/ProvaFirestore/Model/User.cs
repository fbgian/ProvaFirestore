using System;
using System.Collections.Generic;
using System.Text;
using Plugin.CloudFirestore.Attributes;
using Plugin.CloudFirestore;

namespace ProvaFirestore.Model {
    public class User {
        [Id]
        public string Id { get; set; }

        public string nome { get; set; }
        public string cognome { get; set; }
        public string nickname { get; set;}
        public string email { get; set; }



        public User(string nome, string cognome, string nickname, string email) {
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.nickname = nickname;
        }
        public User() { }
        
    }
}




