namespace OrderProductAPI.DTO.Request
{
    public record RequestOrderDTO
    {
        public required string CustomerFullName { get; init; }
        public required string CustomerPhone { get; init;}
        public required IEnumerable<RequestOrderProductDTO> orderProducts { get; init; }
    }
}
