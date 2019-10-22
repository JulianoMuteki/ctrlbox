var ddlProduct = "#ddlProduct";
var ddlTrackingType = '#ddlTrackingType';
var ddlClient = '#ddlClient';
var txtQuantity = '#txtQuantity';

var CreateStock = function () {

   return {
        init: function () {
            jQuery(document).ready(function () {
                $('.select2_option').select2({
                    allowClear: true
                });

                
            });
        }
    };
}();