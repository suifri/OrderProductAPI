namespace OrderProductAPI.DTO
{
    public record OrderDTO
    {
        public required int Id { get; init; }
        public DateTime CreatedOn { get; init; }
        public required string CustomerFullName { get; init; } = null!;
        public required string CustomerPhone { get; init; } = null!;
    }
}
