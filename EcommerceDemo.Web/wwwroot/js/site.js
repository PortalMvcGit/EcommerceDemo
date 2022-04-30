$(document).ready(function () {
    $(document).on('change', '#ProductCreate #ProductCatCreate', function (e) {
        dropvalue = $(this).val();
        $('.attrName,.attrLabel').hide().val('');
        $('.attrName,.attrLabel').filter(function () {
            return $(this).attr('prodcatid') == dropvalue
        }).show();
    });

    var selectedProdCat;
    $("#ProdCatId").on('change', function GetProductByCate(e) {
        var tr = '';
        var th = '';
        th += '<tr>';
        th += '<th>Product Name</th>';
        th += '<th>Product Description</th>';
        var counter = 0;
        selectedProdCat = e.currentTarget.value;
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
                    '<a href=/Product/Delete / ' + item.productId + ' > Delete</a></td>';
                tr += '</tr>';

                $.each(item.attributeNameList, function (index, attrName) {
                    if (th.indexOf(attrName) == -1) {
                        th += '<th>' + attrName + '</th>';
                    }

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

    
    $("#BacktoProductList").on('click', function HideProductList() {
        $("#productListArea,#Createproduct").show();
        $("#productCreateArea").hide();

    });


    $("#productCreatebutton").on('click', function CreateProduct(e) {
        if ($("form").valid()) {
            var productViewModel = {};
            var productViewModeltest = $('form').serializeArray();
            productViewModel = CovertToJson(productViewModeltest, true);

            attrvalue = {};
            attrName = {};
            $.each(MasterAttrList, function (i, e) {
                i++;
                if (e.parentId == $("[id*='ProductCatCreate'] :selected").val()) {
                    
                    attrvalue[i] = $("#attrName_" + i).val();
                    attrName[e.value] = e.name;
                    $("#attrName_" + i).val();
                    console.log(i);
                }
            });
            productViewModel.attributeNameList = attrName;
            productViewModel.attributeValueList = attrvalue;
            productViewModel.ProdCatId = $("#ProdCatId").val();
            var form = $('form');
            var token = $('input[name="__RequestVerificationToken"]', form).val();
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
                    console.log(e.status);
                }
            }).done(function (response) {
                if (response.status == "OK") {
                    $("#ModelTitle").html("Success");
                    $("#ModelText").html("Your Record has been inserted successfully");
                    $("#myModal").modal('show');
                    $("#ModelFooter").hide();
                } else {
                    $("#ModelTitle").html("Fail");
                    $("#ModelText").html("Oops Something went wrong");
                    $("#myModal").modal('show');
                    $("#ModelFooter").hide();
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