$(document).ready(function () {

    $("#ProdCatId").on('change', function GetProductByCate(e) {

        const product = PorudctList.filter(v => v.prodCatId == e.currentTarget.value)[0];
        var tr = '<tr>';
        tr += '<td>' + product.prodName + '</td>';
        tr += '<td>' + product.prodDescription + '</td>';
        tr += '<td> <a href=/Product/Edit/' + product.productId + ' > Edit</a> |' +
        '<a href=/Product/Details/' + product.productId + ' > Details</a> |' +
        '<a href=/Product/Delete / ' + product.productId +' > Delete</a></td>';
        tr += '</tr>';

        console.log(tr);
        $("#ProductList").html(tr);

    });
    $("#productCreateArea").hide();
    $("#Createproduct").on('click', function HideProductList() {
        $("#productListArea,#Createproduct").hide();
        $("#productCreateArea").show();
    });

});