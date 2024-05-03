$(document).ready(function () {/********Document Ready function Starts********/
    InitCustomDropdown();
    CheckBoxCheckUnCheck();
});/********Document Ready function Ends********/

/*************************************** Bring Element To Center Starts *****************************************/

///used to referesh form Validation Added on Run Time
function ResetUnobtrusiveValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}

/*
How to use : 
 $('Id').center();
*/
$.fn.center = function () {
    this.css({
        "position": "absolute",
        "top": ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px",
        "left": ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"
    });
    return this;
}
$.fn.Leftcenter = function () {
    this.css({
        "position": "absolute",
        "left": ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"
    });
    return this;
}


$.fn.Iframecenter = function () {

    return this.each(function () {
        var top = ($(window).height() - $(this).outerHeight()) / 2;
        var left = ($(window).width() - $(this).outerWidth()) / 2;

        $(this).css({ position: 'fixed', margin: 0, top: (top > 0 ? top : 0) + 'px', left: (left > 0 ? left : 0) + 'px' });
    });
};


/*************************************** Bring Element To Center Ends *****************************************/



/************************************** *Trim Function Starts***************************************/
function trim(stringToTrim) {
    if (!isStringValid(stringToTrim)) return "";
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}
function ltrim(stringToTrim) {
    if (!isStringValid(stringToTrim)) return "";
    return stringToTrim.replace(/^\s+/, "");
}
function rtrim(stringToTrim) {
    if (!isStringValid(stringToTrim)) return "";
    return stringToTrim.replace(/\s+$/, "");
}
function isStringValid(str) {
    if (str == "") return false;
    if (str == undefined) return false;
    return true;
}
/************************************** *Trim Function Ends***************************************/

