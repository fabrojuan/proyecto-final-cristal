namespace MVPSA_V2022.Exceptions
{
    public class ResourceIsBeingUsedException : Exception
    {
        public ResourceIsBeingUsedException(string message) : base(message)
        {}
    }
}
