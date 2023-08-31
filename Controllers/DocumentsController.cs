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

