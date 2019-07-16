var _clientID = '';
var _routeID = '';
var _deliveryID = '';

var BoxComponents = function () {
    var oTableBox;

    function initPage() {
        inicializateComponentes();     
    }

    function inicializateComponentes() {
        $.extend($.inputmask.defaults, {
            'autounmask': true
        });

        $('.select2_option').select2({
            allowClear: true
        });

        if (jQuery().datepicker) {
            $('.date-picker').datepicker({
                rtl: App.isRTL(),
                format: 'dd-mm-yyyy'
            });
        }

        oTableSale = $('#tbSale').dataTable({
            "sAjaxSource": 'Sale/GetAjaxHandlerExecuteSale',
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "clienteID", "value": _clientID });
                aoData.push({ "name": "linhaID", "value": _routeID });
                aoData.push({ "name": "deliveryID", "value": _deliveryID });
            },
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "NomeProduto",
                            "bSortable": false
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "sClass": "calc",
                            "defaultContent": "<i>Not set</i>",
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    var statusStock = "green-stripe";
                                    if (data.Amount <= 5) {
                                        statusStock = "red-stripe"
                                    }
                                    return '<span class="btn mini ' + statusStock + '">Total: ' + data.Amount + ' ' + data.UnitMeasure + '</span>';
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "sClass": "calc",
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    var link = '<input type="text" placeholder="0" class="m-wrap small qtdeVenda"> ' + '<span class="label label-danger">' + data.UnitMeasure + '</span>';
                                    if (data.Amount == 0) {
                                        link = '<span class="label label-important">finished products</span>'
                                    }

                                    return link;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "sClass": "calc",
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    var link = '<input type="text" placeholder="0" class="m-wrap small discountValueSale">';
                                    if (data.Amount == 0) {
                                        link = '<span class="label label-important">finished products</span>'
                                    }
                                    return link;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": "ValorProduto"
                        },
                        {
                            "mData": "Total",
                            "sClass": "somaTotal",
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