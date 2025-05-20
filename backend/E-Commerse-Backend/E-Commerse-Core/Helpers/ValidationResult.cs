namespace E_Commerse_Core.Helpers
{
    public class ValidationResult<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public static ValidationResult<T> Successfull(T data) => new ValidationResult<T> { Success = true, Data = data };

        public static ValidationResult<T> Failed(string message) => new ValidationResult<T> { Success = false, Message = message };
    }
}
