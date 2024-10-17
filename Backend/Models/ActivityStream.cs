using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Time_Chat_Application.Models
{
    [Table("ActivityStream")]
    public class ActivityStream
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [StringLength(128)]
        public string Level { get; set; }
        [Required]
        public string Created_By { get; set; }
        [Required]
        public DateTime Created_On { get; set; }
        public string Exception { get; set; }

    }
}
