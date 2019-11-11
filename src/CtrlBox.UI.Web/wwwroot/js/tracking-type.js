var TrackingTypeComponents = function () {
    var oTable;

    function initPage() {
        inicializateComponentes();
    }

    function inicializateComponentes() {
        oTable = $('#tbTrackingTypes').dataTable({
            "sAjaxSource": '../Tracking/GetAjaxHandlerTrackingTypes',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": null,
                            "sType": "html",
                            "bSortable": false,
                            "mRender": function (data, type, row) {
                                if (type === 'display') {
                                    if (data.Picture === null || data.PictureID === undefined) {
                                        return '<img style="width:25px; height: 25px;" src="/../img/avatar.png" />  ' + data.Description;
                                    }
                                    return '<img style="width:25px; height: 25px;" src="' + data.Picture.SrcBase64Image + '" />  ' + data.Description;
                                }
                                return data;
                            }
                        },
                        {
                            "mData": "TrackType",
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