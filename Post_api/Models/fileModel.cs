using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Net.Http.Headers;
using post;

public class FileModel{
public int Id {get; set;}
//namn på filen
public string? Name {get; set;}
//innehållet i filen
public byte[]? Content {get; set;}

//
public string? Extension {get; set;}



public FileModel(){}
public FileModel(string name, byte[] content, string ex){
    this.Name = name;
    this.Content = content;
    this.Extension = ex;

}


}
