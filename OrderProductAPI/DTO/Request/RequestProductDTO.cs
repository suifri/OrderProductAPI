namespace OrderProductAPI.DTO.Request
{
    public record RequestProductDTO
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public required decimal Price { get; init; }
    }
}
