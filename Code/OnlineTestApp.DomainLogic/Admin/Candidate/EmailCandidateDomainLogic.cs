using OnlineTestApp.DomainLogic.Admin.Email;
using OnlineTestApp.ViewModel.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.Admin.DomainLogic.Candidate
{
    public class EmailCandidateDomainLogic : OnlineTestApp.DomainLogic.Admin.BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailCandidateViewModel"></param>
        /// <param name="sendBy"></param>
        /// <returns></returns>
        //public async Task InviteCandidateForTest(EmailCandidateViewModel emailCandidateViewModel, Guid sendBy)
        //{
        //    SendEmailDomainLogic obj = new SendEmailDomainLogic();
        //    string emailSubject = emailCandidateViewModel.SendEmail.EmailSubject;
        //    string smailBody = emailCandidateViewModel.SendEmail.EmailBody;

        //    StringBuilder sbBody, sbSubject;
        //    sbBody = new StringBuilder();
        //    sbSubject = new StringBuilder();
                        
        //    smailBody = smailBody.Replace("@@ApplyForTestUrl@@", SystemSettings.ApplyForTestUrl);
        //    sbSubject = sbSubject.Replace("@@ApplyForTestUrl@@", SystemSettings.ApplyForTestUrl);



        //    foreach (var candidateAssignedTest in emailCandidateViewModel.LstCandidateAssignedTest)
        //    {
        //        sbBody.Clear();
        //        sbSubject.Clear();

        //        sbBody.Append(smailBody);
        //        sbSubject.Append(emailSubject);

        //        sbBody.Replace("@@TestReferenceNumber@@", candidateAssignedTest.TestReferenceNumber);
        //        sbSubject.Replace("@@TestReferenceNumber@@", candidateAssignedTest.TestReferenceNumber);

        //        //getting candidate data                
        //        var candidateData = await obj.GetCandidateData(candidateAssignedTest.CandidateId);

        //        //replacing candidate data
        //        ReplaceCandidateParameters(sbBody: sbBody, sSubject: sbSubject, candidateData: candidateData);

        //        //replace candidate branch data
        //        await SendEmailDomainLogic.ReplaceCompanyData(candidateData.FKCompanyId, sbBody: sbBody, sSubject: sbSubject);

        //        emailCandidateViewModel.SendEmail.EmailBody = sbBody.ToString();
        //        emailCandidateViewModel.SendEmail.EmailSubject = sbSubject.ToString();

        //        //emailCandidateViewModel.SendEmail.EmailToName = candidateData.FullName;
        //        emailCandidateViewModel.SendEmail.EmailToEmailAddress = candidateData.CandidateEmailAddress;

        //        emailCandidateViewModel.SendEmail.EmailSendToCandidateId = candidateData.CandidateId;
        //        emailCandidateViewModel.SendEmail.EmailSentBy = sendBy;

        //        //sending email
        //        await obj.EmailCandidate(emailCandidateViewModel.SendEmail);
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sbBody"></param>
        /// <param name="sSubject"></param>
        /// <param name="candidateData"></param>
        void ReplaceCandidateParameters(StringBuilder sbBody, StringBuilder sSubject, Domain.Candidate.Candidates candidateData)
        {

            sbBody.Replace("@@Candidate_UniqueId", candidateData.CandidateId.ToString());
            //sbBody.Replace("@@Candidate_FirstName", candidateData.FirstName);
            //sbBody.Replace("@@Candidate_LastName", candidateData.LastName);
            //sbBody.Replace("@@Candidate_FullName", candidateData.FullName);
            //sbBody.Replace("@@Candidate_EmailAddress", candidateData.EmailAddress);
            //sbBody.Replace("@@Candidate_MobileNumber", candidateData.MobileNumber);
            //sbBody.Replace("@@Candidate_City", candidateData.City);
            //sbBody.Replace("@@Candidate_AlternateNumber", candidateData.AlternateNumber);


            sSubject.Replace("@@Candidate_UniqueId", candidateData.CandidateId.ToString());
            //sSubject.Replace("@@Candidate_FirstName", candidateData.FirstName);
            //sSubject.Replace("@@Candidate_LastName", candidateData.LastName);
            //sSubject.Replace("@@Candidate_FullName", candidateData.FullName);
            //sSubject.Replace("@@Candidate_EmailAddress", candidateData.EmailAddress);
            //sSubject.Replace("@@Candidate_MobileNumber", candidateData.MobileNumber);
            //sSubject.Replace("@@Candidate_City", candidateData.City);
            //sSubject.Replace("@@Candidate_AlternateNumber", candidateData.AlternateNumber);

        }
    }
}
