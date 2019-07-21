var TraceTypeComponents = function () {
    var oTable;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        oTable = $('#tbTracesTypes').dataTable({
            "sAjaxSource": '../Trace/GetAjaxHandlerTracesTypes',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "Description",
                            "bSortable": false
                        },
                        {
                            "mData": "TypeTrace",
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
            $('#tbTracesTypes').dataTable();

        }
    };
}();