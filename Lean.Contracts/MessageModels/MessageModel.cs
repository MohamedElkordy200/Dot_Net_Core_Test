namespace Lean.Contracts.MessageModels
{
    public class MessageModel
    {
    

        public Guid? Id { get; set; }
        public string Title { get; }
        public string Message { get; }

        public MessageModel(Guid? id,  string title, string message)
        {
            Id = id;
            Title = title;
            Message = message;

        }
        public MessageModel(string title,string message):this(null,title,message)
        {
             
        }
    }
}