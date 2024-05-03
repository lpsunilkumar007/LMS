   var tabWith = 1025;
/********Document Ready function Starts********/
$(document).ready(function () {

    UniversalTimeConvertor();
    UniversalDateTimeConvertor();
    LeftMenuSelected();

    $('table.dataTable tr:visible:odd').addClass('even');
    $('table.dataTable tr:visible:even').addClass('odd');

    /**scroll table**/
    $(".data-grid").doubleScroll({ resetOnWindowResize: true });

});/********Document Ready function Ends********/





/******************************************Left Menu Events Starts******************************************/
function LeftMenuToggle(e) {
    if (screen.width >= tabWith) {
        //e.preventDefault();
        jQuery("#wrapper").toggleClass("MenuActive");
        if ($("body").hasClass("MenuActive") == false) {
            jQuery("li.dropdown-Nav").children('ul.dropdownMenu').hide();
        }
    }
}
$(document).ready(function () {

    /**Window resize**/
    $(window).on('resize', function () {
        if (screen.width < tabWith) {
            jQuery("li.dropdown-Nav").siblings('li.dropdown-Nav').find('ul.dropdownMenu').hide();
        }
    });

    /*Menu-toggle*/
    jQuery("#menu-toggle").click(function (e) {
        LeftMenuToggle(e);
    });
    /*closing left menu*/
    //if ($("#closeMenu").length > 0) {
    //    LeftMenuToggle($("menu-toggle"));
    //}

    /* on hover dropdown open left side menu */

    jQuery("li.dropdown-Nav").hover(function (e) {
        if ($("body").hasClass("MenuActive") == false) {
            jQuery(this).children('ul.dropdownMenu').toggle();
            e.stopPropagation();
        }
    });


    /* On click dropdown open left side menu */
    jQuery("li.dropdown-Nav").on("click", function (e) {
        if (jQuery("body").hasClass("MenuActive")) {
            if (screen.width < tabWith) {
                jQuery(this).siblings('li.dropdown-Nav').find('ul.dropdownMenu').hide();
            }
            jQuery(this).children('ul.dropdownMenu').toggle();
            //jQuery(this).siblings('li.dropdown-Nav').find('ul.dropdownMenu').show();
            e.stopPropagation();
        }
    });
    /** navbar search**/
    $("#search").on("keyup", function () {
        if (this.value.length > 0) {
            $("#navigation li").hide().filter(function () {
                return $(this).text().toLowerCase().indexOf($("#search").val().toLowerCase()) != -1;
            }).show();
        }
        else {
            $("#navigation li").show();
        }
    });


});

/******************************************Left Menu Events Ends******************************************/


/******************************************Top Menu Selected Starts******************************************/
function LeftMenuSelected() {
    var $nav = $("#navigation");
    var loc = location.pathname.toString().toLowerCase();
    loc = checkUrlForInnerpages(loc, $nav).toLowerCase();
    var current = $nav.find('a[href="' + loc + '"]');
    current.addClass("active");
    var t = current.parents("li.sidebar-nav-iten");

    if (t.length != 0) { //For Any level child
        t.addClass('activeLeftMenu');
        t.addClass('activeLeftMenu').children("a").addClass("activeLeftMenuParent");
        if (screen.width > tabWith) {

            current.parents("ul.dropdownMenu").show();
        }
        //return;
    }
    else if (current.length != 0) { // For No Child
        current.parents().addClass('activeLeftMenu');
        current.addClass('activeLeftMenu').children("a").addClass("activeLeftMenuParent");
        //return;
    }
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
    if (url.indexOf("/manageapplicationuser/edituser") != -1) {
        return "/manageapplicationuser/adduser";
    }

    if (url.indexOf("/managesystememailtemplate/editemailtemplate") != -1) {
        return "/managesystememailtemplate/viewemailtemplates";
    }
    if (url.indexOf("/question/editquestion") != -1) {
        return "/question/viewquestions";
    }
    if (url.indexOf("/manageadminemailtemplates/editemailtemplate") != -1) {
        return "/manageadminemailtemplates/viewemailtemplates";
    }
    if (url.indexOf("/test/edittest") != -1 || url.indexOf("/test/viewinvitedtestcandidates") != -1 || url.indexOf("/test/invitedtestcandidatetestresults")!=-1) {
        return "/test/viewtests";
    }  
    return orginalUrl;
}
/******************************************Top Menu Selected Ends******************************************/


