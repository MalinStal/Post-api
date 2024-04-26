namespace post;
public class FileModelDto{
//namn på filen
public string? Name {get; set;}
//innehållet i filen
public byte[]? Content {get; set;}

//vilken typ av fil
public string? Extension {get; set;}
public FileModelDto(){}
public FileModelDto(FileModel file){
    this.Name = file.Name;
    this.Content = file.Content;
    this.Extension = file.Extension;

}
}