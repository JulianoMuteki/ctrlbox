var ddlProduct = "#ddlProduct";

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
                        alert("Complete");
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

            });
        }
    };
}();