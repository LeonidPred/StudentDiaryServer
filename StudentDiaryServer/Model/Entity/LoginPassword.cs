namespace StudentDiaryServer.Model.Entity
{
    public class LoginPassword
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
