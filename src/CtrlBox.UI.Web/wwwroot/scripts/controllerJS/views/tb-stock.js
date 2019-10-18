var StocksTable = function () {
    var oTableBox;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        oTableBox = $('#tbStocks').dataTable({
            "sAjaxSource": '../Product/GetAjaxHandlerStocks',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "StorageLocation.Name",
                            "bSortable": false
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": true,
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.PictureID === '' || data.PictureID === null) {
                                        return '<img style="width:25px; height: 25px;" src="/../img/avatar.png" />  ' + data.Name;
                                    }
                                    return '<img style="max-width:25px; max-height: 25px;" src="' + data.Product.Picture.SrcBase64Image + '" />  ' + data.Product.Name;
                                }
                                else if (type === 'filter') {
                                    return data.Product.Name;
                                }
                                else if (type === 'sort') {
                                    return data.Product.Name;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": "Minimum",
                            "bSortable": false
                        },
                        {
                            "mData": "TotalStock"
                        },
                        {
                            "mData": "DefaultPrice",
                            "bSortable": false,
                        },
                        {
                            "bSortable": false,
                            "defaultContent": "<i>Edit Not set</i>",
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
            $('#tbBoxesType').dataTable();

        }
    };
}();