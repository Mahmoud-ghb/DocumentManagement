using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Data;
using DocumentManagement.Models;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentDbContext _context;

    public DocumentsController(DocumentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Upload a document with value of props and set a unique identifier.
    /// </summary>
    /// <param name="Name">The Name of the document to create.</param>
    /// <param name="File">The file content of the document to create.</param>
    /// <returns>The document with the specified ID.</returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<Document>> UploadDocument([FromForm] DocumentUploadModel model)
    {
        if (model.File == null || model.File.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        if (!IsValidFileType(model.File.ContentType))
        {
            return BadRequest("Invalid file type.");
        }


        var document = new Document
        {
            Name = model.Name,
            Content = await ReadFileContent(model.File.OpenReadStream()),
            FileType = model.File.ContentType,
            CreationDate = DateTime.UtcNow
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDocument), new { id = document.ID }, document);
    }


    /// <summary>
    /// Retrieves a document by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the document to retrieve.</param>
    /// <returns>The document with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Document>> GetDocument(int id)
    {

        var document = await _context.Documents.FindAsync(id);

        if (document == null)
        {
            return NotFound(); // Return a 404 Not Found response
        }

        return Ok(document); // Return a 200 OK response with the document as JSON
    }

    /// <summary>
    /// Update the name of document by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the document to update.</param>
    /// <returns>The updated document with the specified ID.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentUpdateModel model)
    {
        var document = await _context.Documents.FindAsync(id);
        if (document == null)
        {
            return NotFound();
        }


        document.Name = model.Name;

        _context.Documents.Update(document);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    /// <summary>
    /// Delete the document of unique identifier.
    /// </summary>
    /// <param name="id">The ID of the document to delete.</param>
    /// <returns>The deleted document with the specified ID.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var document = await _context.Documents.FindAsync(id);
        if (document == null)
        {
            return NotFound();
        }

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private async Task<byte[]> ReadFileContent(Stream stream)
    {
        using (var memoryStream = new MemoryStream())
        {
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    private bool IsValidFileType(string fileType)
    {
        var allowedFileTypes = new[] { 
            "application/pdf", 
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
            "text/plain" };

        return allowedFileTypes.Contains(fileType);
    }

}

