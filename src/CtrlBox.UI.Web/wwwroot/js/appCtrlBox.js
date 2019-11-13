Number.prototype.formatMoney = function (places, symbol, thousand, decimal) {
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "R$ ";
    thousand = thousand || ".";
    decimal = decimal || ",";
    var number = this,
        negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
};

function handleAjaxNotificationModal(controller, data) {
    if (data.responseJSON.HasNotification) {
        var notify = data.responseJSON.notificationsJSON;
        var url = "/" + controller + "/GetPartialViewNotifications";
        $.ajax({
            url: url,
            type: 'POST',
            cache: false,
            async: true,
            dataType: "html",
            data: { notifications: notify },
        })
           .done(function (result) {
               $('#div1').html(result);
           }).fail(function (xhr) {
               console.log('error : ' + xhr.status + ' - '
               + xhr.statusText + ' - ' + xhr.responseText);
           });
    }
}
function handleAjaxError(xhr, textStatus, error) {

    var resultException = xhr.responseJSON;
    if (resultException !== undefined || resultException !== null) {

        var responseHTML = '';
        responseHTML = '<p>' + resultException.errorCode + '</p>';
        responseHTML += '<p>' + resultException.InnerEx + '</p>';
        responseHTML += '<p>' + resultException.stackTrace + '</p>';

        $('.accordion-inner').html(responseHTML);
        $("#titleError").text(resultException.errorMessage);
        $("#modalError").modal();
        return false;
    }

    var responseText = "";

    if (xhr.jqXHR != undefined) {
        responseText = xhr.jqXHR.responseText;
    }
    else {
        responseText = xhr.responseText;
        if (responseText !== '') {
            var obj = $($.parseHTML(responseText)).filter('div.titleerror');
            console.log(obj.text());
            $('.accordion-inner').html(responseText);
            $("#titleError").text(obj.text());
            $("#modalError").modal();
        }

        if (textStatus === 'timeout') {
            //  alert('The server took too long to send the data.');
        }
        else if (textStatus == 'error') {
            $('.accordion-inner').html(xhr.status);
            $("#titleError").text(error);
            $("#modalError").modal();
        } else {

        }
    }

    return false;
}

var AppCtrlBox = function () {
    function setModalAddress(entityAddress) {
        $("#street").html(entityAddress.Street + ', Nº: ' + entityAddress.Number);
        $("#postCode").html(entityAddress.CEP);
        $("#postCodeLI").text(entityAddress.CEP);
        $("#city").html(entityAddress.City);
        $("#District").html(entityAddress.District);
        $("#state").html(entityAddress.Estate);
        $("#reference").html(entityAddress.Reference);

        $('#modalAddress').modal('show');
    }

    function getAddress(id) {

        $.ajax({
            url: '/../../Address/GetAjaxHandlerAddressByID',
            type: 'Get',
            dataType: 'json',
            data: { addressID: id },
            "success": function (json) {
                if (json.success) {
                    setModalAddress(json.aaData);
                }
                else {
                    alert("Address not found!");
                }
            },
            "error": handleAjaxError
        });
    }

    function putFinalizeDelivery(orderID) {
        $.ajax({
            url: 'Delivery/PostAjaxHanblerFinishDelivery',
            type: 'POST',
            dataType: 'json',
            data: { orderID: orderID },
            "success": function (json) {
                if (json.success) {
                    alert('Completo');
                    window.location.reload(true);
                }
            },
            "error": handleAjaxError
        });
    }
    return {
        callModalAddress: function (addressID) {
            getAddress(addressID);
        },

        callModalFinalizeDelivery: function (deliveryID) {
            $("#btnCloseDelivery").val(deliveryID);

            $.ajax({
                url: 'Delivery/GetTableAjaxHandlerResumeDelivery',
                type: 'GET',
                dataType: 'json',
                data: { deliveryID: deliveryID },
                "success": function (json) {
                    if (json.success) {
                        $("#totalSale").html("<i class=\"icon-money\"></i> Total value sale: " + json.aaData.TotalSale.formatMoney());
                        $("#totalProduct").html("<i class=\"icon-list\"></i> Total Products Items: " + json.aaData.TotalProducts);
                        $("#startDate").html("Start delivery: " + json.aaData.StartDate);
                    }
                },
                "error": handleAjaxError
            });

            $('#modalFinalizeDelivery').modal('show');
        },
        putFinalizeDelivery: function (button) {
            var id = $(button).val();
            putFinalizeDelivery(id)
        },

    };
}();