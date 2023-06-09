﻿using CI_PLatform_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class StoryViewModel
    {
        public string storymediapath { get; set; }

        public string Useravtar { get; set; }


        public string lastname { get; set; }

        public string username { get; set; }

        public string Themename { get; set; }

        public string? StoryTitle { get; set; }
        public string? ShortDescription { get; set; }
        public List<IFormFile> attachment { get; set; }
        public int? StoryViews { get; set; }

        public User Singleuser { get; set; }
        public Timesheet Singlesheet { get; set; }

        public Story singleStory { get; set; }
        public long StoryId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid title")]
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


        //Timesheet
        public List<Timesheet> timesheets { get; set; }

        public long TimesheetId { get; set; }

        public long hiddenid { get; set; }


        public string TimesheetTime { get; set; }

        
        public int? Action { get; set; }


        public DateTime DateVolunteered { get; set; }

        public string? Notes { get; set; }


        [Required]
        public int hour { get; set; }

        [Required]
        public int minute { get; set; }


        [Required(ErrorMessage = "Select mission")]
        public virtual Mission? Mission { get; set; }

    }
}
