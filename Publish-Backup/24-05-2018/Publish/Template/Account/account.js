function forgetPassword() {
    if ($("#UserName").val().length > 0) {
        data = { userName: $("#UserName").val() }
        PostDataWithSuccessParam("/account/checkValidUserByName?userName=" + $("#UserName").val(), data,
            checkValidUserByNameSuccess, true);
    } else {
        $("[data-valmsg-for='UserName']").removeClass('field-validation-valid');
        $("[data-valmsg-for='UserName']").addClass('field-validation-error');
        $("[data-valmsg-for='UserName']").html('<span for="UserName" generated="true" class="">Please enter usename</span>')

    }
}

// check for valid user 
function checkValidUserByNameSuccess(data) {
    console.log(data)
    debugger;
    if (data.UserName) {
        debugger;
        $("#otp-success-msg").removeClass("d-none");
    }
    else {
        $("[data-valmsg-for='UserName']").removeClass('field-validation-valid');
        $("[data-valmsg-for='UserName']").addClass('field-validation-error');
        $("[data-valmsg-for='UserName']").html('<span for="UserName" generated="true" class="">User name does not exist, please try again</span>')

    }
}