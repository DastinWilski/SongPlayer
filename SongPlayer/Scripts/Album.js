$(document).ready(function () {
    getProducts();
});

// Declare a variable to check when the action is Insert or Update
var isUpdateable = false;

// Get products list, by default, this function will be run first for the page load
function getProducts() {
    $.ajax({
        url: '/Albums/GetProducts/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var rows = '';
            $.each(data, function (i, item) {
                rows += "<tr>"
                
                rows += "<td>" + item.AlbumName + "</td>"
                rows += "<td>" + item.Author + "</td>"
                rows += "<td><button type='button' id='btnEdit' class='btn btn-default' onclick='return getProductById(" + item.AlbumId + ")'>Edit</button> <button type='button' id='btnDelete' class='btn btn-danger' onclick='return deleteProductById(" + item.AlbumId + ")'>Delete</button> <button type='button' id='btnDetails' class='btn btn-default' onclick='return getProductById2(" + item.AlbumId + ")'>Details</button></td>"
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
    $("#title").text("Edit Album");
    $.ajax({
        url: '/Albums/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#AlbumId").val(data.AlbumId);
            $("#AlbumName").val(data.AlbumName);
            $("#Author").val(data.Author);
            isUpdateable = true;
            $("#productModal").modal('show');
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

function getProductById2(id) {
    $("#title2").text("Album Details");
    $.ajax({
        url: '/Albums/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#AlbumId2").val(data.AlbumId);
            $("#AlbumName2").val(data.AlbumName);
            $("#Author2").val(data.Author);
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
        AlbumId: $("#AlbumId").val(),
        AlbumName: $("#AlbumName").val(),
        Author: $("#Author").val()
    }

    if (!isUpdateable) {
        $.ajax({
            url: '/Albums/Create/',
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
            url: '/Albums/Update/',
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
            url: "/Albums/Delete/" + id,
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
    $("#AlbumId").val("");
    $("#AlbumName").val("");
    $("#Author").val("");
}