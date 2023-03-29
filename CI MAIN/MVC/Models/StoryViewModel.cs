namespace CI_Platform_Project.Models
{
    public class StoryViewModel
    {
        public long StoryId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string Type { get; set; } = null!;

        public string Path { get; set; } = null!;

        public long StoryMedia { get; set; }
    }
}
