﻿namespace PetCareApplication.Data
{
    public class User
    {
       public int Id { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
