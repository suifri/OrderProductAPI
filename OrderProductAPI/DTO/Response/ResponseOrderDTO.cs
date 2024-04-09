namespace OrderProductAPI.DTO.Response
{
    public record ResponseOrderDTO
    {
        public required int Id { get; init; }
        public DateTime CreatedOn { get; init; }
        public required string CustomerFullName { get; init; } = null!;
        public required string CustomerPhone { get; init; } = null!;
        public IEnumerable<ResponseProductDTO> Products { get; set; }
        //public required Dictionary<int, string> ProductInformation { get; init; } = null!;
    }
}
