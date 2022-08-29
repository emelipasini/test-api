using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Entity { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Error { get; set; }

        public Log(int id, string entity, string action, string error)
        {
            Id = id;
            Entity = entity;
            Action = action;
            Error = error;
        }
    }
}
