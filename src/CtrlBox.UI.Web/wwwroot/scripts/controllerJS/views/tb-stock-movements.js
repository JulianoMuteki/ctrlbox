var _stockID;

var StockMovementsTable = function () {
    var oTableBox;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        oTableBox = $('#tbStockMovements').dataTable({
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "stockID", "value": _stockID });
            },
            "sAjaxSource": '../Product/GetAjaxHandlerStocksMovements',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
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
                            "mData": "ClientSupplier.Name"
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
                            "mData": "StockType"
                        }
            ]
        });
    }

    return {
        //main function to initiate the module
        init: function (stockID) {
            _stockID = stockID;
            initPage();
        }
    };
}();