$(function () {
    $(".navigation").find("li").has("ul").children("a").addClass("has-ul"), $(".navigation").find("li").not(".active").has("ul").children("ul").addClass("hidden-ul"), $(".navigation").find("li").has("ul").children("a").on("click", function (i) {
        i.preventDefault(), $(this).parent("li").not(".disabled").toggleClass("active").children("ul").slideToggle(250)
    }), $("#toggle-sidebar").click(function () {
        $(".sidebar").slideToggle("slow")
    }), $("#toggle-sidebar-desktop").click(function () {
        $(".sidebar").animate({
            width: "toggle"
        }, 250)
    }), $(".nav-tabs a").click(function (i) {
        i.preventDefault(), $(this).tab("show")
    })
});

/*Common Method for data-post-type='ajax' that just display messgaes STARTS */
function AjaxHitRunSuccessfully(data, form) {
    if (data.Success) {
        Success(data.Message);
    }
    else {
        ErrorBlock(data.Message);
    }
}
/*COmmon Method for data-post-type='ajax' that just display messgaes Ends*/

function UniversalTimeConvertor() {
    $('time').each(function () {
        //alert($(this).text() + " UTC");
        var date = new Date($(this).text() + " UTC");
        //alert(date.toString());
        //$(this).text(date.toString());


        //alert(date.format('M j, Y h:i A'));
        $(this).text(date.format('M j, Y'));
    });
}

Date.prototype.format = function (e) { var t = ""; var n = Date.replaceChars; for (var r = 0; r < e.length; r++) { var i = e.charAt(r); if (r - 1 >= 0 && e.charAt(r - 1) == "\\") { t += i } else if (n[i]) { t += n[i].call(this) } else if (i != "\\") { t += i } } return t }; Date.replaceChars = { shortMonths: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"], longMonths: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"], shortDays: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"], longDays: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"], d: function () { return (this.getDate() < 10 ? "0" : "") + this.getDate() }, D: function () { return Date.replaceChars.shortDays[this.getDay()] }, j: function () { return this.getDate() }, l: function () { return Date.replaceChars.longDays[this.getDay()] }, N: function () { return this.getDay() + 1 }, S: function () { return this.getDate() % 10 == 1 && this.getDate() != 11 ? "st" : this.getDate() % 10 == 2 && this.getDate() != 12 ? "nd" : this.getDate() % 10 == 3 && this.getDate() != 13 ? "rd" : "th" }, w: function () { return this.getDay() }, z: function () { var e = new Date(this.getFullYear(), 0, 1); return Math.ceil((this - e) / 864e5) }, W: function () { var e = new Date(this.getFullYear(), 0, 1); return Math.ceil(((this - e) / 864e5 + e.getDay() + 1) / 7) }, F: function () { return Date.replaceChars.longMonths[this.getMonth()] }, m: function () { return (this.getMonth() < 9 ? "0" : "") + (this.getMonth() + 1) }, M: function () { return Date.replaceChars.shortMonths[this.getMonth()] }, n: function () { return this.getMonth() + 1 }, t: function () { var e = new Date; return (new Date(e.getFullYear(), e.getMonth(), 0)).getDate() }, L: function () { var e = this.getFullYear(); return e % 400 == 0 || e % 100 != 0 && e % 4 == 0 }, o: function () { var e = new Date(this.valueOf()); e.setDate(e.getDate() - (this.getDay() + 6) % 7 + 3); return e.getFullYear() }, Y: function () { return this.getFullYear() }, y: function () { return ("" + this.getFullYear()).substr(2) }, a: function () { return this.getHours() < 12 ? "am" : "pm" }, A: function () { return this.getHours() < 12 ? "AM" : "PM" }, B: function () { return Math.floor(((this.getUTCHours() + 1) % 24 + this.getUTCMinutes() / 60 + this.getUTCSeconds() / 3600) * 1e3 / 24) }, g: function () { return this.getHours() % 12 || 12 }, G: function () { return this.getHours() }, h: function () { return ((this.getHours() % 12 || 12) < 10 ? "0" : "") + (this.getHours() % 12 || 12) }, H: function () { return (this.getHours() < 10 ? "0" : "") + this.getHours() }, i: function () { return (this.getMinutes() < 10 ? "0" : "") + this.getMinutes() }, s: function () { return (this.getSeconds() < 10 ? "0" : "") + this.getSeconds() }, u: function () { var e = this.getMilliseconds(); return (e < 10 ? "00" : e < 100 ? "0" : "") + e }, e: function () { return "Not Yet Supported" }, I: function () { var e = null; for (var t = 0; t < 12; ++t) { var n = new Date(this.getFullYear(), t, 1); var r = n.getTimezoneOffset(); if (e === null) e = r; else if (r < e) { e = r; break } else if (r > e) break } return this.getTimezoneOffset() == e | 0 }, O: function () { return (-this.getTimezoneOffset() < 0 ? "-" : "+") + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? "0" : "") + Math.abs(this.getTimezoneOffset() / 60) + "00" }, P: function () { return (-this.getTimezoneOffset() < 0 ? "-" : "+") + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? "0" : "") + Math.abs(this.getTimezoneOffset() / 60) + ":00" }, T: function () { var e = this.getMonth(); this.setMonth(0); var t = this.toTimeString().replace(/^.+ \(?([^\)]+)\)?$/, "$1"); this.setMonth(e); return t }, Z: function () { return -this.getTimezoneOffset() * 60 }, c: function () { return this.format("Y-m-d\\TH:i:sP") }, r: function () { return this.toString() }, U: function () { return this.getTime() / 1e3 } }
var K = function () {
    var a = navigator.userAgent;
    return {
        ie: a.match(/MSIE\s([^;]*)/)
    }
}();



