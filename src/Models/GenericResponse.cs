namespace TriviaApi
{
    public class GenericResponse<T>
    {
        public GenericResponseMetadata Meta { get; set; } = new GenericResponseMetadata();

        public T Data { get; set; }
    }
}