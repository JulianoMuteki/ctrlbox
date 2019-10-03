var _clientID = '';
var _orderID = '';

function blurFunction(obj) {
    console.log(obj);
    //var nRow = $(this).parents('tr')[0];
    //var qtde = $(nRow).find('.txtTotalBoxes').val();

    ////var rowIndex = tb.fnGetPosition($(this).closest('tr')[0]);
    ////tb.fnUpdate(totalRow.formatMoney(), rowIndex, 5);

    //console.log(oTable.row(nRow).data());
}

var MakeDeliveryComponents = function () {
    var oTable;
    var order = {};

    function initPage() {
        inicializateComponentes();
    }

    function visibleInputBox() {
        oTable.column(3).visible(true);
    }

    function isAllowedCrossDocking(data) {
        return (data.BoxType.Total * data.BoxType.MaxBox) == data.TotalProductItems;
    }

    function getInputForBoxes(data) {
        if (isAllowedCrossDocking(data)) {
            var inputBox = '<div style="display: none;" class="forTotalBoxes span6">' +
                 '<div class="user-info">' +
                     '<div><input type="text" placeholder="0" class="m-wrap small txtTotalBoxes" /></div>' +
                     '<div><span class="label label-success"> boxes</span></div>' +
                 '</div>' +
             '</div>';
            return inputBox;
        } else {
            return '';
        }
    }

    function inicializateComponentes() {
        $('.select2_option').select2({
            allowClear: true
        });

        oTable = $('#tbProductItems').dataTable({
            "sAjaxSource": '../Delivery/GetAjaxHandlerMakeDelivery',
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "clientID", "value": _clientID });
                aoData.push({ "name": "deliveryID", "value": _orderID });
            },
            "bProcessing": true,
            "bDestroy": true,
            "bPaginate": false,
            "bFilter": false, "bInfo": false,
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
                                    var link = '<div class="user-info">' +
                                        '<img  src="/../Configuration/ViewImage/' + data.BoxType.BTypePictureID + '" />' +
                                            '<div>' +
                                                '<a href="#">' + data.BoxType.BTypeName + '</a> ' +
                                            '</div>' +
                                            '<div><span class="label label-success boxTypeTotal">' + data.BoxType.Total + '</span> boxes total</div>' +
                                    '</div>';
                                    return link;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.PictureID === '' || data.PictureID === null) {
                                        return '</span><img style="width:15px; height: 15px;"  src="/../img/avatar.png" /> ' + data.NomeProduto;
                                    }
                                    var link = '<div class="user-info">' +
                                                  '<img  src="/../Configuration/ViewImage/' + data.PictureID + '" />' +
                                                      '<div>' +
                                                          '<a href="#">' + data.ProductName + '</a> ' +
                                                      '</div>' +
                                                      '<div><span class="label label-info">' + data.TotalProductItems + '</span> product items total <span class="showDetails row-details-close"><i id="iconDetails" class="m-icon-swapdown m-icon-black"></i></span></div>' +
                                              '</div>';
                                    return link;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "autoWidth": true,
                            "sClass": "tdTotalBoxes",
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    var link = '<div class="row-fluid">' +
                                                    '<div class="span6 forTotalProductItems">' +
                                                        '<div class="user-info">' +
													        '<div><input type="text" placeholder="0" class="m-wrap small txtTotalProductItems"></div>' +
													        '<div><span class="label label-info"> product Items</span></div>' +
												        '</div>' +
                                                    '</div>' +
                                                    getInputForBoxes(data) +
                                                '</div>';
                                    return link;
                                }
                                return data;
                            }
                        },
                        {
                            "sType": "html",
                            "autoWidth": true,
                            "bSortable": false,
                            "autoWidth": false,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    var link = '<span class="label label"> Blocked</span>';
                                    if (isAllowedCrossDocking(data)) {
                                        link = '<div><select class="ddlCrossDocking span6 select2_option" name="CrossDocking"><option value="0">No</option><option value="1">Yes</option></select></div>';
                                    }
                                    return link;
                                }
                                return data;
                            }
                        }
            ],
            "drawCallback": function (settings) {
                $(".ddlCrossDocking").change(function () {
                    $(this).closest("tr").children("td:nth-child(3)").find('div.forTotalBoxes input.txtTotalBoxes').val('');
                    $(this).closest("tr").children("td:nth-child(3)").find('div.forTotalProductItems input.txtTotalProductItems').val('');

                    if ($(this).val() == 1) {
                        $(this).closest("tr").children("td:nth-child(3)").find('div.forTotalBoxes').show();
                        $(this).closest("tr").children("td:nth-child(3)").find('div.forTotalProductItems input.txtTotalProductItems').attr('disabled', 'disabled');
                    } else {
                        $(this).closest("tr").children("td:nth-child(3)").find('div.forTotalBoxes').hide();
                        $(this).closest("tr").children("td:nth-child(3)").find('div.forTotalProductItems input.txtTotalProductItems').removeAttr('disabled');
                    }
                });


            }
        });

        $('#tbProductItems tbody').on('blur', 'td.tdTotalBoxes input.txtTotalBoxes', function () {
            var nRow = $(this).parents('tr')[0];
            var total = $(nRow).find('.txtTotalBoxes').val();
            var orderProductItemsGroup = $('#tbProductItems').DataTable().row(nRow).data();
            $(this).closest("td").find('div.forTotalProductItems input.txtTotalProductItems').val(total * orderProductItemsGroup.BoxType.MaxBox);
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

                var totalProductItems = $(value).find('input.txtTotalProductItems').val();
                totalProductItems = totalProductItems || 0;

                var totalBoxes = $(value).find('input.txtTotalBoxes').val();
                totalBoxes = totalBoxes || 0;

                if (totalProductItems > 0) {
                    deliveryDetail.ClientID = _clientID;
                    deliveryDetail.QuantityProductItem = totalProductItems;
                    deliveryDetail.ProductID = row.DT_RowId;
                    deliveryDetail.OrderID = _orderID;
                    deliveryDetail.HasCrossDocking = $(value).find('select.crossDocking').val();
                    if (deliveryDetail.HasCrossDocking === undefined || deliveryDetail.HasCrossDocking === null)
                        deliveryDetail.HasCrossDocking = false;

                    deliveryDetail.QuantityBoxes = totalBoxes;
                    tbDeliveriesDetails.push(deliveryDetail);
                }
            });
            order.DeliveriesDetails = tbDeliveriesDetails;

            var myJsonString = JSON.stringify(order);
            var trackTypeID = $('#ddlTrackingType').val();

            $.ajax({
                url: '../Delivery/PostAjaxHandlerMakeDelivery',
                type: 'POST',
                dataType: 'json',
                data: { strMakeDeliveryJSON: myJsonString, trackingTypeID: trackTypeID },
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