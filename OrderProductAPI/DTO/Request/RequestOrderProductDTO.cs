namespace OrderProductAPI.DTO.Request
{
    public record RequestOrderProductDTO
    {
        public int ProductId { get; init; }
        public int Amount { get; init; }
    }
}
