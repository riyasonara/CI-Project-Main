﻿using System.Drawing.Printing;

namespace CI_Platform_Project.Models
{
    public class PagerViewModel
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public PagerViewModel()
        {

        }
        public PagerViewModel(int totalItems, int page, int pageSize = 4)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startpage = currentPage - 2;
            int endpage = currentPage + 2;

            if (startpage <= 0)
            {
                endpage = endpage - (startpage - 1);
                startpage = 1;
            }
            if (endpage > totalPages)
            {
                endpage = totalPages;
                if (endpage > 5)
                {
                    startpage = endpage - 4;
                }
            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startpage;
            EndPage = endpage;
        }
    }
}
