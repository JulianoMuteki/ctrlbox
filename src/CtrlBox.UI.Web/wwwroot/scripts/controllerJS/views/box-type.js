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
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.PictureID === '' || data.PictureID === null) {
                                        return '<img style="width:25px; height: 25px;" src="/../img/avatar.png" />  ' + data.Name;
                                    }
                                    return '<img style="width:25px; height: 25px;" src="/../Configuration/ViewImage/' + data.PictureID + '" />  ' + data.Name;
                                }
                                return data;
                            }
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