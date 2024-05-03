using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineTestApp.DomainLogic.Admin.Common
{
    public static class QuestionDomainLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Domain.Question.QuestionFieldTypes>> GetQuestionFieldTypes()
        {
            using (DataAccess.Common.QuestionDataAccess obj = new DataAccess.Common.QuestionDataAccess())
            {
                return await obj.GetQuestionFieldTypes();
            }
        }
    }
}
