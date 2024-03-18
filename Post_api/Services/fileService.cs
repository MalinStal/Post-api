using System.Transactions;
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

    public string CreateFile(int postId, string userId, List<IFormFile> files)
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
        using (var transaction = Context.Database.BeginTransaction())
        {
            try
            {
                foreach (var file in files)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        var content = stream.ToArray();
                        var extension = file.ContentType.ToLower();
                        var fileName = file.FileName;

                        if (content.Length > maxFileSize)
                        {
                            throw new ArgumentException("File is too large.");
                        }

                        if (!ImageTypes.Contains(extension))
                        {
                            throw new ArgumentException("Invalid file type.");
                        }

                        FileModel? newFile = new FileModel(fileName, content, extension);
                        Context.FileModels.Add(newFile);
                        newFiles.Add(newFile);
                    }

                }
                    Context.SaveChanges();
                    transaction.Commit();
                    return "upload succes";
            }
            catch (ArgumentException)
            {
                transaction.Rollback();
                throw new ArgumentException("cant upload this file");
            }
        }
    }

    // public List<FileModel> getAllFiles(string fileName){
    //      FileModel? model = Context.FileModels.Where(file => file.Name == fileName).First();

    //     return FileModel(model.Name, model.Content, model.Extension, );
    // }

    public string DeleteFile(int postId, string userId, int fileId)
    {
        User? user = Context.Users.Find(userId);
        if (user == null)
        {
            throw new ArgumentNullException("you must log in");
        }
        Post? post = Context.Posts.Find(postId);
        if (post != null)
        {
            FileModel? file = Context.FileModels.Find(fileId);
            if (file != null)
            {
                Context.FileModels.Remove(file);
                Context.Update(post);
                Context.SaveChanges();
                return "Delete successfully";
            }
        }
        throw new ArgumentNullException("Post dos not exist ");
    }
}
