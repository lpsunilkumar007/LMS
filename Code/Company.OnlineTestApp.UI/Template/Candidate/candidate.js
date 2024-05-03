function startCandidateTest(testInvitationId) {
    // window.location = '/Candidate/CandidateTestQuestions?testId=' + testPaperId;
    if ($("#frm-Candidate-Info").valid()) {
        PostDataWithSuccessParam("/Candidate/addCandidateInfo", $("#frm-Candidate-Info").serialize(), addCandidateInfoSuccess, true);

    }
    else {
        $("#alert-name-required").removeClass('d-none');
    }

}


function addCandidateInfoSuccess(result, _data) {
    debugger;
    //start test 
    if (result.success) {

        //data = { testInvitationId: result.data.FkTestInvitationId };
        PostDataWithSuccessParam("/Candidate/StartTest"
            , $("#frm-Candidate-Info").serialize(), startCandidateTestSuccess, true);
    }
}
function startCandidateTestSuccess(result, _data) {
    //start test 
    if (result.success) {
        window.location = '/Candidate/CandidateTestQuestions?id=' + result.data.FkTestInvitationId;
    }
}