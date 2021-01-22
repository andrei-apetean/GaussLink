using GaussLink.Models;

namespace GaussLink.Data.Messages
{
    public class DataMessage
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public bool IsInput { get; set; }
        public bool IsStatic { get; set; }
        public bool IsStandard { get; set; }
        public JobFile JobFile { get; set; }

        public VibrationMode VibrationMode {get;set;}
        public DataMessage(string Message, JobFile JobFile, bool IsInput)
        {
            this.Message = Message;
            this.JobFile = JobFile;
            this.IsInput = IsInput;
        }
        public DataMessage(string Message, JobFile JobFile)
        {
            this.Message = Message;
            this.JobFile = JobFile;
        }
        public DataMessage(string Message, JobFile JobFile, bool IsStatic, bool IsStandard)
        {
            this.IsStatic = IsStatic;
            this.IsStandard = IsStandard;
            this.Message = Message;
            this.JobFile = JobFile;
        }
        public DataMessage(string Message, string Name, VibrationMode VibrationMode)
        {
            this.Message = Message;
            this.Name = Name;
            this.VibrationMode = VibrationMode;
        }
      
        public DataMessage(string Message)
        {
            this.Message = Message;
        }
        public DataMessage()
        {

        }
    }
}
