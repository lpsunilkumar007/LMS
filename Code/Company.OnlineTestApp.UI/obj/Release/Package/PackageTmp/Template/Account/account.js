function forgetPassword() {
    if ($("#EmailAddress").val().length > 0) {
        data = { userName: $("#EmailAddress").val() }
        PostDataWithSuccessParam("/account/validateuserbyemail?email=" + $("#EmailAddress").val(), data,
            checkValidUserByEmailSuccess, true);
    } else {
        $("[data-valmsg-for='EmailAddress']").removeClass('field-validation-valid');
        $("[data-valmsg-for='EmailAddress']").addClass('field-validation-error');
        $("[data-valmsg-for='EmailAddress']").html('<span for="EmailAddress" generated="true" class="">Please enter EmailAddress</span>');
    }
}

// check for valid user 
function checkValidUserByEmailSuccess(data) {    
    console.log(data)
    if (data.UserName) {
        $("#login-fail-msg").addClass("d-none");
        $("#otp-success-msg").removeClass("d-none");
    }
    else {
        $("[data-valmsg-for='EmailAddress']").removeClass('field-validation-valid');
        $("[data-valmsg-for='EmailAddress']").addClass('field-validation-error');
        $("[data-valmsg-for='EmailAddress']").html('<span for="UserName" generated="true" class="">Email Address does not exist, please try again</span>')
        $("#otp-success-msg").addClass('d-none');
        $("#login-fail-msg").addClass('d-none');   
    }
}