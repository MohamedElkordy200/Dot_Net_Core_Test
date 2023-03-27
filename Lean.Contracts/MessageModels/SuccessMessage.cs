using RM;

namespace Lean.Contracts.MessageModels
{
    public class SuccessMessage : MessageModel
    {
        public SuccessMessage() : base(Common.TitleSuccess, Common.SuccessMessage)
        {
             
        }
        public SuccessMessage(string message) : base(Common.SuccessMessage, message)
        {
        }
        
    }
}