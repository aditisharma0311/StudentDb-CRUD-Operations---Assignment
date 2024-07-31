using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDb.Models
{
    public class Result
    {
        [Key]
        public int RollNo { get; set; }

        public int Hindi { get; set; }
        public int English { get; set; }
        public int Science { get; set; }
        public int History { get; set; }
        public int GK { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int TotalMarks { get; set; }

        // Foreign key relationship
        [ForeignKey("RollNo")]
        public Students Student { get; set; }
    }
}
