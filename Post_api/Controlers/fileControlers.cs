using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using post;

[ApiController]
[Route("post")]
public class FileControllers : ControllerBase
{
  
    FileService fileService;

    public FileControllers( FileService fileService)
    {
    
        this.fileService = fileService;
    }
  [HttpPost("addFiles")]
    public IActionResult AddFiles([FromForm]  int postId, List<IFormFile> files){
        
       

            if(files == null || files.Count > 0){
                return BadRequest("File is null");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            try{
                var newFiles = fileService.CreateFile(postId, userId, files);
            var output = newFiles.Select(f => new FileModelDto(f)).ToList();
            return Ok(output);
           
        } catch(ArgumentException ex){
            return BadRequest(ex.Message);
        }
    }
} // List<FileModel> FileList = new List<FileModel>();

            // if (files != null && files.Count > 0)
            // {
            //     using (MemoryStream stream = new MemoryStream())
            //     {
            //         foreach (var file in files)
            //         {
            //             file.CopyTo(stream);
            //             // sparar streamen i en byte Array
            //             byte[] content = stream.ToArray();

            //             FileModel model = new FileModel(file.FileName, content, file.ContentType);
            //             FileList.Add(model);
            //         }
            //         // gör en kopia av filen

            //         List<FileModel> newFiles = fileService.CreateFile(postId, userId, FileList);
            //         FileModelDto output = new FileModelDto(newFile);
            //         return Ok(output);
            //     }