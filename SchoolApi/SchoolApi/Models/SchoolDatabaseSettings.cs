namespace SchoolApi.Models
{
    public class SchoolDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string StudentsCollectionName { get; set; } = null!;
        public string CoursesCollectionName { get; set; } = null!;
        public string ConnectionString {  get; set; } = null!;
    }
}
