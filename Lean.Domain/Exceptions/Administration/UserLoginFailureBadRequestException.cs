namespace Lean.Domain.Exceptions.Administration
{
    public  sealed class UserLoginFailureBadRequestException : BadRequestException
    {
        public UserLoginFailureBadRequestException():base(RM.Exceptions.UserNameOrPasswordNotValid)
        {
            
        }
    }
}
