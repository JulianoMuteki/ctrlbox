var _ddlRoute = '#ddlRoute';
var _ddlUser = '#ddlUser'

var CreateOrderComponents = function () {
    var oTable;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        $('.select2_option').select2({
            allowClear: true
        });

        $(_ddlRoute).change(function (event) {
            if ($(this).val() !== '0') {

                loadTableBox($(this).val());
            }
        });

        $("#btnSubmit").click(function () {
            var oTable = $('#tbBox').dataTable();
            var tb = [];
            $.each(oTable.fnGetNodes(), function (index, value) {
                if ($(value).find('input.quantityToDelivery').val() > 0) {
                    var row = oTable.fnGetData(value);

                    var quantityToDelivery = $(value).find('input.quantityToDelivery').val();
                    if (quantityToDelivery !== undefined && quantityToDelivery !== '') {
                        row.QuantityToDelivery = quantityToDelivery;
                        row.BoxTypeID = row.DT_RowId;
                        tb.push(row);
                    }
                }
            });
            var myJsonString = JSON.stringify(tb);
            var routeId = $(_ddlRoute).val();
            var userId = $(_ddlUser).val();

            $.ajax({
                url: '../Delivery/PostAjaxHandlerCreateDelivery',
                type: 'POST',
                dataType: 'json',
                data: { tbBoxesTypes: myJsonString, routeID: routeId, userID: userId },
                "success": function (json) {
                    if (!json.NotAuthorized) {
                        alert('Completo');
                        window.history.go(-1);
                    }
                },
                "error": handleAjaxError
            });
            event.preventDefault();
            return true;
        });
    }

    function loadTableBox(routeID) {
        $('#tbBox').dataTable({
            "sAjaxSource": '../Delivery/GetBoxesByRouteID',
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "routeID", "value": routeID });
            },
            "bPaginate": false,
            "bAutoWidth": false,
            "bFilter": false, "bInfo": false,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.SrcPicture === '' || data.SrcPicture === null) {
                                        return '<img style="width:15px; height: 15px;"  src="/../img/avatar.png" /> ' + data.BoxType;
                                    }
                                    return '<img  src="' + data.SrcPicture + '" /> ' + data.BoxType;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    var statusStock = "green-stripe";
                                    if (row.TotalBox <= 0) {
                                        statusStock = "red-stripe"
                                    }
                                    return '<div class="btn mini ' + statusStock + '">Total: ' + row.TotalBox + '</div>';
                                }
                                return data;
                            }
                        },
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    var txtTotal = '<input type="text" placeholder="0" class="m-wrap small quantityToDelivery">';

                                    if (row.TotalBox <= 0) {
                                        txtTotal = '<span class="label label-important">Unavailable</span>'
                                    }
                                    return txtTotal;
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

        }
    };
}();