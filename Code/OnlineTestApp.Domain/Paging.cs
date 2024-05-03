using OnlineTestApp.Domain.BaseClasses;


namespace OnlineTestApp.Domain
{
    public class Paging : DomainBase
    {
        /// <summary>
        /// gives Current page of the paging
        /// </summary>
        public int CurrentPage
        {
            get
            {
                int currentPage;
                if (!Utilities.QueryStringHelper.GetIntValue("page", out currentPage))
                {
                    currentPage = 1;
                }
                return currentPage;
            }
        }
        /// <summary>
        /// From where the paging starts
        /// </summary>
        public int FirstIndex { get; set; }
        /// <summary>
        /// From where the paging ends
        /// </summary>
        public int LastIndex { get; set; }
        /// <summary>
        /// gives the last page of the paging
        /// </summary>
        public int LastPage { get; set; }
        /// <summary>
        /// Contains Other Query String in URL
        /// </summary>
        public string QueryString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SkipRecords
        {
            get
            {
                return (CurrentPage - 1) * PageSize;
            }
        }

        int _pageSize = SystemSettings.PageSize;

        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Used to get Sort by
        /// </summary>
        public string SortBy
        {
            get
            {
                if (string.IsNullOrEmpty(_sortBy))
                {
                    _sortBy = Utilities.QueryStringHelper.GetQueryStringVaue("SortBy");
                }
                if (string.IsNullOrWhiteSpace(_sortBy) || string.IsNullOrEmpty(_sortBy))
                {
                    return string.Empty;
                }
                return _sortBy;
            }
            set
            {
                _sortBy = value;
            }
        }

        private string _sortBy = string.Empty;


    }
}
