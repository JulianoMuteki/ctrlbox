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
                if (!json.NotAuthorized) {
                    setModalAddress(json.aaData);
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
                        window.history.back();
                    }
                },
                "error": handleAjaxError
            });
        }
    };
}();