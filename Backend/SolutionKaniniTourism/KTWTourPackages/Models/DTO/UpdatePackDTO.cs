namespace KTWTourPackages.Models.DTO
{
    public class UpdatePackDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
    }
}