using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using post;

[ApiController]
[Route("post")]
public class FileControllers : ControllerBase
{
  
    public FileService fileService;


    public FileControllers( FileService fileService)
    {
    
        this.fileService = fileService;
       
    }
    [HttpPost("addFiles/{postId}")]
    [Authorize("create-file")]
    public IActionResult AddFiles( int postId, [FromForm] List<IFormFile> files){
        
       

            if(files == null || files.Count == 0){
                return BadRequest("File is null");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            try{
                var newFiles = fileService.CreateFile(postId, userId, files);
            return Ok("upload succes");
           
        } catch(ArgumentException ex){
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("delete/{postId}/{fileId}")]
       [Authorize("delete-file")]
       public IActionResult deleteFile(int postId, int fileId)
       {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            try{
                var newFiles = fileService.DeleteFile( postId, userId, fileId);
         
            return Ok();
           
        } catch(ArgumentException ex){
            return BadRequest(ex.Message);
        }
       }

//  [HttpGet("file")]
//     public IActionResult DownloadFile([FromQuery] string fileName)
//     {
//         FileModel? model = context.Files.Where(file => file.Name == fileName).First();

//         return File(model.Content, model.Extension, model.Name);
//     }

}