/***************Add Edit Questions, Start****************/

function NewQuestionAddedSuccessfully(data, form) {
    if (data.Success) {
        Success(data.Message);
        $("#QuestionOptionsBody").find("tr").remove();
        $("#OptionsOrder").val(1);
        AddQuestionOptions();
    }
    else {
        ErrorBlock(data.Message);
    }
}

/***************Add Edit Questions, End****************/


/***************Add Edit Question Options Starts****************/
function AddQuestionOptions() {
    PostData("/question/_addquestionoptions", { displayOrder: $("#OptionsOrder").val() }, AddQuestionOptionsSuccess);
}
function AddQuestionOptionsSuccess(result) {
    $("#QuestionOptionsBody").append(result);
    $("#OptionsOrder").val(parseInt($("#OptionsOrder").val()) + 1);
    ResetUnobtrusiveValidation($("#addEditQuestion"));
}

function DelQuestionOptions(optionsDataId, delValue) {
    var tr = $("#tr" + optionsDataId);
    tr.find(".deleteOptions").val(delValue);
    if (delValue == true) {
        tr.find("td:not(.td-action)").addClass("disabled");
        tr.find(".isDeletedTrue").show();
        tr.find(".isDeletedFalse").hide();
        tr.find("input").attr("readOnly", true);
        tr.find("input:checkbox").on("click", function (e) {
            e.preventDefault();
        });
    }
    else {
        tr.find("td").removeClass("disabled");
        tr.find(".isDeletedTrue").hide();
        tr.find(".isDeletedFalse").show();
        tr.find(":input").attr("readOnly", false);
        tr.find("input:checkbox").unbind('click');
    }
}
/***************Add Edit Question Options Ends****************/

/*Assign System Email Template to Candidate Starts*/
function AssignEmailTemplateToCompanySuccess(result) {
    SuccessWithClosePopUp(result.Message, false);
}
/*Assign System Email Template to Candidate Ends*/
function UniversalDateTimeConvertor() {
    $('dateTime').each(function () {
        var dateTime = new Date($(this).text() + " UTC");
        $(this).text(dateTime.format('M j, Y h:i A'));
    });
}



function EditProfileSuccess(data, form) {
    if (data.Success) {
        Success(data.Message);
        GetControllerScope('nav', 'TopNavController').loadTopPanelUserDetails();
    }
    else {
        ErrorBlock(data.Message);
    }
}

function CloseLookUpDomainForm(lookUpCode) {
    GetControllerScope('div', 'LookUpController').GetLookUpValue(lookUpCode);

}