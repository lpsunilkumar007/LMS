function experinceLevelSubmit() {//step1


    if ($('input[name="experienceLevels"]:checked').val() == null) {
        $("#experience-tab-error").removeClass("d-none");
    } else {
        $("#experience-tab-error").addClass("d-none");
        $(".invite-test-tabs").addClass('d-none');
        $("#technology-tab").removeClass('d-none');
        $("#link-experience-tab").addClass('completed');
        $(".test-progress  ul  li").removeClass('active');
        $("#link-technology-tab").addClass('active');
        //
        $("#company-left-panel-text").text('Select Technologies ');
    }
}
function technologyListSubmit() {//step 2
    if ($('#question-technology :selected').val() == null) {
        $("#technology-tab-error").removeClass("d-none");
    } else {
        $("#technology-tab-error").addClass("d-none");
        $(".invite-test-tabs").addClass('d-none');
        $("#time-tab").removeClass('d-none');
        $("#link-technology-tab").addClass('completed');
        $(".test-progress  ul  li").removeClass('active');
        $("#link-time-tab").addClass('active');
        //
        $("#company-left-panel-text").text('Prepare Test');
    }
}
function testDurationSubmit() {// step 3 
    if ($('input[name="testDuration"]:checked').val() == null) {
        $("#duration-tab-error").removeClass("d-none");
    } else {


        $("#duration-tab-error").addClass("d-none");
        $(".invite-test-tabs").addClass('d-none');
        $("#sampleTest-tab").removeClass('d-none');
        $("#link-time-tab").addClass('completed');

        $(".test-progress  ul  li").removeClass('active');
        //$("#link-preview-tab").addClass('active');
        $("#link-preview-tab").addClass('completed');
        $("#company-left-panel-text").text('Sample Tests');

        var experience = $('input[name="experienceLevels"]:checked').val();
        var technology = [];
        $('#question-technology :selected').each(function (i, selected) {
            technology[i] = $(selected).val();
        });
        var duration = parseInt($('input[name="testDuration"]:checked').val());
        var isNagativeMarking = ($('input[name="IsNagativeMarking"]:checked').val() == "true");
        data = {
            selectedTechnologies: technology, experience: experience, duration: duration, isNagativeMarking: isNagativeMarking
        }
        PostData("/invitetest/PrepareTest", data, prepareSampleTestSuccess, true);
    }
}
function selectTestMockup(sampleTestMockUpId) {
    $('#btn-inviteCandidate').prop("disabled", false);
    $("#selectdTestMockup").val(sampleTestMockUpId);
    $("[sample-test-btn='" + sampleTestMockUpId + "']").addClass('selected-mockup');
    $("[sample-test-btn-mb='" + sampleTestMockUpId + "']").addClass('selected');

}
function previewTestSubmit() {
    var sampleTestMockUpId = $("#selectdTestMockup").val()
    if (sampleTestMockUpId == null || sampleTestMockUpId == "") {
        $("#sampleTest-tab-error").removeClass("d-none");
    } else {
        $("#company-left-panel-text").text('Send Invitations');
        $("#sampleTest-tab-error").addClass("d-none");
        $(".invite-test-tabs").addClass('d-none');
        $("#send-invitation-tab").removeClass('d-none');
        $("#link-preview-tab").addClass('completed');
        $(".test-progress  ul  li").removeClass('active');
        $("#link-preview-tab").addClass('active');
        data = { sampleTestMockUpId: sampleTestMockUpId }
        PostDataWithSuccessParam("/InviteTest/PrepareTestPaper?sampleTestId=" + sampleTestMockUpId, data, prepareTestPaperSuccess, true);
    }
}
function prepareTestPaperSuccess(result, _data) {
    if (result.success) { $("#FkTestPaperId").val(result.data.Data.TestPaperId); }
}
function prepareSampleTestSuccess(result) {
    if (result.success) {
        var sampleTestlst = result.data;
        data = {
            sampleTestlst: sampleTestlst
        }
        PostData("/invitetest/_GetSampleTests", data, GetSampleTestSuccess, true);
    }
}
function GetSampleTestSuccess(data) {
    $(".test-progress  ul  li").removeClass('active');
    $("#link-preview-tab").addClass('active');
    $(".invite-test-tabs").addClass('d-none');
    $("#sampleTest-tab").removeClass('d-none');
    $("#sampleTest-tab").html(data);
}
function checkTestDurationRadio(checkBox) {
    $('.tim-tab>div>a.time-dur').removeClass('selected-duration');

    $(".sample-test-duration input[type='radio']").prop("checked", false);
    $("#" + checkBox).prop("checked", true);

    $("#" + checkBox).parents('a').addClass('selected-duration');
}
$(function () {
    $(document).on("click", '[data-inline-select-tab="true"]', function () {
        var clickedTag = $(this);
        var clickedTagData = $(this).data();
        if ($(this).hasClass('completed')) {
            $(".invite-test-tabs").addClass('d-none');
            $("#" + clickedTagData.tab).removeClass('d-none');
            debugger;
            var leftPanelText = clickedTagData.leftPanelHeading;
            // change text of left panel
            $("#company-left-panel-text").text(leftPanelText);
        }
    })
});
function previewMockupTestById(sampleTestMockUpId) {
    data = {}
    PostData("/InviteTest/PreviewTestById?sampleTestId=" + sampleTestMockUpId, data, previewMockupTestSuccess, true);
}
function openCompanyModel() {
    $("#company-modal-pop-up").modal('show');
}
function previewMockupTestSuccess(data) {
    $("#company-modal-pop-up").html(data);
    openCompanyModel();
}
function viewCandidatesByTestId(testPaperId, retrievePapers) {
    data = {}
    PostData("/dashboard/getcandidatesresultbytestpaperId?testPaperId=" + testPaperId + "&retrievePapers=" + retrievePapers, data, viewCandidatesByTestIdSuccess, true);
}
function viewCandidatesByTestIdSuccess(data) {

    $("#company-modal-pop-up").html(data);
    openCompanyModel();
}