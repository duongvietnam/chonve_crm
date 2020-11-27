/////const
const TT_THEM_MOI = 1;
const TT_CAP_NHAT = 2;
const TT_DUYET = 3;
const TT_HUY = 4;
/////////////////////

 
// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};
function showThrobber(message) {
    $('.throbber-header').html(message);
    window.setTimeout(function () {
        $(".throbber").show();
    }, 1000);
}
function ShowSuccess(msg) {
    toastr.success(msg);
}
function ShowWarning(msg) {
    toastr.warning(msg);
} function ShowError(msg) {
    toastr.error(msg);
}
//hien thi cac loi validate cho control 
function display_valid_ctl(cltid,hasErr, msgerr) {
    var keyCtl = $("#" + cltid).attr('name');
    var validationMessageElement = $('span[data-valmsg-for="' + keyCtl + '"]');    
    if (hasErr) {
        validationMessageElement.removeClass('field-validation-valid');
        validationMessageElement.addClass('field-validation-error');
        validationMessageElement.html(msgerr);
        $("#" + cltid).removeClass('is-valid');
        $("#" + cltid).addClass('is-invalid');
        $("#" + cltid).focus();
    }
    else {
        validationMessageElement.addClass('field-validation-valid');
        validationMessageElement.removeClass('field-validation-error');
        validationMessageElement.html("");
        $("#" + cltid).removeClass('is-invalid');
        $("#" + cltid).addClass('is-valid');
        
    }
    
}
//hien thi cac loi validate tren form luc dung ajax de xu ly
function display_valid_error(e) {
    if (e) {
        //clear all error before
        $('span[data-valmsg-for]').each(function () {
            $(this).removeClass("field-validation-error").addClass("field-validation-valid");
            $(this).html("");
            var keyCtl = $(this).data("valmsg-for");
            keyCtl = keyCtl.replace(/./g, "_");
            $("#" + keyCtl).removeClass('is-invalid');
        });
        if ((typeof e) == 'string') {
            //single error
            //display the message
            ShowError(e);
        } else {
            
            //create a message containing all errors.
            var isSetFocus = false;
            $.each(e, function (key, value) {
                if (value.errors) {
                    var validationMessageElement = $('span[data-valmsg-for="' + key + '"]');
                    validationMessageElement.removeClass('field-validation-valid');
                    validationMessageElement.addClass('field-validation-error');
                    validationMessageElement.html(value.errors.join("<br />"));
                    key = key.replace(/./g, "_");
                    $("#" + key).addClass('is-invalid');
                    if (!isSetFocus) {
                        isSetFocus = true;
                        $("#" + key).focus();
                    }

                }
            });
            //display the message
        }
        //ignore empty error
    } else if (e.errorThrown) {
        ShowError('Error happened');
    }
}
// Ajax activity indicator bound to ajax start/stop document events
$(document).ajaxStart(function () {
    $('#ajaxBusy').show();
}).ajaxStop(function () {
    $('#ajaxBusy').hide();
});
function ShowChangePassword() {
    OpenModalManual("Thay đổi mật khẩu", "/Customer/_ChangePassword", "modal-lg");
}
function ShowViewLoading(ctl) {
    $(ctl).html($("#partialViewLoading").html());
}
function OpenModalManual(title, src, classType) {
    
    var modal = $('#globalModalIframe');
    ShowViewLoading("#globalModalIframeContent");
    modal.find('.modal-title').text(title);
    modal.find('.modal-dialog').removeClass("modal-lg").removeClass("modal-xl").removeClass("modal-sm").addClass(classType);
    $.ajax({
        type: "GET",
        url: src,
        success: function (data) {
            $("#globalModalIframeContent").html(data);
        },
    });
    modal.modal('show');
}
function CloseModalManual() {
    $("#globalModalIframeContent").html("");
    $('#globalModalIframe').modal("hide");
}
function DeleteWorkFile(fileId) {
    if (fileId == 0)
        return;
    var postData = { Id: fileId };
    $.ajax({
        cache: false,
        type: "POST",
        url: "/Workfiles/Delete?Id=" + fileId,
        data: postData,
        complete: function (data) {

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        },
        traditional: true
    });
}

function SetSelectDonVi(Id, LoaiId, Ten,callbackfn) {
    curSelectDonVi.Id = Id;
    curSelectDonVi.LoaiId = LoaiId;
    curSelectDonVi.Ten = Ten;
    if (typeof callbackfn === "function") {
        callbackfn();
    }
}
//chuyen doi date text co format: dd/MM/yyyy ra date cua javascript
Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}
Date.prototype.addMonths = function (months) {
    var date = new Date(this.valueOf());
    date.setMonth(date.getMonth() + months);
    return date;
}
Date.prototype.toVNDateString = function ()
{
    var d = new Date(this.valueOf());
    return ("0" + (d.getDate())).slice(-2) + "/" + ("0" + (d.getMonth() + 1)).slice(-2) + "/" + d.getFullYear();
}
Date.prototype.toVNDateTimeString = function () {
    var d = new Date(this.valueOf());
    return ("0" + (d.getDate())).slice(-2) + "/" + ("0" + (d.getMonth() + 1)).slice(-2) + "/" + d.getFullYear() + " " + ("0" + (d.getHours())).slice(-2) + ":" + ("0" + (d.getMinutes())).slice(-2);
}
Date.prototype.toSysDateString = function () {
    var d = new Date(this.valueOf());
    return d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2)+"-"+ ("0" + (d.getDate())).slice(-2);
}
function ToDate(strdate) {
    if (!strdate.length)
        return null;
   return new Date(strdate.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"));
}
/**
 * Number.prototype.format(n, x, s, c)
 * 
 * @param integer n: length of decimal
 * @param integer x: length of whole part
 * @param mixed   s: sections delimiter
 * @param mixed   c: decimal delimiter
 */
Number.prototype.format = function (n, x, s, c) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
};
Number.prototype.formatNum = function () {
    return this.format(0, 3, '.', ',');
};
Number.prototype.formatFloat2 = function () {
    return this.format(2, 3, '.', ',');
};
Number.prototype.formatFloat1 = function () {
    return this.format(1, 3, '.', ',');
};
Number.prototype.formatPrice = function () {
    return this.format(0, 3, '.', ',') +" ₫";
};
//12345678.9.format(2, 3, '.', ',');  // "12.345.678,90"
//123456.789.format(4, 4, ' ', ':');  // "12 3456:7890"
//12345678.9.format(0, 3, '-');       // "12-345-679"
