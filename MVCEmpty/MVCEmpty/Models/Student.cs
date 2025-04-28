namespace MVCEmpty.Models
{
    public class Student
    {
        public Student()
        {
            
        }
        public Student(string name, string surname)
        {
            Id = id++;
            Name = name;
            Surname = surname;
        }
        static int id = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