function GetQueryStringValueFromString(url, name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        //results = regex.exec(location.search);
        results = regex.exec(url);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function GetQueryStringValueFromUrl(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    //results = regex.exec(url);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

/************************************** *Referesh CHkeditor Instance Starts***************************************/
$(function () {
    $("form[data-has-chk-editor='true']").find(':submit').on("click", function () {
        UpdateCKEDITOREditorInstance();
    });
});
function UpdateCKEDITOREditorInstance() {
    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }
}
/************************************** *Referesh CHkeditor Instance Ends***************************************/
/***************************************Ajax Loading Bar Starts***************************************/
$(document).ajaxStart(function () {
    AjaxLoading();
});

$(document).ajaxStop(function () {
    CloseAjaxLoading();
});
function AjaxLoading() {

    $.blockUI({
        centerX: true,
        centerY: true,
        css: { width: "140px", height: "140px" },
        message: "<img src='/Template/images/ajaxLoader.gif'/>",
        overlayCSS: {
            opacity: 0.4
        }

    });
    $(".blockUI.blockMsg").css("background-color", "red");
    $('.blockUI.blockMsg').center();
    $('.blockUI.blockMsg').css({
        "border": "0",
        "background-color": "transparent"

    });
}
function CloseAjaxLoading() {
    $.unblockUI();
}
/***************************************Ajax Loading Bar Ends***************************************/

/***************************************** DropDown Search Feature Starts *****************************************/

function InitCustomDropdown() {
    var config = {
        'select': {
            no_results_text: "No results match",
            placeholder_text_single: "Select Option",
            placeholder_text_multiple: "Select Some Options"
        },
    }
    for (var selector in config) {
        $(selector).not(".notchosen").chosen(config[selector]);
    }
}

/***************************************** DropDown Search Feature Ends *****************************************/

function CheckBoxCheckUnCheck() {
    $('th.checkbox-column :checkbox').on('change', function () {
        $(this).parents('table').eq(0).find('tr:visible').find('td.checkbox-column :checkbox').prop("checked", $(this).prop('checked')).trigger('change');
    });
}

/********************************* Show messages starts *********************************/
function ErrorBlock(msg) {
    sweetAlert("Error !!!", msg, "error");
}
function ErrorBlockWithRedirect(msg, redirectUrl) {
    swal({
        title: "Error !!!",
        text: msg,
        type: "error"
    }, function () {
        window.location = redirectUrl;
    });
}
function Success(msg) {
    sweetAlert("Success", msg, "success");
}
function SuccessWithClosePopUp(msg, refereshPage) {
    swal({
        title: "Success",
        text: msg,
        type: "success"
    }, function () {
        window.parent.closeModelPopUpForm(refereshPage);
    });
}
/********************************* Show messages starts *********************************/




/*
How to use - 
data-inline-delete="true" 
data-process-url="where we need to ping" 
data-succuss-funciton="success function called" 
data-succuss-message="success message" 
*/
$(function () {
    $(document).on("click", '[data-inline-delete="true"]', function () {
        //$('[data-inline-delete="true"]').on("click", function () {

        var clickedTag = $(this);
        var clickedTagData = $(this).data();
        var displayMessage = "Are you sure you want to delete record";
        var newDisplayMessage = clickedTagData.confirmMessage;
        if (trim(newDisplayMessage) != "") {
            displayMessage = newDisplayMessage;
        }
        var processUrl = clickedTagData.processUrl;
        var succussfunciton = clickedTagData.succussFunciton;
        var succussMessage = clickedTagData.succussMessage;
        if (trim(processUrl) == "") {
            alert("data-process-url is empty");
            return;
        }
        var workUrl = clickedTagData.workurl;
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to delete this record",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3bafda",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                //swal("Deleted!", "Your imaginary file has been deleted.", "success");
                $.ajax({
                    type: 'POST',
                    url: processUrl,
                    success: function (data) {
                        //calling success message
                        if (trim(succussMessage) != "") {
                            Success(succussMessage);
                        }
                        //calling success function
                        if (trim(succussfunciton) != "") {
                            window[succussfunciton](data);
                        }
                        //getting clicked tbody
                        var parentTbody = clickedTag.parents("tbody");

                        //removing parent tr
                        clickedTag.parents("tr").remove();

                        //adding last empty messsage if its last record               
                        if (parentTbody.find("tr").length == 0) {
                            //removing thhead
                            //$(parentTbody).prev("thead").remove();
                            AddEmptyRowInTable(parentTbody);
                            //$(parentTbody).append('<tr class="norecordss"><td colspan="200"><div class="norecords alert alert-info fade in alert-dismissable"><strong>No record found!</strong></div></td></tr>');
                        }

                    }
                });
            });
    })
});

function AddEmptyRowInTable(parentTbody) {
    $(parentTbody).append('<tr class="norecordss"><td colspan="200"><div class="norecords alert alert-info fade in alert-dismissable"><strong>No record found!</strong></div></td></tr>');
}


//grid filters
$(function () {
    $('[data-filter-grid="true"]').on("keyup", (function () {

        var gridId = $(this).data("filter-grid-id");
        var rex = new RegExp($(this).val(), 'i');
        $('#' + gridId + ' tr').hide();

        // remove the norecords row if it already exists
        $('.norecordss').remove();

        $('#' + gridId + ' tr').filter(function () {
            return rex.test($(this).text());
        }).show();

        if ($('#' + gridId + ' tr:visible').length == 0) {

            // remove the norecords row if it already exists
            $('.norecordss').remove();

            // add the norecords row          
            //$('#' + gridId).parent('table').parent('div').append('<div class="norecords alert alert-info fade in alert-dismissable"><strong>No record found!</strong></div>');
            $('#' + gridId).append('<tr class="norecordss"><td colspan="200"><div class="norecords alert alert-info in alert-dismissable"><strong>No record found!</strong></div></td></tr>');
        }
    }))
});


