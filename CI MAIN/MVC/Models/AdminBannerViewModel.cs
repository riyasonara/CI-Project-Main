using CI_PLatform_Entities.Models;

namespace CI_Platform_Project.Models
{
    public class AdminBannerViewModel
    {
        public List<Banner> banners { get; set; }     
        public string BannerText { get; set; }
        public int? BannerSortOrder { get; set; }
        public string img { get; set; }
        public long BannerId { get; set; }

        public string? Description { get; set; }

    }
}
