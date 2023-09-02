namespace DocumentManagement.Models
{
    /// <summary>
    /// Represents a document in the system.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Unique identifier for the document.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the document.
        /// </summary>
        public string Name { get; set; }


        public byte[] Content { get; set; }


        /// <summary>
        /// Type of the document (txt, pdf, microsoftWord ...etc).
        /// </summary>
        public string FileType { get; set; }


        /// <summary>
        /// Date and time when the document was created.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
