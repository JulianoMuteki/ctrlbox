var AddressPage = function () {
    var oTableSale = {};
    function initPage() {
        inicializateComponentes();

    }
    function inicializateComponentes() {


        oTableSale = $('#tbAddress').dataTable({
            "sAjaxSource": 'Address/GetAjaxHandlerAddresses',
            "bProcessing": true,
            "bDestroy": true,
            "aoColumns": [
                        {
                            "mData": "CEP",
                            "bSortable": false
                        },
                        {
                            "mData": "Street",
                            "bSortable": false
                        },
                        {
                            "mData": "City",
                            "bSortable": false
                        },
                        {
                            "mData": "Number",
                            "bSortable": false
                        },
                        {
                            "mData": "District",
                            "bSortable": false
                        },
                        {
                            "mData": "Estate",
                            "bSortable": false
                        },
                        {
                            "mData": "Reference",
                            "bSortable": false
                        }                      
            ]
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            initPage();
        },  
    };
}();
