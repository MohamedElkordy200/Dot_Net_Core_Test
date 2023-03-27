namespace Lean.Domain.Exceptions.Administration
{
    public  sealed class UserCreatedErrorBadRequestException : BadRequestException
    {
        public UserCreatedErrorBadRequestException(string message):base(message??RM.Exceptions.SaveError)
        {
            
        }
    }
}
