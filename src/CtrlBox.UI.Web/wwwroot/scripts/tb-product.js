var TableProduct = function () {

    return {

        //main function to initiate the module
        init: function (url) {

            function elementEdit() {
                return '<a class="edit" href="javascript:;"><i class="icon-edit"></i> Edit</a>';
            }
            function elementDelete() {
                return '<a class="delete" href="javascript:;"><i class="icon-trash"></i> Delete</a>';
            }

            var oTable = $('#sample_editable_1').dataTable({
                "sAjaxSource": url,
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
                                    return '<div class="fileupload-new thumbnail" style="width:15px; height: 15px;"><img  src="/../img/avatar.png" /></div>';
                                }
                                return '<div class="fileupload-new thumbnail" style="width:15px; height: 15px;"><img  src="/../Configuration/ViewImage/' + data.PictureID + '" /></div>';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": "Name"
                    },
                    {
                        "mData": "Description"
                    },
                    {
                        "mData": "Package"
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "bSortable": false,
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<span class="label label-danger"><span class="bold">' + data.Capacity + ' ' + data.UnitMeasure + '</span></span> ';
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
                                return '<span class="label label-danger"><span class="bold">' + data.Weight + ' ' + data.MassUnitWeight + '</span></span> ';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<a href="/Product/Create?productID=' + data.DT_RowId + '" class="edit"><i class="icon-edit"></i> Edit</a>';
                            }
                            return data;
                        }
                    },
                    {
                        "mData": null,
                        "sType": "html",
                        "mRender": function (data, type, row) {
                            if (type === 'display') {
                                return '<a class="delete" href="javascript:;"><i class="icon-trash"></i> Delete</a>';
                            }
                            return data;
                        }
                    }
                ]
            });
        }

    };

}();