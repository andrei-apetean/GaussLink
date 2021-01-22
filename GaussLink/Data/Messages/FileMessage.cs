namespace GaussLink.Data.Messages
{
    public class FileMessage
    {
        public string Message { get; set; }
        public FileMessage(string Message)
        {
            this.Message = Message;
        }
    }
}