/*************************************** Model Window Starts ***************************************/
/*
How to use : 
 $(this).modelPopUp({
            windowId: "addVacancyLocationDetails",
            width: 900,
            url: url
            closeOnOutSideClick: false,
        });
*/
function closeModelPopUpForm(refereshPreviousPage) {
    if (refereshPreviousPage == true) {
        window.parent.location.reload();

    }
    $("#" + model_windowId).remove();
    $("#" + model_DivModalOverlayId).remove();
    removeScrollFromBody();
}

function closeModelPopUpFormById(refereshPreviousPage, modelWindowId) {
    if (refereshPreviousPage == true) {
        window.parent.location.reload();
    }
    $("#" + modelWindowId).remove();
    removeScrollFromBody();
}

function addScrollToBody() {
    //$('body').addClass('HiddenValue');
}
function removeScrollFromBody() {
    $('body').removeClass('HiddenValue');
}

var model_windowId, model_DivModalOverlayId;
(function ($) {
    $.fn.modelPopUp = function (params) {
        //scroll to top
        $('html, body').animate({ scrollTop: 0 }, 'slow');
        //hide scroll of body
        addScrollToBody();

        var defaults = {
            parent: "body",
            windowId: "_windowId",
            url: null,
            width: 750,
            height: 700,
            scroll: "no",
            addCloseButton: false,
            IFrameId: "_IFrameId",
            DivModalOverlayId: "divmodaloverlay",
            closeOnOutSideClick: false,
            close: function () {
                $("#" + params.windowId).remove();
                $("#" + params.DivModalOverlayId).remove();
                removeScrollFromBody();
            },
        };
        //Overwrite default options 
        // with user provided ones 
        // and merge them into "options". 
        var params = $.extend({}, defaults, params);

        var modal = "";
        modal += "<div id=\"" + params.DivModalOverlayId + "\" class=\"modal-overlay\"></div>";
        modal += "<div  id=\"" + params.windowId + "\" class=\"modal-window\" style=\"width:" + params.width + "px; height:" + params.height + "px; top:1%; position: absolute;left: 50%;transform: translateX(-50%);-webkit-transform: translateX(-50%); -moz-transform: translateX(-50%); -ms-transform: translateX(-50%); max-width:90%;\">";
        if (params.addCloseButton) {
            modal += "<button style=\"float:right\" class=\"btn btn-dark-Green\" onclick=\"closeModelPopUpForm()\">Close</button>";
        }
        modal += "<iframe width='" + params.width + "'  id='" + params.IFrameId + "' height='" + params.height + "' frameborder='0' scrolling='" + scroll + "' allowtransparency='true' src='" + params.url + "'></iframe>";
        modal += "</div>";
        $(params.parent).append(modal);

        if (params.closeOnOutSideClick) {
            $("#" + params.DivModalOverlayId).click(function () {
                params.close();
            });
        }
        model_windowId = params.windowId;
        model_DivModalOverlayId = params.DivModalOverlayId;
        //Close on PouUp Close
        $(document).keyup(function (e) {
            if (e.keyCode == 27) {
                params.close();
                $(document).off("keyup");
            }
        });
    }
})(jQuery);
/*************************************** Model Window Ends *****************************************/

