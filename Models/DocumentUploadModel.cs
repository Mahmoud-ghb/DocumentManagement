using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Models
{
    public class DocumentUploadModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }

}
