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
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.BoxType.PictureID === '' || data.BoxType.PictureID === null) {
                                        return '<img style="width:15px; height: 15px;"  src="/../img/avatar.png" />' + data.BoxType.Name ;
                                    }
                                    return '<img  src="/../Configuration/ViewImage/' + data.BoxType.PictureID + '" /> ' + data.BoxType.Name ;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": "Barcode",
                            "bSortable": false
                        },
                        {
                            "mData": "Description"
                        },
                        {
                            "mData": "Product.Name",
                            "defaultContent": "<i>not applicable</i>",
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