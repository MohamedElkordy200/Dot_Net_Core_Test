using RM;

namespace Lean.Contracts.MessageModels
{
    public class ErrorMessage:MessageModel 
    {
        public ErrorMessage() : base(Common.TitleError,Common.ErrorMessage)
        {

        }

        
        public ErrorMessage(string message) : base(Common.ErrorMessage, message)
        {

        }
    }
}
