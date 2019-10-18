var StockMovementsTable = function () {
    var oTableBox;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes(stockID) {
        oTableBox = $('#tbStockMovements').dataTable({
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "stockID", "value": stockID });
            },
            "sAjaxSource": '../Product/GetAjaxHandlerStockMovements',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "ClientSupplier.Name",
                            "bSortable": false
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": true,
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.Stock.Product.Picture === '' || data.Stock.Product.Picture === null) {
                                        return '<img style="width:25px; height: 25px;" src="/../img/avatar.png" />  ' + data.Stock.Product.Name;
                                    }
                                    return '<img style="max-width:25px; max-height: 25px;" src="' + data.Stock.Product.Picture.SrcBase64Image + '" />  ' + data.Stock.Product.Name;
                                }
                                else if (type === 'filter') {
                                    return data.Stock.Product.Name;
                                }
                                else if (type === 'sort') {
                                    return data.Stock.Product.Name;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": "UnitPrice",
                            "bSortable": false
                        },
                        {
                            "mData": "TotalValue"
                        },
                        {
                            "mData": "Amount",
                            "bSortable": false,
                        },
                        {
                            "mData": "StockType",
                            "bSortable": false,
                            "defaultContent": "<i>Edit Not set</i>",
                        }
            ]
        });
    }

    return {
        //main function to initiate the module
        init: function (stockID) {
            initPage(stockID);
        }
    };
}();