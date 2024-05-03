using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.Common
{
    public class QuestionDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.Question.QuestionFieldTypes>> GetQuestionFieldTypes()
        {
            return await _DbContext.QuestionFieldTypes.OrderBy(x => x.DisplayOrder).ToListAsync();
        }
    }
}
