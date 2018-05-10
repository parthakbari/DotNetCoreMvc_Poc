var MakeDetailUrl = BASEPATHURL + "Vehicle/GetMakeList";
var ModelDetailUrl = BASEPATHURL + "Vehicle/GetModelVariantList";
var ErrorUrl = BASEPATHURL + "Vehicle/Error";
var GenerateErrorUrl = BASEPATHURL + "Vehicle/GenerateError";
var LoadVehicleBasicDetail = BASEPATHURL + "Vehicle/LoadVehicleBasicDetail";

$("#btnGet").click(function () {
    AjaxCall({
        url: MakeDetailUrl, dataType: 'html', httpMethod: 'GET',
        successCallBackFunction: 'OnSuccessMakeDetail'
    });
});

function OnSuccessMakeDetail(response) {
    $("#makemodellist").show();
    $("#makemodellist").html(response);
    $("#makemodellist").on('click', '.list-group-item', SelectMake);
}

function SelectMake() {
    var selectedvalue = $(this).attr("value");
    var selectedname = $(this).attr("name");
    console.log(selectedvalue + "___" + selectedname);
    LoadVehicleBasicDetailMethod("MD", selectedvalue, selectedname);
}

function FillVehicleDetailPage() {

}

function LoadVehicleBasicDetailMethod(mode, selectedvalue, selectedname) {
    AjaxCall({
        url: LoadVehicleBasicDetail, postData: { mode: mode, value: selectedvalue, name: selectedname }, dataType: 'html', httpMethod: 'GET',
        successCallBackFunction: 'NextVehicleDetailScreen'
    });
}

function NextVehicleDetailScreen(response) {
    $("#makemodellist").html(response);
}

$("#btnGenerateError").click(function () {
    AjaxCall({
        url: GenerateErrorUrl, dataType: 'Json', httpMethod: 'GET'
    });
});

function AjaxCall(options) {
    $.ajax({
        type: options.httpMethod,
        url: options.url,
        data: options.postData,
        cache: options.cache == undefined ? true : options.cache,
        global: options.showLoading == undefined ? true : options.showLoading,
        dataType: options.dataType,
        contentType: options.contentType == undefined ? "application/x-www-form-urlencoded;charset=UTF-8" : options.contentType,
        async: options.isAsync == undefined ? true : options.isAsync,
        success: function (data) {
            if (data.status != undefined && (data.status == "VALIDATION_ERROR" || data.status == 206)) { // handle server side errors
                IsPPartialloader = false;
                // ShowServerErrors(data.Data);
            }
            else if ((data.status != undefined && data.status == "SESSION_TIMEOUT") || data == "{\"status\":\"SESSION_TIMEOUT\"}") { // handle request time-out                                                
                //RedirectToSessionTimeoutORError(SessionTimeOutUrl);
            }
            else if (data.status != undefined && data.status == "UNAUTHORIZED") { // handle request time-out
                window.location = UnAuthorizedUrl;
            }
            else if ((data.status != undefined && data.status == "ERROR") || data == "{\"status\":\"ERROR\"}") { // handle request error
                window.location = ErrorUrl;
            }
            else if (data.status != undefined && data.status == "THIRD_PARTY_SERVICE_ERROR") { //Handle third party service error.
                // RedirectToSessionTimeoutORError(ErrorUrl);
            }
            else {
                if (typeof (options.successCallBackFunction) != "undefined" && options.successCallBackFunction != null && options.successCallBackFunction != '') {
                    if (options.params != undefined) {
                        eval("new function () {" + options.successCallBackFunction + "(data,'" + options.params + "')}");
                    }
                    else {
                        eval(options.successCallBackFunction + '(data)');
                    }
                }
                else {
                    returnVal = data.Data;
                }
            }
        },
        error: function (xhr, textstatus, errorThrown) {
            try {
                if (!UserAborted(xhr)) {
                    if (xhr.status == 500 || xhr.status == 200) { //Error while any method execution.                        
                        //RedirectToSessionTimeoutORError(ErrorUrl);
                    }
                    if (xhr.status == 408) { // Request Time Out - redirect to login
                        // RedirectToSessionTimeoutORError(SessionTimeOutUrl);
                    }
                    else {
                        //window.location = ExceptionUrl;
                    }
                }
            }
            catch (e) {
                window.location = ErrorUrl;
            }
        }
    });
}

// invalid user exception checking
function UserAborted(xhr) {
    return !xhr.getAllResponseHeaders();
}
