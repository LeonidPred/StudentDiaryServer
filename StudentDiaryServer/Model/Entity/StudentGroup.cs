namespace StudentDiaryServer.Model.Entity
{
    //таблица привязки студента к группе
    public class StudentGroup
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
    }
}
