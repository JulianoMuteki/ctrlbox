var BoxTypeComponents = function () {
    var oTableBox;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        oTableBox = $('#tbBoxesType').dataTable({
            "sAjaxSource": '../Box/GetAjaxHandlerBoxesType',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "Name",
                            "bSortable": false
                        },
                        {
                            "mData": "Description",
                            "bSortable": false
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