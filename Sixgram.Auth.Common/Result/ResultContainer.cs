using Sixgram.Auth.Common.Error;

namespace Sixgram.Auth.Common.Result
{
    public class ResultContainer<T>
    {
        public T Data { get; set; }
        public ErrorType? ErrorType { get; set; }
    }
}