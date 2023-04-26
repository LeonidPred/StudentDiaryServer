namespace StudentDiaryServer.Model.Entity
{
    //сущность для типа аккаунта: уникальный id и название типа аккаунта
    //по умолчанию 0 - студент, 1 - преподаватель, 2 - администратор
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
