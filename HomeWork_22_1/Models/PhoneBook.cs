using System;

namespace HomeWork_22.Models
{
    public class PhoneBook
    {
        // ID
        public int PhoneBookID { get; set; }
        // Фамилию
        public string LastName { get; set; }
        // Имя
        public string FirstName { get; set; }
        // Отчество
        public string ThreeName { get; set; }
        // Номер телефона
        public string NumberPhone { get; set; }
        //Адрес
        public string Address { get; set; }
        // Описание
        public string Description { get; set; }
    }
}
