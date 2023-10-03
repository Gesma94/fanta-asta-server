using FantaAstaServer.Models.DTOs;

namespace FantaAstaServer.Models.APIs
{
    public class HubMethodResult
    {
        public static HubMethodResult Success = new HubMethodResult();

        public HubMethodResult()
        {
        }

        public HubMethodResult(Error error)
        {
            Error = error;
        }

        public bool HasError { get => Error != null; }

        public Error Error { get; private set; } = null;
    }

    public class HubMethodResult<T> : HubMethodResult where T : class
    {
        public HubMethodResult(T result) 
        {
            Result = result;
        }

        public HubMethodResult(Error error) : base(error) 
        {
        }

        public T Result { get; private set; } = null;
    }


}
