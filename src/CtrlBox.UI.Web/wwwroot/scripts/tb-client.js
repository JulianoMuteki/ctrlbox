﻿var TableEditable = function () {

    return {

        //main function to initiate the module
        init: function (url) {
            function restoreRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);


                oTable.fnUpdate(aData.Name, nRow, 0, false);
                oTable.fnUpdate(aData.QuantityBoxes, nRow, 1, false);
                oTable.fnUpdate(aData.BalanceDue, nRow, 2, false);
                oTable.fnUpdate(getElementEdit(), nRow, 7, false);
                oTable.fnUpdate(getElementDelete(), nRow, 8, false);

                oTable.fnDraw();
            }

            function editRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text" class="m-wrap medium" value="' + aData.Name + '">';
                jqTds[1].innerHTML = '<input type="text" class="m-wrap small" value="' + aData.QuantityBoxes + '">';
                jqTds[2].innerHTML = '<input type="text" class="m-wrap small" value="' + aData.BalanceDue + '">';
                jqTds[7].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[8].innerHTML = '<a class="cancel" href="">Cancel</a>';
            }

            function saveRow(oTable, nRow) {
                var jqInputs = $('input', nRow);

                var data = {};
                data.DT_RowId = nRow.id;
                data.Name = jqInputs[0].value;
                data.QuantityBoxes = jqInputs[1].value;
                data.BalanceDue = jqInputs[2].value;

                var data_json = JSON.stringify(data);

                // simple ajax call to post your json data
                $.ajax({
                    type: 'post',
                    url: 'Client/Edit',
                    dataType: 'json',
                    data: { produtoVM: data },
                    success: function (result) {
                        // check result object for what you returned
                        oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                        oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                        oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                        oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 7, false);
                        oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 8, false);
                        oTable.fnDraw();
                    },
                    error: function (error) {
                        // check error object or return error
                        alert(error);
                    }
                });
            }

            function getElementEdit() {
                return '<a class="edit" href="javascript:;"><i class="icon-edit"></i> Edit</a>';
            }
            function getElementDelete() {
                return '<a class="delete" href="javascript:;"><i class="icon-trash"></i> Delete</a>';
            }

            function cancelEditRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 7, false);
                oTable.fnDraw();
            }

            var oTable = $('#sample_editable_1').dataTable({
                "sAjaxSource": url,
                "bProcessing": true,
                "bDestroy": true,
                "aoColumns": [
                    {
                        "mData": "Name", "name": "Name", "autoWidth": true },
                    {
                        "mData": "QuantityBoxes"
                    },
                    {
                        "mData": "BalanceDue"
                    },
                    {
                        "mData": "Address"
                    },
                    {
                        "mData": "Phone"
                    },                    
                    {
                        "mData": "Contact"
                    },
                    {
                        "mData": "IsDelivery"
                    },                  
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return getElementEdit();
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

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialize select2 dropdown

            var nEditing = null;

            $('#sample_editable_1_new').click(function (e) {
                e.preventDefault();
                var aiNew = oTable.fnAddData(['', '', '',
                    '<a class="edit" href="">Edit</a>', '<a class="cancel" data-mode="new" href="">Cancel</a>'
                ]);
                console.log(aiNew[0]);
                var nRow = oTable.fnGetNodes(aiNew[0]);
                editRow(oTable, nRow);
                nEditing = nRow;
            });

            $('#sample_editable_1 a.delete').live('click', function (e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];
                oTable.fnDeleteRow(nRow);
                alert("Deleted! Do not forget to do some ajax to sync with backend :)");
            });

            $('#sample_editable_1 a.cancel').live('click', function (e) {
                e.preventDefault();
                if ($(this).attr("data-mode") == "new") {
                    var nRow = $(this).parents('tr')[0];
                    oTable.fnDeleteRow(nRow);
                } else {
                    restoreRow(oTable, nEditing);
                    nEditing = null;
                }
            });

            $('#sample_editable_1 a.edit').live('click', function (e) {
                e.preventDefault();

                /* Get the row as a parent of the link that was clicked on */
                var nRow = $(this).parents('tr')[0];

                if (nEditing !== null && nEditing != nRow) {
                    console.log("nrow " + nRow)
                    /* Currently editing - but not this row - restore the old before continuing to edit mode */
                    restoreRow(oTable, nEditing);
                    editRow(oTable, nRow);
                    nEditing = nRow;
                } else if (nEditing == nRow && this.innerHTML == "Save") {
                    /* Editing this row and want to save it */
                    saveRow(oTable, nEditing);
                    nEditing = null;
                    alert("Updated completed!");
                } else {
                    /* No edit in progress - let's start one */
                    editRow(oTable, nRow);
                    nEditing = nRow;
                }
            });
        }
    };
}();