/*
How to use - 
data-inline-popup="true"
data-process-url="where we need to ping" 
data-window-id="unique ide of popup" 
data-pop-width="widh of popup withot px or %" 
data-pop-height="height of popup withot px or %" 
*/
$(function () {
    $(document).on("click", '[data-inline-popup="true"]', function () {

        // $('[data-inline-popup="true"]').on("click", function () {
        var clickedControl = $(this);
        var clickedControlData = $(this).data();
        var processUrl = clickedControlData.processUrl;
        if (trim(processUrl) == "") {
            alert("data-process-url is empty");
            return;
        }


        var windowId = clickedControlData.windowId;
        if (trim(windowId) == "") {
            alert("data-window-id is empty");
            return;
        }

        var width = clickedControlData.popWidth;
        if (isNaN(width)) {
            //alert("data-pop-width is empty or invalid width");
            width = $(window).width() - 100;
            //return;
        }

        var height = clickedControlData.popHeight;
        if (isNaN(height)) {
            //alert("data-pop-height is empty or invalid height");
            height = $(window).height() - 100;
            //return;
        }

        $(this).modelPopUp({
            windowId: windowId,
            width: width,
            height: height,
            url: processUrl,
            closeOnOutSideClick: false,
        });
    })
});
/****************************************Dropdown change starts **************************************/
$(function () {
    $("select[data-enable-onChange='True']").on("change", function () {
        var selectedValue = $(this).val();
        var succussfunciton = $(this).data("onchange-success");

        var canPingIfEmpty = false;
        if (trim($(this).data("ping-on-empty")) == "yes") {
            canPingIfEmpty = true;
        }

        if ((canPingIfEmpty && trim(selectedValue) == "") || trim(selectedValue) != "") {
            $.ajax({
                type: 'GET',
                url: $(this).data("onchange-url") + selectedValue,
                //data: selectedValue,
                success: function successHandler(result) {
                    if (trim(succussfunciton) != "") {
                        window[succussfunciton](result, selectedValue);
                    }
                }
            });
        }
        else {
            if (trim(succussfunciton) != "") {
                window[succussfunciton](null, selectedValue);
            }
        }
    })
});

/****************************************Dropdown change ends   **************************************/



/************************************** *AJAX POST Starts***************************************/
$(function () {
    $("form[data-post-type='ajax'][ method=\"post\"]").find(':submit').on("click", function () {
        var button = $(this);
        var form = $(this).parents('form:first');
        var successFunction = form.data("success-method");

        var resetForm = form.data("clear-form");

        if (form.data("has-chkeditor") == true) {
            UpdateCKEDITOREditorInstance();
        }
        var isValid = form.validate().form();
        if (isValid) {

            form.css("opacity", "0.2");
            $(button).attr("disabled", true);

            $.ajax({
                type: 'POST',
                url: form.attr('action'),
                data: form.serialize(),
                success: function (data) {

                    form.css("opacity", "1");
                    $(button).removeAttr("disabled");
                    if (resetForm == true && (data.Success == undefined || data.Success || data.Success == null)) {
                        (form).trigger("reset");
                        $("select").trigger("chosen:updated")
                    }
                    if (trim(successFunction) != "") {
                        window[successFunction](data, form);
                    }
                }
            });
            return false;
        }
        return false;
    });
});
function UpdateCKEDITOREditorInstance() {
    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }
}
/************************************** *AJAX POST Ends***************************************/

/************************************** *AJAX Custom POST Starts***************************************/
function PostData(url, _data, _successHandler, ShowBlackImage) {
    if (ShowBlackImage == null || ShowBlackImage == undefined) {
        ShowBlackImage = true;
    }
    $.ajax({
        type: 'POST',
        url: url,
        data: _data,
        success: _successHandler,
        global: ShowBlackImage
    });
}
/************************************** *AJAX Custom POST Ends***************************************/

/************************************** *AJAX Custom POST Starts***************************************/
function PostDataWithSuccessParam(url, _data, _successHandler, ShowBlackImage) {
    if (ShowBlackImage == null || ShowBlackImage == undefined) {
        ShowBlackImage = true;
    }
    $.ajax({
        type: 'POST',
        url: url,
        data: _data,
        success: function successHandler(result) {
            _successHandler(result, _data)
        },
        global: ShowBlackImage
    });
}
/************************************** *AJAX Custom POST Ends***************************************/


/************************************* Hover on Top menu to diplay it Starts *************************************/
$(document).ready(function () {
    jQuery('ul.nav li.dropdown').hover(function () {
        jQuery(this).find('.dropdown-menu').fadeIn('fast');
    }, function () {
        jQuery(this).find('.dropdown-menu').fadeOut('fast');
    });
});
/************************************* Hover on Top menu to diplay it Ends *************************************/

