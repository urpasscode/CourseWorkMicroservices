using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    [Table ("note")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int note_id { get; set; }
        [Required]
        public string note_name { get; set; } = string.Empty;
        public DateTimeOffset note_create { get; set; } = DateTimeOffset.Now;
        public string description { get; set; } = string.Empty;
        public int id_user { get; set; }
    }
}
