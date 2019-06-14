
var ProductStock = function () {
    return {
        init: function (stockID) {
            var _stockID = stockID;
            var list = [];
            (function ($) {
                $.fn.serializeFormJSON = function () {
                   
                    var a = this.serializeArray();
                    $.each(a, function () {
                        if (this.value != "") {
                            list.push({ "StockID": _stockID, "ProductID": this.name, "Amount": this.value });
                        }
                    });
                    return JSON.stringify(list);
                };
            })(jQuery);

            $('#btnSubmit').click(function () {
                var oTableAdd = $('#tbProductStock').dataTable();
                var sData = oTableAdd.$('input').serializeFormJSON();
               
                if (list.length > 0) {
                    $.ajax({
                        url: "PutAjaxHandlerProductStock",
                        type: 'POST',
                        dataType: 'json',
                        data: { tbProducts: sData },
                        "success": function (json) {
                            if (!json.NotAuthorized) {
                                alert('Complete');
                                window.history.back();
                            }
                        }
                    }); 
                } else
                {
                    alert("There are not products selected");
                }

                return false;
            });

            var url = "GetAjaxHandlerProductStock";
            var oTable = $('#tbProductStock').dataTable({
                "sAjaxSource": url,
                "bProcessing": true,
                "bDestroy": true,
                "aoColumns": [
                    {
                        "mData": "ProductName",
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "bSortable": false,
                        sClass: "calc",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<input type="text" placeholder="' + row.Amount + '" class="m-wrap small" name="' + row.ProductID + '" />'
                            }
                            return data;
                        }
                    }
                ],
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });
        }
    };
}();