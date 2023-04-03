using CI_PLatform_Entities.Models;

namespace CI_Platform_Project.Models
{
    public class StoryViewModel
    {
        public long StoryId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string? Title { get; set; }

        public string? editor1 { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string Type { get; set; } = null!;

        public string Path { get; set; } = null!;

        public long StoryMedia { get; set; }

        public List<Story> stories { get; set; }
        public List<User> users { get; set; }
        public List<Mission> missions { get; set; }
        public List<MissionTheme> themes { get; set; }
        public List<StoryMedium> storyMedia { get; set; }

        public List<MissionApplication> applications { get; set; }
    }
}