/*************************************Position Top Menu JS Starts*************************************/
$('.dropdown-toggle').on("hover", function () {

    var dropdownList = $('.dropdown-menu'),
        dropdownOffset = $(this).offset(),
        offsetLeft = dropdownOffset.left,
        dropdownWidth = dropdownList.width(),
        docWidth = $(window).width(),

        subDropdown = dropdownList.eq(1),
        subDropdownWidth = subDropdown.width(),

        isDropdownVisible = (offsetLeft + dropdownWidth <= docWidth),
        isSubDropdownVisible = (offsetLeft + dropdownWidth + subDropdownWidth <= docWidth);

    if (!isDropdownVisible || !isSubDropdownVisible) {
        dropdownList.addClass('pull-right');
    } else {
        dropdownList.removeClass('pull-right');
    }
});

/*************************************Position Top Menu JS Ends*************************************/

/***************************************just-post-me Starts***************************************/
$(function () {
    $(document).on("click", '[data-just-post-me="true"]', function () {
        var clickedTag = $(this);
        var processUrl = clickedTag.data("process-url");
        var succussfunction = clickedTag.data("success-function");
        var succussMessage = clickedTag.data("success-message");
        var showBlackImage = clickedTag.data("showblackimage");

        if (showBlackImage == null || showBlackImage == undefined) {
            showBlackImage = true;
        }

        if (trim(processUrl) == "") {
            alert("data-process-url is empty");
            return;
        }
        $.ajax({
            type: 'POST',
            url: processUrl,
            global: showBlackImage,
            success: function (data) {
                //calling success message
                if (trim(succussMessage) != "") {
                    Success(succussMessage);
                }
                //calling success function
                if (trim(succussfunction) != "") {
                    window[succussfunction](data);
                }
            }
        });

    });
});
/***************************************just-post-me ends***************************************/


/**************************Required if Validation starts***********************************/
$.validator.unobtrusive.adapters.add('requiredif', ['dependentproperty', 'desiredvalue'], function (options) {
    options.rules['requiredif'] = options.params;
    options.messages['requiredif'] = options.message;
});

$.validator.addMethod('requiredif', function (value, element, parameters) {
    var desiredvalue = parameters.desiredvalue;
    desiredvalue = (desiredvalue == null ? '' : desiredvalue).toString();
    var controlType = $("input[id$='" + parameters.dependentproperty + "']").attr("type");
    var actualvalue = {}
    if (controlType == "checkbox" || controlType == "radio") {
        var control = $("input[id$='" + parameters.dependentproperty + "']:checked");
        actualvalue = control.val();
    } else {
        actualvalue = $("#" + parameters.dependentproperty).val();
    }
    if ($.trim(desiredvalue).toLowerCase() === $.trim(actualvalue).toLocaleLowerCase()) {
        var isValid = $.validator.methods.required.call(this, value, element, parameters);
        return isValid;
    }
    return true;
});

/**************************Required if Validation end***********************************/

jQuery("document").ready(function () {
    //jQuery(".page-content").on("click", function () {
    //    jQuery(".dropdownMenu").hide();
    //});    

    /**spinner numbing**/
    $('.spinner .btn:first-of-type').on('click', function () {
        var btn = $(this);
        var input = btn.closest('.spinner').find('input');
        var inputValue = (trim(input.val()) == "" || input.val() == undefined) ? 0 : parseInt(input.val());

        if (input.attr('max') == undefined || parseInt(inputValue) < parseInt(input.attr('max'))) {
            input.val(parseInt(inputValue, 10) + 1);
        } else {
            btn.next("disabled", true);
        }
    });
    $('.spinner .btn:last-of-type').on('click', function () {
        var btn = $(this);
        var input = btn.closest('.spinner').find('input');

        var inputValue = (trim(input.val()) == "" || input.val() == undefined) ? 0 : parseInt(input.val());
        if (input.attr('min') == undefined || parseInt(inputValue) > parseInt(input.attr('min'))) {
            input.val(parseInt(inputValue, 10) - 1);
        } else {
            btn.prev("disabled", true);
        }
    });

});

