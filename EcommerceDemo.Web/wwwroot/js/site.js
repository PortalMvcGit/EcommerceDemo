$(document).ready(function () {

    var deleteProductid;
    $("#productListArea").on('click', ' a#Deletemodel', DeleteProduct);

    $(document).on('change', '#ProductCreate #ProductCatCreate', function (e) {
        dropvalue = $(this).val();
        $('.attrName,.attrLabel').hide().val('');
        $('.attrName,.attrLabel').filter(function () {
            return $(this).attr('prodcatid') == dropvalue
        }).show();
    });

    var selectedProdCat;
    $("#ProdCatId").on('change', function GetProductByCate(e) {
        selectedProdCat = e.currentTarget.value;
        CreateProductList(ProudctList, e.currentTarget.value);
    });

    $("#productCreateArea").hide();

    $("#Createproduct").on('click', function HideProductList() {
        if (isEditpage == 1) {
            window.location.href = "/";
        } else {
            $("#productListArea,#Createproduct,#ProductEdit").hide();
            $("#productCreateArea").show();
        }
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

    $("#productUpdatebutton").on('click', function CreateProduct(e) {
        if ($("form").valid()) {
            var productViewModel = {};
            var productViewModeltest = $('form').serializeArray();
            productViewModel = CovertToJson(productViewModeltest, true);

            attrvalue = {};
            attrName = {};
            $.each(MasterAttrList, function (i, e) {
                i++;
                if (e.parentId == $("#ProdCatId").val()) {

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
                    $("#ModelText").html("Your Record has been updated successfully.");
                    $("#myModal").modal('show');
                    $("#ModelFooter").hide();
                } else {
                    $("#ModelTitle").html("Fail");
                    $("#ModelText").html("Oops Something went wrong.");
                    $("#myModal").modal('show');
                    $("#ModelFooter").hide();
                }
            })
        }
    });

});

function DelFromGlobalproduct() {
    var data = $.grep(ProudctList, function (e) {
        return e.productId != deleteProductid;
    });
    return data;
}

function CreateProductList(masterProductlist, prodctCatid) {
    var tr = '';
    var th = '';
    th += '<tr>';
    th += '<th>Product Name</th>';
    th += '<th>Product Description</th>';
    var counter = 0;
    $.each(masterProductlist, function (index, item) {
        if (item.prodCatId == prodctCatid) {
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
                " <a href='javascript:void(0);' data-ProductId=" + item.productId + " id='Deletemodel'>Delete</a></td>";
            tr += '</tr>';

            $.each(item.attributeNameList, function (index, attrName) {
                if (th.indexOf(attrName) == -1) {
                    th += '<th>' + attrName + '</th>';
                }

            });
        }
    });
    th += '<th>Operation</th></tr>';
    $("#ProductheaderList").html("").html(th);
    $("#ProductList").html("").html(tr);
}


function CancelDelete() {
    deleteProductid = '';
}

function DeleteProduct(e) {
    $("#myDeleteModal").modal("show");
    deleteProductid = $(e.currentTarget).attr('data-ProductId');
}

$("#ConfirmDelete").on('click', function (e) {
    var form = $('form');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        url: '/Product/Delete',
        method: 'GET',
        dataType: 'json',
        data: { 'id': deleteProductid },
        headers: {
            "RequestVerificationToken": token
        },
        contentType: "application/json",
        error: function (e) {
            console.log(e.status);
        }
    }).done(function (response) {
        if (response.status == "OK") {
            $("#myDeleteModal").modal('hide');
            ProudctList = DelFromGlobalproduct();
            CreateProductList(ProudctList, $("#ProdCatId").val())
            deleteProductid = '';
            $("#ModelTitle").html("Success");
            $("#ModelText").html("Your Record has been delete successfully.");
            $("#myModal").modal('show');
            $("#ModelFooter").hide();
        } else {
            $("#myDeleteModal").modal('hide');
            deleteProductid = '';
            $("#ModelTitle").html("Fail");
            $("#ModelText").html("Oops Something went wrong.");
            $("#myModal").modal('show');
            $("#ModelFooter").hide();
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