var _clientID = '';
var _routeID = '';
var _deliveryID = '';

var BoxComponents = function () {
    var oTableBox;

    function initPage() {
        inicializateComponentes();     
    }

    function inicializateComponentes() {

        oTableSale = $('#tbBox').dataTable({
            "sAjaxSource": 'Box/GetAjaxHandlerBoxes',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "Barcode",
                            "bSortable": false
                        },
                        {
                            "mData": "Description"
                        },
                        {
                            "mData": "BoxType.Name"
                        },
                        {
                            "mData": "Product.Name"
                        }
            ]

        });
    }

    return {
        //main function to initiate the module
        init: function () {
            initPage();
        },
        readOnly: function () {
            $('#tbBox').dataTable();
          
        }
    };
}();