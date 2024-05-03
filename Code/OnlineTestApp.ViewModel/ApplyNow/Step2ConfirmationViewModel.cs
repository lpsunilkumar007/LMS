using System;

namespace OnlineTestApp.ViewModel.ApplyNow
{
    public class Step2ConfirmationViewModel : ApplyNowBase
    {
        public Domain.Test.TestDetails TestDetails { get; set; }
        
        public Guid CandidateAssignedId { get; set; }

        public int TotalQuestions { get; set; }

        public decimal TotalTime { get; set; }

        public string StrTotalTime
        {
            get
            {
                return string.Format("{0:%h}", TimeSpan.FromMinutes(Convert.ToInt32(TotalTime)).Hours.ToString()) + ":" + string.Format("{0:%m}", (Convert.ToInt32(TotalTime) - (TimeSpan.FromMinutes(Convert.ToInt32(TotalTime)).Hours * 60)).ToString());
            }
        }
    }
}
