$(document).ready(function () {

    $("#ProdCatId").on('change', function GetProductByCate(e) {

        var tr = '';
        $.each(ProudctList, function (index, item) {
            if (item.prodCatId == e.currentTarget.value) {
                tr += '<tr>';
                tr += '<td>' + item.prodName + '</td>';
                tr += '<td>' + item.prodDescription + '</td>';
                tr += '<td> <a href=/Product/Edit/' + item.productId + ' > Edit</a> |' +
                    '<a href=/Product/Details/' + item.productId + ' > Details</a> |' +
                    '<a href=/Product/Delete / ' + item.productId + ' > Delete</a></td>';
                tr += '</tr>';
            }
        });
        $("#ProductList").html(tr);

    });
    $("#productCreateArea").hide();
    $("#Createproduct").on('click', function HideProductList() {
        $("#productListArea,#Createproduct").hide();
        $("#productCreateArea").show();
    });

});