namespace Lean.Domain.Exceptions.DataBase
{
    public  sealed class MigrationBadRequestException : BadRequestException
    {
        public MigrationBadRequestException():base("Error when apply Migration")
        {
            
        }
    }
}
