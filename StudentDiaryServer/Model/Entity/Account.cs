namespace StudentDiaryServer.Model.Entity
{
    //сущность с основными данными человека
    //уникальный ключ, имя, фамилия и ключ для связи с типом аккаунта
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int accountTypeId { get; set; }
    }
}
