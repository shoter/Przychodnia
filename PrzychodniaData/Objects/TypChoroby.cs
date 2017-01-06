using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Objects
{
    [Table("typychorob", Schema = "public")]
    public class TypChoroby
    {
        public TypChoroby()
        {
        }

        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int ID { get; set; }

        [Column("nazwa"), MaxLength(50)]
        public string Nazwa { get; set; }
    }
}
