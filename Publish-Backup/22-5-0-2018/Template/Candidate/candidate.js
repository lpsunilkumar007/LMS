function startCandidateTest(testInvitationId) {
   // window.location = '/Candidate/CandidateTestQuestions?testId=' + testPaperId;
    data = { testInvitationId: testInvitationId};
    PostDataWithSuccessParam("/Candidate/StartTest?testInvitationId=" + testInvitationId, data, startCandidateTestSuccess, true);


}
function startCandidateTestSuccess(result, _data) {
    //start test 
    if (result.success) {
        window.location = '/Candidate/CandidateTestQuestions?id=' + _data.testInvitationId;
    }
   
}