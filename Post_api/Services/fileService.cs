

class FileService{

    public List<string> ImageTypes {get; set;} = new List<string>(){
        "png", "jpg"
    };

    
        public FileModel CreateFile(string name, byte[] content, string ex)
     {
        var maxFileSize = 256000; //ska kunna ladda upp 250kb 

        if(content == null){
            throw new ArgumentException("file must not be null");
        } 
        if(content.Length > maxFileSize){
            throw new ArgumentOutOfRangeException("File is to big");
        }

        if(!ImageTypes.Contains(ex)){
            throw new ArgumentOutOfRangeException("the file is not valid");
        }
        FileModel? file = new FileModel(name, content, ex); 
        return file;
     }

}