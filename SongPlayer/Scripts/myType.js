$(document).ready(function () {
    getProducts();
});

// Declare a variable to check when the action is Insert or Update
var isUpdateable = false;

// Get products list, by default, this function will be run first for the page load
function getProducts() {
    $.ajax({
        url: '/myTypes/GetProducts/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var rows = '';
            $.each(data, function (i, item) {
                rows += "<tr>"

                rows += "<td>" + item.TypeName + "</td>"

                rows += "<td><button type='button' id='btnEdit' class='btn btn-default' onclick='return getProductById(" + item.myTypeId + ")'>Edit</button> <button type='button' id='btnDelete' class='btn btn-danger' onclick='return deleteProductById(" + item.myTypeId + ")'>Delete</button> <button type='button' id='btnDetails' class='btn btn-default' onclick='return getProductById2(" + item.myTypeId + ")'>Details</button></td>"
                rows += "</tr>";
                $("#listProducts tbody").html(rows);
            });
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

// Get product by id
function getProductById(id) {
    $("#title").text("Edit myType");
    $.ajax({
        url: '/myTypes/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#myTypeId").val(data.myTypeId);
            $("#TypeName").val(data.TypeName);

            isUpdateable = true;
            $("#productModal").modal('show');
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

function getProductById2(id) {
    $("#title2").text("myType Details");
    $.ajax({
        url: '/myTypes/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#myTypeId2").val(data.myTypeId);
            $("#TypeName2").val(data.TypeName);

            isUpdateable = true;
            $("#productModal2").modal('show');
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

// Insert/ Update a product
$("#btnSave").click(function (e) {

    var data = {
        myTypeId: $("#myTypeId").val(),
        TypeName: $("#TypeName").val()

    }

    if (!isUpdateable) {
        $.ajax({
            url: '/myTypes/Create/',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                getProducts();
                $("#productModal").modal('hide');
                clear();
            },
            error: function (err) {
                alert("Error: " + "Nie jesteś zalogowany jako Admin");
            }
        })
    }
    else {
        $.ajax({
            url: '/myTypes/Update/',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                getProducts();
                isUpdateable = false;
                $("#productModal").modal('hide');
                clear();
            },
            error: function (err) {
                alert("Error: " + "Nie jesteś zalogowany jako Admin");
            }
        })
    }
});

// Delete product by id
function deleteProductById(id) {
    $("#confirmModal #title").text("Delete Product");
    $("#confirmModal").modal('show');
    $("#confirmModal #btnOk").click(function (e) {
        $.ajax({
            url: "/myTypes/Delete/" + id,
            type: "POST",
            dataType: 'json',
            success: function (data) {
                getProducts();
                $("#confirmModal").modal('hide');
            },
            error: function (err) {
                alert("Error: " + "Nie jesteś zalogowany jako Admin");
            }
        });

        e.preventDefault();
    });
}

// Set title for create new
$("#btnCreate").click(function () {
    $("#title").text("Create New");
})

// Close modal
$("#btnClose").click(function () {
    clear();
});

// Clear all items
function clear() {
    $("#myTypeId").val("");
    $("#TypeName").val("");

}