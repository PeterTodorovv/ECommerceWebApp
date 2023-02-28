using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Services.Models.Pagination
{
    public class BasePagination
    {
        private const int maxItemsPerPage = 50;
        private int itemsPerPage = 10;
        private int page = 1;
        public BasePagination()
        {
            isAscending = true;
            searchParam = "";
            orderBy = "default";
        }
        public int Page
        {
            get => page;
            set => page = value <= 0 ? 1 : value;
        }

        public int ItemsPerPage
        {
            get => itemsPerPage;
            set => itemsPerPage = value > maxItemsPerPage ? maxItemsPerPage : value;
        }
        public string orderBy { get; set; }
        public bool isAscending { get; set; }
        public string searchParam { get; set; }
    }
}
