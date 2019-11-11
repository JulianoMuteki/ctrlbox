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
                            "sType": "html",
                            "bSortable": true,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.BoxType.PictureID === '' || data.BoxType.PictureID === null) {
                                        return '<img style="width:15px; height: 15px;"  src="/../img/avatar.png" />' + data.BoxType.Name ;
                                    }
                                    return '<img  src="/../Configuration/ViewImage/' + data.BoxType.PictureID + '" /> ' + data.BoxType.Name ;
                                }
                               
                                else if (type === 'sort') {
                                    console.log(data);
                                    return data.BoxType.Name;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": "BoxBarcode.BarcodeEAN13",
                            "defaultContent": "<i>not found</i>",
                            "bSortable": false
                        },
                        {
                            "mData": "Description",
                            "defaultContent": "<i>not found</i>",
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "width": "8%",
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    return '  <div class="progress progress-striped"><div style="width: ' + data.PorcentFull + '%;" class="bar"></div></div>';
                                }
                                return data;
                            }                         
                        },
                        {
                            "mData": "Status",
                            "defaultContent": "<i>--</i>",
                        },
                        {
                            "mData": "Product.Name",
                            "defaultContent": "<i>--</i>",
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    return '<a href="/Box/ViewBoxes?boxFatherID=' + data.DT_RowId + '" class="edit"><i class="icon-external-link"></i> Children</a>';
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    return '<a href="/Tracking/Index?boxID=' + data.DT_RowId + '" class="edit"><i class="icon-external-link"></i> Tracking</a>';
                                }
                                return data;
                            }
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