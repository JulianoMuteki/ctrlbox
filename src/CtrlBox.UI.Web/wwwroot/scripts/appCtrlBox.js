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

    function putFinalizeDelivery(deliveryID) {

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
            $.ajax({
                url: 'Delivery/PutAjaxHandlerFinalizeDelivery',
                type: 'POST',
                dataType: 'json',
                data: { deliveryID: id },
                "success": function (json) {
                    if (!json.NotAuthorized) {
                        alert('Completo');
                        window.location.reload(false);
                    }
                },
                "error": handleAjaxError
            });
        }
    };
}();