var _clientID = '';
var _orderID = '';

var MakeDeliveryComponents = function () {
    var oTable;
    var order = {};

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        oTable = $('#tbProductItems').dataTable({
            "sAjaxSource": '../Delivery/GetAjaxHandlerMakeDelivery',
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "clientID", "value": _clientID });
                aoData.push({ "name": "deliveryID", "value": _orderID });
            }, 
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.PictureID === '' || data.PictureID === null) {
                                        return '</span><img style="width:15px; height: 15px;"  src="/../img/avatar.png" /> ' + data.NomeProduto;
                                    }
                                    return '<div><img  src="/../Configuration/ViewImage/' + data.PictureID + '" /><a class="showDetails row-details-close" style="display: inline-block;margin-left: 10%;">' + data.NomeProduto + ' <i id="iconDetails" class="m-icon-swapdown m-icon-black"></i></a></div>';
                                }
                                return data;
                            }
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
                                    if (data.TotalBox <= 5) {
                                        statusStock = "red-stripe"
                                    }
                                    return '<span class="btn mini ' + statusStock + '">Total loaded: ' + data.TotalBox + '</span>';
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
                                    var link = '<input type="text" placeholder="0" class="m-wrap small qtdeVenda"> ' + '<span class="label label-danger"> product Items</span>';
                                    if (data.TotalBox == 0) {
                                        link = '<span class="label label-important">finished products</span>'
                                    }

                                    return link;
                                }
                                return data;
                            }
                        }

            ]

        });

        $('#tbProductItems').on('click', ' tbody td .showDetails', function () {
            var nTr = $(this).parents('tr')[0];
            if (oTable.fnIsOpen(nTr)) {
                /* This row is already open - close it */
                $(this).find("#iconDetails").addClass("m-icon-swapdown").removeClass("m-icon-swapup");
                oTable.fnClose(nTr);
            }
            else {
                /* Open this row */
                $(this).find("#iconDetails").addClass("m-icon-swapup").removeClass("m-icon-swapdown");
                oTable.fnOpen(nTr, fnFormatDetails(oTable, nTr), 'details');
            }
        });

        $(".btnSubmit").click(function () {

            order.DT_RowId = _orderID;

            var tbDeliveriesDetails = [];
            $.each(oTable.fnGetNodes(), function (index, value) {
                var row = oTable.fnGetData(value);
                var deliveryDetail = {};

                var qtdeVenda = $(value).find('input.qtdeVenda').val();
                qtdeVenda = qtdeVenda || 0;
                deliveryDetail.ClientID = _clientID;
                deliveryDetail.QuantityProductItem = qtdeVenda;
                deliveryDetail.ProductID = row.DT_RowId;
                deliveryDetail.OrderID = _orderID;
                tbDeliveriesDetails.push(deliveryDetail);
            });
            order.DeliveriesDetails = tbDeliveriesDetails;

            var myJsonString = JSON.stringify(order);

            $.ajax({
                url: '../Delivery/PostAjaxHandlerMakeDelivery',
                type: 'POST',
                dataType: 'json',
                data: { strMakeDeliveryJSON: myJsonString },
                "success": function (json) {
                    if (!json.NotAuthorized) {
                        alert('Completo');
                        window.history.back();
                    }
                },
                "error": handleAjaxError
            });
            event.preventDefault();

            return true;
        });

    }

    function fnFormatDetails(oTable, nTr) {
        var aData = oTable.fnGetData(nTr);
        console.log(aData);
        var sOut = '<table>';
        sOut += '<tr><td>Description:</td><td>' + aData.Product.Description + '</td></tr>';
        sOut += '<tr><td>Package:</td><td>' + aData.Product.Package + '</td></tr>';
        sOut += '<tr><td>Capacity:</td><td>' + aData.Product.Capacity + '</td></tr>';
        sOut += '<tr><td>Weight:</td><td>' + aData.Product.Weight + '</td></tr>';
        sOut += '</table>';

        return sOut;
    }

    return {
        //main function to initiate the module
        init: function (clientID, deliveryID) {
            _clientID = clientID;
            _orderID = deliveryID;
            initPage();
        },
        readOnly: function () {
            $('#tbProductItems').dataTable();

        }
    };
}();