using SchoolApi.Models;

namespace SchoolApi.Interfaces
{
    public interface ICourseService
    {
        Task<Course?> Create(Course course);
        Task<Course?> GetById(string id);
    }
}
