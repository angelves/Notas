using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notas.Models.POCOs
{
    [Table("Notas", Schema = "dbo")]
    public class Notas
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Important { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Text { get; set; }

    }
}
