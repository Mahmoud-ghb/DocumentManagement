using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Models
{
    public class DocumentUpdateModel
    {
        [Required]
        public string Name { get; set; }
    }

}