/*Common Method for data-post-type='ajax' that display messgaes and redirect to url STARTS */
function AjaxRedirectToUrlSuccessfully(data, form) {
    var message = "", redirectUrl = "";
    if (trim(data.Message) != "") {
        message = data.Message;
    }
    if (trim(data.RedirectUrl) != "") {
        redirectUrl = data.RedirectUrl;
    }
    if (data.Success) {
        if (message != "") {
            swal({
                title: "Success",
                text: message,
                type: "success"
            }, function () {
                if (redirectUrl != "") {
                    window.location = redirectUrl;
                }
            });
        }
        else if (redirectUrl != "") {
            window.location = redirectUrl;
        }
    }
    else {
        if (message != "") {
            swal({
                title: "Error !!!",
                text: message,
                type: "error"
            }, function () {
                if (redirectUrl != "") {
                    window.location = redirectUrl;
                }
            });
        }
        else if (redirectUrl != "") {
            window.location = redirectUrl;
        }
    }
}
/*COmmon Method for data-post-type='ajax' that just display messgaes Ends*/
function AjaxOnSamePageWithSuccessMessage(data, form) {
    var frmData = $(form).data();
    var succeessMessageDiv = frmData.successSelector;
    var failMessageDiv = frmData.failSelector;    
    $("#" + succeessMessageDiv).addClass('d-none');
    $("#" + failMessageDiv).addClass('d-none');
    if (data.Success) {
       // $("#send-invitation-panel").hide();
        $("#" + succeessMessageDiv).removeClass('d-none');
        //$("#send-more-invitation").removeClass('d-none');
        
    }
    else {
        $("#" + failMessageDiv).removeClass('d-none')
        $("#" + failMessageDiv).text(data.Message);
    }

}



/******************************************Top Menu Selected Starts******************************************/
function TopMenuSelected() {
    var $nav = $("#collapsibleNavbar");
    var loc = location.pathname.toString().toLowerCase();
    //loc = checkUrlForInnerpages(loc, $nav).toLowerCase();
    var current = $nav.find('a[href="' + loc + '"]');
   // current.addClass("active");
    var p = current.parents("li.nav-item");
    p.addClass('active');
    //var t = current.parents("li.sidebar-nav-iten");

    //if (t.length != 0) { //For Any level child
    //    t.addClass('activeLeftMenu');
    //    t.addClass('activeLeftMenu').children("a").addClass("activeLeftMenuParent");
    //    if (screen.width > tabWith) {

    //        current.parents("ul.dropdownMenu").show();
    //    }
    //    //return;
    //}
    //else if (current.length != 0) { // For No Child
    //    current.parents().addClass('activeLeftMenu');
    //    current.addClass('activeLeftMenu').children("a").addClass("activeLeftMenuParent");
    //    //return;
    //}
}
function GetURLParameter() {
    var sPageUrl = location.pathname.toLowerCase(); //window.location.href;
    var indexOfLastSlash = sPageUrl.lastIndexOf("/");
    if (indexOfLastSlash > 0 && sPageUrl.length - 1 != indexOfLastSlash)
        return sPageUrl.substring(indexOfLastSlash + 1);
    else
        return 0;
}
function checkUrlForInnerpages(url, nav) {
    var orginalUrl = url;
    if (nav.find('a[href="' + orginalUrl + location.search + '"]').length > 0) {
        return orginalUrl + location.search;
    }
    url = url.replace("/" + GetURLParameter(), "");

    if (url.indexOf("/companies/editcompanydetails") != -1) {
        return "/companies/addnewcompany";
    }
 
    return orginalUrl;
}
/******************************************Top Menu Selected Ends******************************************/

$(document).ready(function () {
    TopMenuSelected();
});