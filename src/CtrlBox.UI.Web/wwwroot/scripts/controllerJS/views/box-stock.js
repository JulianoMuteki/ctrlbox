var ddlProduct = "#ddlProduct";
var ddlTrackingType = '#ddlTrackingType';
var ddlClient = '#ddlClient';
var txtQuantity = '#txtQuantity';

var BoxStock = function () {

    function getTotalProductItemByClientIDAndProductID() {
        var _productID = $(ddlProduct).val();
        var _clientID = $(ddlClient).val();

        $.ajax({
            url: '../Box/GetAjaxHandlerAvailableProductItemsByClientIDAndProductID',
            type: 'POST',
            dataType: 'json',
            data: { productID: _productID, clientID: _clientID },
            "success": function (json) {
                if (!json.NotAuthorized) {
                    $('#txtAvailableChild').val(json.aaData.length);
                    console.log(json);
                }
            },
            "error": handleAjaxError
        });
    }

    return {
        init: function () {
            jQuery(document).ready(function () {
                $('.select2_option').select2({
                    allowClear: true
                });

                $('.radio input[type=radio][name=HasProduct]').change(function () {
                    var hasProduct = $(".radio input[type=radio][id=idHasProduct]").is(":checked");
                    $('#txtAvailableChild').val('');
                    if (hasProduct) {
                        $("#grpProducts").show();
                        $("#grpBoxesChild").hide();
                    } else {
                        $("#grpProducts").hide();
                        $("#grpBoxesChild").show();
                    }
                });

                $(ddlProduct).change(function (event) {
                    if ($(this).val() !== '0') {
                        getTotalProductItemByClientIDAndProductID();
                    }
                });

                $(ddlClient).change(function (event) {
                    var hasProduct = $(".radio input[type=radio][id=idHasProduct]").is(":checked");

                    if ($(this).val() !== '0' && hasProduct && $('#ddlProduct').val() !== '0') {
                        getTotalProductItemByClientIDAndProductID();
                    }
                });

                $('#btnSubmit').click(function () {
                    var _productID = $(ddlProduct).val();
                    var _clientID = $(ddlClient).val();
                    var _trackingTypeID = $(ddlTrackingType).val();
                    var _quantity = $(txtQuantity).val();

                    $.ajax({
                        url: '../Product/PostAjaxHandlerAddStockProduct',
                        type: 'POST',
                        dataType: 'json',
                        data: { productID: _productID, clientID: _clientID, trackingTypeID: _trackingTypeID, quantity: _quantity },
                        "success": function (json) {
                            if (!json.NotAuthorized) {
                                alert('Completo');
                                //  window.history.back();
                            }
                        },
                        "error": handleAjaxError
                    });

                });
            });
        }
    };
}();