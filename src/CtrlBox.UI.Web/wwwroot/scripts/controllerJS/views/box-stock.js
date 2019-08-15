var ddlProduct = "#ddlProduct";
var ddlTrackingType = '#ddlTrackingType';
var ddlClient = '#ddlClient';
var txtQuantity = '#txtQuantity';
var ddlBoxesTypeChild = '#ddlBoxesTypeChild';

var BoxStock = function () {

    function getTotalProductItemByClientIDAndProductID() {
        var _productID = $(ddlProduct).val();
        var _clientID = $(ddlClient).val();

        $.ajax({
            url: '../Box/GetAjaxHandlerAvailableProductItemsByClientIDAndProductID',
            type: 'GET',
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

    function getTotalAvailableBoxesByBoxTypeIDAndProductID() {
        var _boxTypeID = $(ddlBoxesTypeChild).val();
        var _clientID = $(ddlClient).val();

        $.ajax({
            url: '../Box/GetAjaxHandlerAvailableBoxesByBoxTypeIDAndProductID',
            type: 'GET',
            dataType: 'json',
            data: { boxTypeID: _boxTypeID, clientID: _clientID },
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

                $(ddlBoxesTypeChild).change(function (event) {
                    if ($(this).val() !== '0') {
                        getTotalAvailableBoxesByBoxTypeIDAndProductID();
                    }
                });

                $(ddlClient).change(function (event) {
                    var hasProduct = $(".radio input[type=radio][id=idHasProduct]").is(":checked");

                    if ($(this).val() !== '0' && hasProduct && $('#ddlProduct').val() !== '0') {
                        getTotalProductItemByClientIDAndProductID();
                    }
                });

                $('#btnSubmit').click(function () {
                    var _boxTypeID = $(ddlBoxesType).val();
                    var _clientID = $(ddlClient).val();
                    var _trackingTypeID = $(ddlTrackingType).val();
                    var _quantity = $(txtQuantity).val();

                    var hasProduct = $(".radio input[type=radio][id=idHasProduct]").is(":checked");

                    if (hasProduct) {
                        var _productID = $(ddlProduct).val();

                        $.ajax({
                            url: '../Box/PostAjaxHandlerAddBoxStockWithProductItems',
                            type: 'POST',
                            dataType: 'json',
                            data: { boxTypeID: _boxTypeID, productID: _productID, clientID: _clientID, trackingTypeID: _trackingTypeID, quantity: _quantity },
                            "success": function (json) {
                                if (json.success) {
                                    alert('Completo');
                                    window.history.back();
                                }
                            },
                            "error": handleAjaxError
                        });

                    } else {
                        var _boxTypeChildID = $(ddlBoxesTypeChild).val();

                        $.ajax({
                            url: '../Box/PostAjaxHandlerAddBoxStockWithBoxes',
                            type: 'POST',
                            dataType: 'json',
                            data: { boxTypeID: _boxTypeID, productID: _productID, clientID: _clientID, trackingTypeID: _trackingTypeID, boxTypeChildID: _boxTypeChildID, quantity: _quantity },
                            "success": function (json) {
                                if (json.success) {
                                    alert('Completo');
                                    window.history.back();
                                }
                            },
                            "error": handleAjaxError
                        });
                    }
                });
            });
        }
    };
}();