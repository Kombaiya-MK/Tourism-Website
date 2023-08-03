namespace KTWTourPackages.Models.DTO
{
    public class PackageDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string? LocationId { get; set; }
        public double Price { get; set;}
        public int Capacity { get; set;}
        public string? Status { get; set; }
    }
}