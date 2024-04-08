namespace OrderProductAPI.DTO
{
    public record ProductDTO
    {
        public required int Id { get; init; }

        public required string Name { get; init; }
    }
}
