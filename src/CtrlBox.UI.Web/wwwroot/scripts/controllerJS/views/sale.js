var _clientID = '';
var _routeID = '';
var _deliveryID = '';

var SaleComponents = function () {
    var sale = {};
    var paymentTemp = {};
    var nextDate = new Date();
    sale.Payment = {};
    sale.Payment.NumberParcels = 0;
    sale.Payment.IsCashPayment = false;

    var oTableSale;

    function initPage() {
        inicializateComponentes();

        $("#exprireDate").val(moment(add_months(new Date(), 1)).format("DD-MM-YYYY"));

        $("#btnClear").click(function (event) {
            initialPayment();
        });

        $('#tbSale tbody').on('blur', 'td.calc', function () {
            var sum = 0;
            var nRow = $(this).parents('tr')[0];

            var valor = $(this).closest('tr').children().eq(4);
            var valorProduto = parseFloat($(valor).html().replace(/[^0-9\,]+/g, "").replace(",", "."));

            var qtde = $(nRow).find('.qtdeVenda').val();
            var valueDiscount = $(nRow).find('.discountValueSale').val();

            var totalRow = 0;
            if (valueDiscount !== undefined && valueDiscount !== '') {
                totalRow = (valorProduto * qtde) - parseInt(valueDiscount);
            }
            else {
                totalRow = valorProduto * qtde;
            }

            var tb = $('#tbSale').dataTable();

            var rowIndex = tb.fnGetPosition($(this).closest('tr')[0]);
            tb.fnUpdate(totalRow.formatMoney(), rowIndex, 5);

            $("#tbSale tbody .somaTotal").each(function () {
                var valor = $(this).html();
                sum += parseFloat(valor.replace(/[^0-9\,]+/g, "").replace(",", "."));
            });
            sale.TotalAmount = sum;
            $('#txtTotalAmount').val(sale.TotalAmount.formatMoney());

            $('#somaVenda').html(sum.formatMoney());

            return false;
        });

        $(".btnSubmit").click(function () {

            sale.ClientID = _clientID;
            sale.DeliveryID = _deliveryID;

            var tbVenda = [];
            $.each(oTableSale.fnGetNodes(), function (index, value) {
                var row = oTableSale.fnGetData(value);
                var qtdeVenda = $(value).find('input.qtdeVenda').val();
                var discountValueSale = $(value).find('input.discountValueSale').val();

                qtdeVenda = qtdeVenda || 0;
                discountValueSale = discountValueSale || 0;

                row.Quantity = qtdeVenda;
                row.DiscountValueSale = discountValueSale;

                row.ValueProductSale = parseFloat(row.ValorProduto.replace(/[^0-9\,]+/g, "").replace(",", "."));
                row.TotalValue = parseFloat(row.Total.replace(/[^0-9\,]+/g, "").replace(",", "."));
                row.ProductID = row.DT_RowId;
                tbVenda.push(row);
            });
            sale.SalesProducts = tbVenda;

            var myJsonString = JSON.stringify(sale);

            $.ajax({
                url: 'Sale/PostAjaxHandlerAddSale',
                type: 'POST',
                dataType: 'json',
                data: { strSaleJSON: myJsonString },
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

        $("#AddPayment").click(function (event) {
            addPaymentSchedule();
        });

        $.ajax({
            type: "GET",
            url: 'Sale/GetAjaxHandlerPayMethods',
            dataType: "json",
            "success": function (data) {
                console.log(data);
                if (data.aaData.length > 0) {
                    $.each(data.aaData, function (index, obj) {
                        var newOption = new Option(obj.Text, obj.Value, false, obj.Selected);
                        $("#ddlMethod").append(newOption).trigger('change');
                    });
                }
            },
            "error": handleAjaxError
        });

        initialPayment();
    }

    function inicializateComponentes() {
        $.extend($.inputmask.defaults, {
            'autounmask': true
        });
        $("#valueDivide").inputmask('R$ 999.999,99', { numericInput: true, rightAlignNumerics: false, greedy: false }); //123456  =>  € ___.__1.234,56
        $("#exprireDate").inputmask("d/m/y", { "placeholder": "dd/mm/yyyy" }); //multi-char placeholder

        $("#slider-range-max").slider({
            range: "max",
            min: 1,
            max: 12,
            value: 1,
            slide: function (event, ui) {
                $("#slider-range-max-amount").text(ui.value);

                calcDivide(ui.value);
            }
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
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mData": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.PictureID === '' || data.PictureID === null) {
                                        return '<img style="width:15px; height: 15px;"  src="/../img/avatar.png" /> ' + data.NomeProduto;
                                    }
                                    return '<img  src="/../Configuration/ViewImage/' + data.PictureID + '" /> ' + data.NomeProduto;
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
                                    return '<span class="btn mini ' + statusStock + '">Total: ' + data.TotalBox + ' ' + data.UnitMeasure + '</span>';
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
                                    if (data.TotalBox == 0) {
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
                                    if (data.TotalBox == 0) {
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

        $('.radio input[type=radio][name=IsCashPayment]').change(function () {           
            sale.Payment.IsCashPayment = $(".radio input[type=radio][id=IsCashRadio]").is(":checked");
            
            if (sale.Payment.IsCashPayment) {
                $(".isCashPayment").hide();
            } else {
                $(".isCashPayment").show();
            }
        });
    }

    function initialPayment() {
        sale.Payment.PaymentsSchedules = [];
        sale.Rest = 0;
        paymentTemp = {};
        nextDate = new Date();
        validadeInputs();

        $("#exprireDate").prop('disabled', false);
        $("#idPayment-Group").show();
        $('#tbPayment tbody tr').remove();
        $("#txtRest").val(sale.Rest.formatMoney());
        $("#slider-range-max").slider("values", 0);
    }

    function validadeInputs() {
        if ($("#exprireDate").val() == '' || $("#valueDivide").val() == '') {

            $(".errorForm").addClass("error");
            $(".iconError").show();
            return false;
        }
        else {
            $(".errorForm").removeClass("error");
            $(".iconError").hide();
            return true;
        }
    }

    function add_months(dt, n) {
        return new Date(dt.setMonth(dt.getMonth() + n));
    }

    function addPaymentSchedule() {
        if (!validadeInputs()) {
            return false;
        }

        paymentTemp.NumberDevide = $("#slider-range-max").slider("value");

        if (sale.Payment.PaymentsSchedules.length === 0) {
            sale.Rest = sale.TotalAmount;
            nextDate = new Date(moment($("#exprireDate").val(), "DD/MM/YYYY").format("YYYY-MM-DD"));
        }

        for (var i = 0; i < paymentTemp.NumberDevide; i++) {
            var paymentSchedule = {};

            paymentSchedule.ExprireDate = new Date(nextDate);
            paymentSchedule.PaymentMethodID = $("select#ddlMethod").val();
            paymentSchedule.MethodName = $("#ddlMethod").select2('data').text;

            paymentSchedule.BenefitValue = parseFloat($("#valueDivide").val().replace(/[^0-9\,]+/g, "").replace(",", "."));

            sale.Rest -= paymentSchedule.BenefitValue;
            sale.Payment.PaymentsSchedules.push(paymentSchedule);

            var dt = add_months(nextDate, 1);
            nextDate = dt;
        }
        $("#txtRest").val(sale.Rest.formatMoney());

        addRowsTablePayments();

        if (sale.Payment.PaymentsSchedules.length > 0) {
            $("#exprireDate").prop('disabled', true);
        }
        if (sale.Rest == 0) {
            $("#idPayment-Group").hide();
        }

        sale.Payment.NumberParcels = sale.Payment.PaymentsSchedules.length;
        sale.Payment.Amount = 0;
    }

    function addRowsTablePayments() {
        $('#tbPayment tbody tr').remove();

        for (var i = 0; i < sale.Payment.PaymentsSchedules.length; i++) {
            $('#tbPayment tbody').append(
                "<tr>" +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + parseFloat(sale.Payment.PaymentsSchedules[i].BenefitValue).formatMoney() + '</td>' +
                '<td>' + sale.Payment.PaymentsSchedules[i].MethodName + '</td>' +
                '<td>' + moment(sale.Payment.PaymentsSchedules[i].ExprireDate).format("DD-MM-YYYY") + '</td>' +
                '</tr>'
            );
        }
    }

    function calcDivide(devide) {
        var valorParcela = 0;
        if (sale.Rest == 0) {
            valorParcela = sale.TotalAmount / devide;
        } else {
            valorParcela = sale.Rest / devide;
        }

        var valueFormat = parseFloat(valorParcela).formatMoney();

        $("#valueDivide").val(valueFormat.replace(/[^0-9\,]+/g, ""));
        validadeInputs();
    }

    return {
        //main function to initiate the module
        init: function (clientID, routeID, deliveryID) {
            _clientID = clientID;
            _routeID = routeID;
            _deliveryID = deliveryID;
            initPage();
        },
        readOnly: function (totalValueSale, isCashPayment) {
            $('#tbSale').dataTable();
            $(".isReadOlny").hide();
            $('#txtTotalAmount').val(parseInt(totalValueSale).formatMoney());
            $('.radio').remove();
            
            if (isCashPayment) {            
                $('#rdCashPayment').append('<input class="m-wrap medium" type="text" placeholder="Cash Payment" disabled="">');
                $(".isCashPayment").hide();
            }else
            {
                $('#rdCashPayment').append('<input class="m-wrap medium" type="text" placeholder="Financed Payment" disabled="">');
            }
        }
    };
}();