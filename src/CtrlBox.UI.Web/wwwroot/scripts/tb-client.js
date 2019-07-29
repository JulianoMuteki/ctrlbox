var TableEditable = function () {
    return {
        //main function to initiate the module
        init: function (url) {

            function getElementEdit() {
                return '<a class="edit" href="javascript:;"><i class="icon-edit"></i> Edit</a>';
            }
            function getElementDelete() {
                return '<a class="delete" href="javascript:;"><i class="icon-trash"></i> Delete</a>';
            }

            var oTable = $('#sample_editable_1').dataTable({
                "sAjaxSource": url,
                "bProcessing": true,
                "bDestroy": true,
                "aoColumns": [
                    {
                        "mData": "Name", "name": "Name", "autoWidth": true },
                    {
                        "mData": "QuantityBoxes",
                        "defaultContent": "<i>not found</i>"
                    },
                    {
                        "mData": "BalanceDue",
                        "defaultContent": "<i>not found</i>"
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                var id = row.AddressID;
                                return '<label onClick="AppCtrlBox.callModalAddress(\'' + id + '\')" class="btn mini"><i class="icon-link"></i> Address</label>';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": "Phone"
                    },                    
                    {
                        "mData": "Contact"
                    },
                    {
                        "mData": "SaleIsFinished",
                        "defaultContent": "<i>not found</i>"
                    },                  
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<a href="/Client/Create?clientID=' + data.DT_RowId + '" class="edit"><i class="icon-edit"></i> Edit</a>';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return getElementDelete();
                            }
                            return data;
                        }
                    },
                      {
                          "mData": null,
                          "sType": "html",
                          "mRender": function (data, type, row) {
                              if (type === 'display') {
                                  var url = '/Product/ClientProductValue?clientID=' + data.DT_RowId;
                                  return '<a href="' + url + '" class="btn mini"><i class="icon-link"></i> Price product</a>';
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