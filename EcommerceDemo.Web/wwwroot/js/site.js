$(document).ready(function () {

    $("#ProdCatId").on('change', function GetProductByCate(e) {
        var tr = '';
        var th = '';
        th += '<tr>';
        th += '<th>Product Name</th>';
        th += '<th>Product Description</th>';
        var counter = 0;
        $.each(ProudctList, function (index, item) {
            if (item.prodCatId == e.currentTarget.value) {
                tr += '<tr>';
                tr += '<td>' + item.prodName + '</td>';
                tr += '<td>' + item.prodDescription + '</td>';

                if (Object.keys(item.attributeValueList).length > 0) {
                    $.each(item.attributeValueList, function (index, attrvalue) {
                        tr += '<td>' + attrvalue + '</td>';
                        counter++;
                    });
                } else {
                    for (var i = 0; i < counter; i++) {
                        tr += '<td></td>';
                    }
                }
                tr += '<td> <a href=/Product/Edit/' + item.productId + ' > Edit</a> |' +
                    '<a href=/Product/Details/' + item.productId + ' > Details</a> |' +
                    '<a href=/Product/Delete / ' + item.productId + ' > Delete</a></td>';
                tr += '</tr>';

                $.each(item.attributeNameList, function (index, attrName) {
                    th += '<th>' + attrName + '</th>';
                });
            }
        });
        th += '<th>Operation</th></tr>';
        $("#ProductheaderList").html(th);
        $("#ProductList").html(tr);

    });
    $("#productCreateArea").hide();
    $("#Createproduct").on('click', function HideProductList() {
        $("#productListArea,#Createproduct").hide();
        $("#productCreateArea").show();
    });


    $("#productCreatebutton").on('click', function CreateProduct(e) {
        if ($("form").valid()) {
            var productViewModeltest = $('form').serializeArray();
            var productViewModel = CovertToJson(productViewModeltest, true);
            productViewModel.attributeNameList = MasterAttrList;
            attrvalue = {};
            $.each(MasterAttrList, function (i, e) {
                attrvalue[i] = $("#attrName_" + i).val();
                $("#attrName_" + i).val();
                console.log(i);
            });
            debugger;
            productViewModel.attributeValueList = attrvalue;
            var form = $('form');
            var token = $('input[name="__RequestVerificationToken"]', form).val();
            debugger;
            $.ajax({
                url: '/Product/CreateProduct',
                method: 'POST',
                dataType: 'json',
                headers: {
                    "RequestVerificationToken": token
                },
                contentType: "application/json",
                data: JSON.stringify(productViewModel),
                error: function (e) {
                    console.log(e);
                }
            }).done(function (response) {
                if (response.status == "Ok") {
                    alert('Hi Ok staus')
                }
            })
        }
    });

});

function CovertToJson(data, returnJsonObject) {
    var jsonData = {};
    $(data).each(function (i, e) {
        jsonData[e.name] = e.value;
    });

    if (returnJsonObject == true) {
        return jsonData;
    } else {
        return JSON.stringify(jsonData);
    }
}