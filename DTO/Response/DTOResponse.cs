namespace DNDServer.DTO.Response
{
    public class DTOResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; } 

    }
}
