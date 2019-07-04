var TableProduct = function () {

    return {

        //main function to initiate the module
        init: function (url) {

            function elementEdit() {
                return '<a class="edit" href="javascript:;"><i class="icon-edit"></i> Edit</a>';
            }
            function elementDelete() {
                return '<a class="delete" href="javascript:;"><i class="icon-trash"></i> Delete</a>';
            }

            var oTable = $('#sample_editable_1').dataTable({
                "sAjaxSource": url,
                "bProcessing": true,
                "bDestroy": true,
                "aoColumns": [
                    {
                        "mData": "Name"
                    },
                    {
                        "mData": "Weight"
                    },
                    {
                        "mData": "Description"
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "bSortable": false,
                        "sClass": "calc",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                var statusStock = "green-stripe";
                                if (data.StockTotal <= 0) {
                                    statusStock = "red-stripe"
                                }
                                return '<a href="#" class="btn mini ' + statusStock + '">Total: ' + data.StockTotal + '</a> <span class="bold">' + data.UnitMeasure + '</span>';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<a href="/Product/Create?productID=' + data.DT_RowId + '" class="edit"><i class="icon-edit"></i> Edit</a>';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<a class="delete" href="javascript:;"><i class="icon-trash"></i> Delete</a>';
                            }
                            return data;
                        }
                    }
                ]
            });
        }

    };

}();