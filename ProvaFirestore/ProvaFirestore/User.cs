using System;
using Plugin.CloudFirestore.Attributes;
using Plugin.CloudFirestore;



namespace ProvaFirestore {
    public class User {
        [Id]
        public string Id { get; set; }
        public User(string nome) {
            this.nome = nome;
        }
        public User() { }
        public string nome { get; set; }
    }
}
