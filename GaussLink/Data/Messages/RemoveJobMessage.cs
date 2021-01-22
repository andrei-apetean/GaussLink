namespace GaussLink.Data.Messages
{
    public class RemoveJobMessage
    {
        public string Message { get; set; }
        public RemoveJobMessage()
        {

        }
        public RemoveJobMessage(string Message)
        {
            this.Message = Message;
        }
    }
}
