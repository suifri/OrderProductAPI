namespace OrderProductAPI.DTO.Response
{
    public record ResponseProductDTO
    {
        public required int Id { get; init; }
        public required string Code { get; init; }
        public required string Name { get; init; }
        public required decimal Price { get; init; }
    }
}
