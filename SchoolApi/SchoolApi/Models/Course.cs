using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Course name is required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Course code is required")]
        public string Code { get; set; } = null!;
    }
}
