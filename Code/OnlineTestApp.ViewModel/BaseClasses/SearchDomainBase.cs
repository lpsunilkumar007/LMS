using OnlineTestApp.Domain;
namespace OnlineTestApp.ViewModel.BaseClasses
{
    public abstract class SearchDomainBase : Paging
    {
        public string FreeTextBox { get; set; }

        public bool HasFreeText
        {
            get
            {
                return !string.IsNullOrEmpty(FreeTextBox);
            }
        }
    }
}
