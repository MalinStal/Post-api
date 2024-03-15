using Microsoft.EntityFrameworkCore;
using post;

public class FileService
{
    DatabaseContext Context;

    public FileService(DatabaseContext context)
    {
        this.Context = context;
    }

    public List<string> ImageTypes { get; set; } = new List<string>() { "png", "jpg" };

    public List<FileModel>  CreateFile(int postId, string userId, List<IFormFile> files)
    {
    
        int maxFileSize = 256000; //ska kunna ladda upp 250kb

        var post = Context.Posts.Include(p => p.Images).FirstOrDefault(p => p.Id == postId);
        if (post == null)
        {
            throw new ArgumentException("Post does not exist.");
        }
        var user = Context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
        
            throw new ArgumentException("User does not exist.");

        }

        var newFiles = new List<FileModel>();

        foreach (var file in files)
        {
            if (file.Length > maxFileSize)
                throw new ArgumentException("File is too large.");

            var extension = Path.GetExtension(file.ContentType).ToLower();
            if (!ImageTypes.Contains(extension))
                throw new ArgumentException("Invalid file type.");

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var content = memoryStream.ToArray();

                var newFile = new FileModel(file.FileName, content, extension);
                post.AddFile(newFile);
                newFiles.Add(newFile);
            }
        }

        Context.SaveChanges();
        return newFiles;

    }


    public string RemoveFile(int postId, int userId, FileModel file)
    {
        User? user = Context.Users.Find(userId);
        if (user == null)
        {
            throw new ArgumentNullException("you must log in");
        }
        Post? post = Context.Posts.Find(postId);
        if (post != null)
        {
            post.RemoveFile(file);
            Context.Update(post);
            Context.SaveChanges();
            return "Delete successfully";
        }
        throw new ArgumentNullException("Post dos not exist ");
    }
}



        // List<FileModel> FileHolder = files;


        // User? user = Context.Users.Find(userId);
        // if (user == null)
        // {
        //     throw new ArgumentNullException("you must log in");
        // }

        // Post? post = Context.Posts.Find(postId);

        // if (post != null)
        // {
            
        //     foreach (FileModel file in FileHolder)
        //     {
        //         string? fileName = file.Name;
        //         byte[]? content = file.Content;
        //         string? extension = file.Extension;

        //         if (content == null)
        //         {
        //             throw new ArgumentException("file must not be null");
        //         }
        //         if (fileName == null)
        //         {
        //             throw new ArgumentNullException("Name must not be null");
        //         }
        //         if (content.Length > maxFileSize)
        //         {
        //             throw new ArgumentOutOfRangeException("File is to big");
        //         }
        //         if (extension != null)
        //         {
        //             if (!ImageTypes.Contains(extension))
        //             {
        //                 throw new ArgumentOutOfRangeException(
        //                     "the file is not valid, only png and jpg files"
        //                 );
        //             }
        //             else if (ImageTypes.Contains(extension))
        //             {
        //                 FileModel? newFile = new FileModel(fileName, content, extension);
        //                 post.AddFile(newFile);
        //                 Context.Update(post);
        //             }

        //             throw new ArgumentException("file type must not be null");
        //         }

        //     } 
        //     List<FileModel>? newList = post.Images;
        //     if(newList != null){
        //         Context.SaveChanges();
        //         return newList ; 
        //     }

        //  throw new ArgumentNullException("tje new list is null");                 

        // }

        // throw new ArgumentNullException("Post dos not exist ");