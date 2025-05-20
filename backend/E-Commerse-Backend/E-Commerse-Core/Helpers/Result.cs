namespace E_Commerse_Core.Helpers
{
    public class Result
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public static Result Successfull() => new Result { Success = true };

        public static Result Failed(string message) => new Result { Success = false, Message = message };

    }
}
