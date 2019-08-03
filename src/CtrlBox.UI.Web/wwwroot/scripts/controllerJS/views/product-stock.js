var ddlProduct = "#ddlProduct";
var ddlTrackingType = '#ddlTrackingType';
var ddlClient = '#ddlClient';
var txtQuantity = '#txtQuantity';

var ProductStock = function () {

    function getTotalProductItemByClientIDAndProductID() {
        var _productID = $(ddlProduct).val();
            $.ajax({
                url: '../Product/GetTotalProductItemByProductID',
                type: 'POST',
                dataType: 'json',
                data: { productID: _productID },
                "success": function (json) {
                    if (!json.NotAuthorized) {
                        $('#txtTotalProductItem').val(json.aaData);
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

                $(ddlProduct).change(function (event) {
                    if ($(this).val() !== '0') {
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