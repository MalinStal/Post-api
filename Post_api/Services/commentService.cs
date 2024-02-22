namespace post;

public class CommentService{
    
    private DatabaseContext context;
    public CommentService(DatabaseContext context){
         this.context = context;
    }
}